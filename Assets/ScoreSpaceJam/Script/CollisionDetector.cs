using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private bool isPlayerOnTop = false;
    private GameObject player;
    public float floatSpeed = 3f;
    public bool isFloating = false;
 private Rigidbody2D rb;

 public GameObject BoiteBallon;
 public GameObject Boite;
void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D component found on the object.");
        }
    }
    void Update()
    {
        if (isFloating) //  && script.ballonVert
        {
            rb.velocity = new Vector2(rb.velocity.x, floatSpeed);
        }

    }

    public void IsFloating(){
        if (isFloating){
            isFloating = false;
            BoiteBallon.SetActive(false);
            Boite.SetActive(true);
        }
       else {
            Debug.Log("marchennnnnn");
            isFloating = true;
            BoiteBallon.SetActive(true);
            Boite.SetActive(false);

        }
        }
    }




