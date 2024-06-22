using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    public bool isInRange;
    public KeyCode InteractKey;
    public bool isInteracting = false;
    public UnityEvent interactAction;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("lance tou");
        isInRange = false;
        Debug.Log(isInRange);
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange){
            if (Input.GetKeyDown(InteractKey))
        {
            if (!isInteracting)
            {
                isInteracting = true;
                Debug.Log("Interact key pressed.");
                // Perform your interact action here
                interactAction.Invoke();
            }
        }
        else if (Input.GetKeyUp(InteractKey))
        {
            isInteracting = false;
        }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            isInRange = true;
            Debug.Log("dedans");
        }
    }



    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            isInRange = false;
            Debug.Log("dehors");
        }
    }
}
