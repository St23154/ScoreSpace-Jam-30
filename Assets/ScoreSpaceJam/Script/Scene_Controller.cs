using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Controller : MonoBehaviour
{
    private Animator _startingSceneTransition;
    private Animator _endingSceneTransition;
    public static Scene_Controller instance;
    private void Awake(){
        instance = this;
    }

    private void Start()
    {
        _startingSceneTransition = GameObject.FindWithTag("startscene").GetComponent<Animator>();
        _endingSceneTransition = GameObject.FindWithTag("endscene").GetComponent<Animator>();
        _endingSceneTransition.SetTrigger("Transition");
    }


   void Update(){
        if(Input.GetKeyUp(KeyCode.R)){
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
    // Start is called before the first frame update
    public void NextLevel(){
        _startingSceneTransition.SetTrigger("Transition");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex +1 );
    }
}
