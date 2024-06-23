using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Next_Level : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            Scene_Controller.instance.NextLevel();
        }
    }
}
