using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header(" - - - - - - Audio Source - - - - - -")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header(" - - - - - - Audio Clip - - - - - -")]
    public AudioClip background;
    public AudioClip balloon;
    public AudioClip deathstinger;
    public AudioClip respawn;
    public AudioClip victory;
    
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
