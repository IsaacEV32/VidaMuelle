using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;


    private void Start()
    {
        rb.freezeRotation = true;
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float horizontal = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = 1f;
            transform.localScale = new Vector2(1, 1);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -1f;
            transform.localScale = new Vector2(-1, 1);
        }

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        // Solo permite saltar si la velocidad vertical es casi cero (está en el suelo)
        bool enSuelo = Mathf.Abs(rb.linearVelocity.y) < 0.01f;

        if (enSuelo && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    }

