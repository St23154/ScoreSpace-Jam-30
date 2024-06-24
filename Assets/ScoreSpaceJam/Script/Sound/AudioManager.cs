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
}
