using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject _door;
    private float speed = 0f;    // Vitesse de déplacement

    // Start is called before the first frame update

    void Update()
    {

        // Calculer la nouvelle position Y
        float newY = transform.localPosition.y + speed * Time.deltaTime;
        float newY_01;

        // S'assurer que l'objet ne dépasse pas la position cible
        if (newY < -0.5)
        {
            newY_01 = _door.transform.localPosition.y + speed * 3 * Time.deltaTime;
            if (newY_01 < -4.05)
            {
                newY_01 = -4.05f;
            }
            newY = -0.5f;
        }
        else
        {
            if ( newY > 0)
            {
            newY = 0;
            }
            newY_01 = _door.transform.localPosition.y + Mathf.Abs(speed * 3 * Time.deltaTime);
            if (newY_01 > 0)
            {
                newY_01 = 0;
            }
        }

        // Appliquer la nouvelle position
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        _door.transform.localPosition = new Vector3(_door.transform.localPosition.x, newY_01, _door.transform.localPosition.z);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")   || other.gameObject.CompareTag("moveable_object"))
        {
            Debug.Log("collision avec le bouton");
            speed = -0.4f;

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("moveable_object"))
        {
            Debug.Log("sortie bouton");
            speed = 0.4f;

        }
    }
}