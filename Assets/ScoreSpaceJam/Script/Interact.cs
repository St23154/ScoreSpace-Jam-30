using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    private bool isInRange;
    public KeyCode InteractKey;
    public GameObject _text;
    private bool isInteracting = false;
    private PlayerMovement _playerCode;
    private CollisionDetector _parentScript;

    // Start is called before the first frame update
    void Start()
    {
        isInRange = false;
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
                    _parentScript = transform.parent.GetComponent<CollisionDetector>();
                    _playerCode.Check_Balloon(_parentScript);
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
            _playerCode = collision.gameObject.GetComponent<PlayerMovement>();
            if (_playerCode._BalloonsDictionary["Green"] >= 1 || _parentScript.isFloating == true)
            {
                _text.SetActive(true);
            }
        }
    }



    private void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            _text.SetActive(false);
            isInRange = false;
        }
    }
}
