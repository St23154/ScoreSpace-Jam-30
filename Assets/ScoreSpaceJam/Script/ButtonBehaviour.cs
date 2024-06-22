using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public Animator _doorAnimator;
    public Animator _buttonAnimator;
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
        Debug.Log("aie");
        if (other.gameObject.CompareTag("Player"))
        {
            _buttonAnimator.SetBool("ButtonUp", false);
            _doorAnimator.SetBool("DoorUp", false);
        }
        else
        {
            _buttonAnimator.SetBool("ButtonUp", true);
            _doorAnimator.SetBool("DoorUp", true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _buttonAnimator.SetBool("ButtonUp", true);
            _doorAnimator.SetBool("DoorUp", true);
        }
    }
}
