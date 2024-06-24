using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_reload : MonoBehaviour
{
    public static Game_reload instance;
    private void Awake(){
        if(instance==null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

   void Update(){
        if(Input.GetKeyUp(KeyCode.F2)){
            SceneManager.LoadSceneAsync(0);
        }
    }
    // Start is called before the first frame updat
}
