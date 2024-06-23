using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChrono : MonoBehaviour
{
    private Minuteur minuteur;
    // Start is called before the first frame update
    void Start()
    {
        minuteur = GameObject.FindWithTag("GameManager").GetComponent<Minuteur>();
        minuteur.stopTimer();
    }
}
