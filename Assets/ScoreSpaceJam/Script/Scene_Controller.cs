using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Controller : MonoBehaviour
{
    [SerializeField] private GameObject _startingSceneTransition;
    [SerializeField] private GameObject _endingSceneTransition;
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

    private void Start()
    {
        _startingSceneTransition = GameObject.FindWithTag("startscene").GetComponent<GameObject>();
        _endingSceneTransition = GameObject.FindWithTag("endscene").GetComponent<GameObject>();
        _endingSceneTransition.SetActive(true);
    }


   void Update(){
        if(Input.GetKeyUp(KeyCode.R)){
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
    // Start is called before the first frame update
    public void NextLevel(){
        _startingSceneTransition.SetActive(true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex +1 );
    }
}
