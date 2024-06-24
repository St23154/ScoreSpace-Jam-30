using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header(" - - - - - - Audio Source - - - - - -")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource WalkSource;

    [Header(" - - - - - - Audio Clip - - - - - -")]
    public AudioClip background;
    public AudioClip balloon;
    public AudioClip deathstinger;
    public AudioClip respawn;
    public AudioClip victory;
    public AudioClip footStep;
    public AudioClip jump;

    public static AudioManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayWalk(AudioClip clip)
    {
        WalkSource.PlayOneShot(clip);
    }

    public void StopWalk(AudioClip clip)
    {
        if (IsPlaying(clip))
        {
        WalkSource.Stop();
        }
    }

    public bool IsPlaying(AudioClip clip)
    {
        // Check if the clip is being played in the SFX source
        if (WalkSource.isPlaying)
        {
            return true;
        }

        // If the clip is not being played in any source
        return false;
    }
}
