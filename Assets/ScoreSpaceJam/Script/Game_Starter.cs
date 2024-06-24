using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Starter : MonoBehaviour
{
    // Start is called before the first frame update
public void PlayGame(){
    SceneManager.LoadSceneAsync("Level_0");
}


public void Credits(){
    SceneManager.LoadSceneAsync("Credits");
}

public void Back(){
    SceneManager.LoadSceneAsync("Menu");
}
}
