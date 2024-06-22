using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public Animator _doorAnimator;
    public Animator _buttonAnimator;

    private bool isPlayerOnButton = false;

    private void Start()
    {
        // Assurez-vous que l'animation commence au bon état
        _buttonAnimator.Play("ButtonMove", 0, 0); // Jouer l'animation au début
        _buttonAnimator.speed = 0; // Stopper l'animation au début
        Debug.Log("Start : Animation initialisée à ButtonMove et stoppée.");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnButton = true;
            _buttonAnimator.speed = 1; // Jouer l'animation en avant
            _doorAnimator.SetBool("DoorUp", false);
            Debug.Log("OnCollisionEnter2D : Joueur sur le bouton, animation en avant.");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerOnButton = false;
            _buttonAnimator.speed = -1; // Jouer l'animation en arrière
            Debug.Log("OnCollisionExit2D : Joueur hors du bouton, animation en arrière.");
        }
    }

    private void Update()
    {
        AnimatorStateInfo stateInfo = _buttonAnimator.GetCurrentAnimatorStateInfo(0);

        // Stopper l'animation lorsqu'elle atteint les extrémités
        if (isPlayerOnButton && stateInfo.normalizedTime >= 1.0f && _buttonAnimator.speed > 0)
        {
            _buttonAnimator.speed = 0;
            Debug.Log("Update : Animation stoppée à la fin.");
        }
        else if (!isPlayerOnButton && stateInfo.normalizedTime <= 0.0f && _buttonAnimator.speed < 0)
        {
            _buttonAnimator.speed = 0;
            Debug.Log("Update : Animation stoppée au début.");
        }

        // Pour éviter les valeurs négatives de normalizedTime
        if (_buttonAnimator.speed < 0 && stateInfo.normalizedTime <= 0.0f)
        {
            _buttonAnimator.Play("ButtonMove", 0, 1.0f); // Rejouer l'animation à la fin
            _buttonAnimator.speed = -1;
            Debug.Log("Update : Rejouer l'animation à la fin.");
        }
    }
}
