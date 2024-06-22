using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOve_object : MonoBehaviour
{



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("moveable_object"))
        {
            Debug.Log("Touched");
        }
    }
}


    
