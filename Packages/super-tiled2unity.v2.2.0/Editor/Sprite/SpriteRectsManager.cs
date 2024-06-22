﻿using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Xml.Linq;
using System;

namespace SuperTiled2Unity.Editor
{
    internal class SpriteRectsManager
    {
        internal static SpriteRectsManager Instance { get; }

        private readonly HashSet<RectangleEntry> m_RectangleEntries = new HashSet<RectangleEntry>();

        private readonly List<string> m_TextureAssetsReimported = new List<string>();

        static SpriteRectsManager()
        {
            Instance = CreateInstance();
        }

        internal IEnumerable<Rect> GetSpriteRectsForTexture(string assetPathTexture)
        {
            var absolutePathTexture = Path.GetFullPath(assetPathTexture).SanitizePath().ToLower();
            return m_RectangleEntries.Where(r => r.AbsolutePathTexture == absolutePathTexture).Select(r => new Rect(r.X, r.Y, r.Width, r.Height)).Distinct();
        }

        private static string GetSanitizedAbsolutePathFromRelative(string absolutePathParent, string relativeImagePath)
        {
            using (ChDir.FromFilename(absolutePathParent))
            {
                return Path.GetFullPath(relativeImagePath).SanitizePath();
            }
        }

        private void ProcessTiledFile(string path)
        {
            var absoluteTsxPath = Path.GetFullPath(path).SanitizePath().ToLower();
            m_RectangleEntries.RemoveWhere(r => r.AbsolutePathTsx == absoluteTsxPath);

            try
            {
                // Open up the filed file and look for "tileset" elements
                XDocument doc = XDocument.Load(absoluteTsxPath);
                if (doc == null)
                {
                    Debug.LogError($"'Looking for tilesets: {absoluteTsxPath}' failed to load.");
                    return;
                }

                foreach (var xTileset in doc.Descendants("tileset"))
                {
                    // External tilesets (which have a source) are ignored here. They will be processed on their own.
                    var source = xTileset.GetAttributeAs<string>("source");
                    if (string.IsNullOrEmpty(source))
                    {
                        ProcessTileset(absoluteTsxPath, xTileset);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Looking for tilesets. Unknown error processing Tiled file '{absoluteTsxPath}': {e.Message}");
            }
        }

        private void ProcessTileset(string abspath, XElement xTileset)
        {
            // The tileset either has one <image> element or <tile> elements
            var xImage = xTileset.Element("image");
            var xTiles = xTileset.Elements("tile");
            if (xImage != null)
            {
                ProcessTilesetSingle(abspath, xTileset, xImage);
            }
            else
            {
                ProcessTilesetMultiple(abspath, xTileset, xTiles);
            }
        }

        private void ProcessTilesetSingle(string absolutePathTsx, XElement xTileset, XElement xImage)
        {
            var relativePathTexture = xImage.GetAttributeAs<string>("source");
            var absolutePathTexture = GetSanitizedAbsolutePathFromRelative(absolutePathTsx, relativePathTexture);

            var tileCount = xTileset.GetAttributeAs<int>("tilecount");
            var tileWidth = xTileset.GetAttributeAs<int>("tilewidth");
            var tileHeight = xTileset.GetAttributeAs<int>("tileheight");
            var columns = xTileset.GetAttributeAs<int>("columns");
            var spacing = xTileset.GetAttributeAs<int>("spacing", 0);
            var margin = xTileset.GetAttributeAs<int>("margin", 0);

            var imageHeight = xImage.GetAttributeAs<int>("height");

            for (int i = 0; i < tileCount; i++)
            {
                // Get grid x,y coords
                int x = i % columns;
                int y = i / columns;

                // Get x source on texture
                int srcx = x * tileWidth;
                srcx += x * spacing;
                srcx += margin;

                // Get y source on texture
                int srcy = y * tileHeight;
                srcy += y * spacing;
                srcy += margin;

                // In Tiled, texture origin is the top-left. However, in Unity the origin is bottom-left.
                srcy = (imageHeight - srcy) - tileHeight;

                if (srcy < 0)
                {
                    // This is an edge condition in Tiled if a tileset's texture has been resized
                    break;
                }

                var entry = new RectangleEntry
                {
                    AbsolutePathTsx = absolutePathTsx.ToLower(),
                    AbsolutePathTexture = absolutePathTexture.ToLower(),
                    X = srcx,
                    Y = srcy,
                    Width = tileWidth,
                    Height = tileHeight,
                };

                m_RectangleEntries.Add(entry);
            }
        }

        private void ProcessTilesetMultiple(string absolutePathTsx, XElement xTileset, IEnumerable<XElement> xTiles)
        {
            foreach (var xTile in xTiles)
            {
                var xImage = xTile.Element("image");
                if (xImage != null)
                {
                    var relativePathTexture = xImage.GetAttributeAs<string>("source");
                    var absolutePathTexture = GetSanitizedAbsolutePathFromRelative(absolutePathTsx, relativePathTexture);

                    int texture_w = xImage.GetAttributeAs<int>("width");
                    int texture_h = xImage.GetAttributeAs<int>("height");

                    int tile_x = xTile.GetAttributeAs<int>("x", 0);
                    int tile_y = xTile.GetAttributeAs<int>("y", 0);
                    int tile_w = xTile.GetAttributeAs<int>("width", texture_w);
                    int tile_h = xTile.GetAttributeAs<int>("height", texture_h);

                    var entry = new RectangleEntry
                    {
                        AbsolutePathTsx = absolutePathTsx.ToLower(),
                        AbsolutePathTexture = absolutePathTexture.ToLower(),
                        X = tile_x,
                        Y = tile_y,
                        Width = tile_w,
                        Height = tile_h,
                    };

                    m_RectangleEntries.Add(entry);
                }
            }
        }

        private void CheckForTextureReimports(string tiledAssetPath)
        {
            // Does the the tiled asset have any import errors?
            var importErrors = AssetDatabase.LoadAssetAtPath<ImportErrors>(tiledAssetPath);
            if (importErrors != null)
            {
                // Do not try to reimport textures if pixels per unit settings don't match
                // That requires user input
                if (!importErrors.m_WrongPixelsPerUnits.Any())
                {
                    // Do we have any errors of the missing sprite variety?
                    foreach (var missing in importErrors.m_MissingTileSprites)
                    {
                        // Only try to reimport these textures once
                        // Otherwise we may introduce a cyclic depenency chain
                        var textureAssetPath = missing.m_TextureAssetPath.ToLower();
                        if (!m_TextureAssetsReimported.Contains(textureAssetPath))
                        {
                            m_TextureAssetsReimported.Add(textureAssetPath);
                            AssetDatabase.ImportAsset(textureAssetPath);
                        }
                    }
                }
            }
        }

        private void ImportTiledFile(string path)
        {
            ProcessTiledFile(path);

            // Does the tiled file we've just imported require textures to have sprites in them?
            // We can't have cyclic dependencies between textures -> tilesets -> textures
            // But we can try a one-time reimport of textures here
            // This will help in cases where users imported their tiled files before their textures
            // Or they are upgrading from an older version of ST2U that created its own sprite atlases
            CheckForTextureReimports(path);
        }

        private void ImportImageFile(string path)
        {
        }

        private void DeleteTiledFile(string path)
        {
        }

        private void DeleteImageFile(string path)
        {
        }

        private void MoveTiledFile(string oldPath, string newPath)
        {
        }

        private void MoveImageFile(string oldPath, string newPath)
        {
        }

        // Seed the collection of sprite rectangles needed to make tiles
        // Further imports will keep this collection updated
        private static SpriteRectsManager CreateInstance()
        {
            var instance = new SpriteRectsManager();

            // Maps (tmx) and tilesets (tsx) may contain tiles and will instruct us on how a texture is to be cut up into sprites
            // We have to look by file extension and not by asset type because these files may not yet be imported
            var tmxFiles = Directory.EnumerateFiles(Application.dataPath, "*.tmx", SearchOption.AllDirectories).ToList();
            foreach (string file in tmxFiles)
            {
                var path = file.SanitizePath();
                instance.ProcessTiledFile(path);
            }

            var tsxFiles = Directory.EnumerateFiles(Application.dataPath, "*.tsx", SearchOption.AllDirectories).ToList();
            foreach (string file in tsxFiles)
            {
                var path = file.SanitizePath();
                instance.ProcessTiledFile(path);
            }

            return instance;
        }

        private struct RectangleEntry : IEquatable<RectangleEntry>
        {
            public string AbsolutePathTsx { get; set; }
            public string AbsolutePathTexture { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }

            public static bool operator ==(RectangleEntry lhs, RectangleEntry rhs) => lhs.Equals(rhs);
            public static bool operator !=(RectangleEntry lhs, RectangleEntry rhs) => !(lhs == rhs);

            public override bool Equals(object obj)
            {
                return obj is RectangleEntry other && this.Equals(other);
            }

            public override int GetHashCode()
            {
                return (AbsolutePathTsx, AbsolutePathTexture, X, Y, Width, Height).GetHashCode();
            }

            public bool Equals(RectangleEntry other)
            {
                return AbsolutePathTsx == other.AbsolutePathTsx &&
                    AbsolutePathTexture == other.AbsolutePathTexture &&
                    X == other.X &&
                    Y == other.Y &&
                    Width == other.Width &&
                    Height == other.Height;
            }
        }

        private class InternalAssetPostprocessor : AssetPostprocessor
        {
            private static readonly string[] TiledExtensions = new string[]
            {
                ".tmx",
                ".tsx",
            };

            private static readonly string[] ImageExtensions = new string[]
            {
                ".bmp",
                ".gif",
                ".jpeg",
                ".jpg",
                ".png",
                ".tga",
                ".tif",
                ".tiff",
            };

            private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
            {
                // Assets that were imported
                foreach (var imported in importedAssets)
                {
                    if (TiledExtensions.Any(x => imported.EndsWith(x)))
                    {
                        Instance.ImportTiledFile(imported);
                    }
                    else if (ImageExtensions.Any(x => imported.EndsWith(x)))
                    {
                        Instance.ImportImageFile(imported);
                    }
                }

                // Assets that were deleted
                foreach (var deleted in deletedAssets)
                {
                    if (TiledExtensions.Any(x => deleted.EndsWith(x)))
                    {
                        Instance.DeleteTiledFile(deleted);
                    }
                    else if (ImageExtensions.Any(x => deleted.EndsWith(x)))
                    {
                        Instance.DeleteImageFile(deleted);
                    }
                }

                // Assets that were moved or renamed
                for (int i = 0; i < movedAssets.Length; i++)
                {
                    string moved = movedAssets[i];
                    string movedFrom = movedFromAssetPaths[i];

                    if (TiledExtensions.Any(x => moved.EndsWith(x)))
                    {
                        Instance.MoveTiledFile(movedFrom, moved);
                    }
                    else if (ImageExtensions.Any(x => moved.EndsWith(x)))
                    {
                        Instance.MoveImageFile(movedFrom, moved);
                    }
                }
            }
        }
    }
}
