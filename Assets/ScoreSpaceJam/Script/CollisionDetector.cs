using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private bool isPlayerOnTop = false;
    private GameObject player;
    public float floatSpeed = 2f;
    public bool isFloating = false;
 private Rigidbody2D rb;

 public PlayerMovement script;
private bool assezballon;
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
        Debug.Log(script.ballonVert);
        if (isFloating){
            isFloating = false;
        }
       else {
            Debug.Log("marchennnnnn");
            isFloating = true;
        }
        }
    }




