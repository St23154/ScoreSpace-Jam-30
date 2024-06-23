using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_platform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("moveable_object"))
        {
            Debug.Log("zert");
            other.transform.parent = transform;
    
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("moveable_object"))
        {
            other.transform.parent = null;
            
        }
    }
}
