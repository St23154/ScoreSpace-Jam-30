using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChrono : MonoBehaviour
{
    private Minuteur minuteur;
    AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
		_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        _audioManager.PlaySFX(_audioManager.victory);
        minuteur = GameObject.FindWithTag("GameManager").GetComponent<Minuteur>();
        // minuteur.stopTimer();
        minuteur.stopTimer();
    }
}
