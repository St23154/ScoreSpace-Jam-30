using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private bool isPlayerOnTop = false;
    private GameObject player;
    public float floatSpeed = 2f;
    private bool isFloating = false;
 private Rigidbody2D rb;

 public PlayerMovement script;

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
        if (isFloating && script.ballonVert)
        {
            rb.velocity = new Vector2(rb.velocity.x, floatSpeed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (isPlayerOnTop && Input.GetKeyDown(KeyCode.E))
        {
            isFloating = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("1");
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("moveable_object"))
        {
            isPlayerOnTop = true;
            player = collision.gameObject;
            Debug.Log("Touched");
        }

        if(collision.gameObject.tag == "ground") {
            Debug.Log("Toucheqefzed");
            isFloating = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            isPlayerOnTop = false;
            player = null;
        }
    }



}
