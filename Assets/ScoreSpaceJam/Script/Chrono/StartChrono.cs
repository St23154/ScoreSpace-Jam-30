using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChrono : MonoBehaviour
{
    private Minuteur minuteur;
    // Start is called before the first frame update
    void Start()
    {
        Minuteur._time = 0;
        minuteur = GameObject.FindWithTag("GameManager").GetComponent<Minuteur>();
        minuteur.startTimer();
    }
}
