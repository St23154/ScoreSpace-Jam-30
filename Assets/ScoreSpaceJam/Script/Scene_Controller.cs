using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Controller : MonoBehaviour
{
    public static Scene_Controller instance;
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
        if(Input.GetKeyUp(KeyCode.R)){
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
    // Start is called before the first frame update
    public void NextLevel(){
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex +1 );
    }
}
