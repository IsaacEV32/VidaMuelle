using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    Vector3 movement;
    [SerializeField] float speed = 2;
    bool isMoving = false;

    bool isJumping = false;
    float jumpForce = 10;
    float jumpHolding = 0;
    float maxJumpHolding = 5;
    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnMove(InputAction.CallbackContext contextMovement)
    {
        if (contextMovement.performed)
        {
            movement = contextMovement.action.ReadValue<Vector2>();
            isMoving = true;
        }
        else if (contextMovement.canceled)
        {
            isMoving = false;
        }
    }
    public void OnJump(InputAction.CallbackContext contextJump)
    {
        if (contextJump.performed)
        {
            isJumping = true;
            jumpHolding = Time.time;

        }
        else if (contextJump.canceled)
        {
            if (Time.time - jumpHolding < maxJumpHolding)
            {
                isJumping = false;
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(this.transform.position, transform.position + Vector3.down, Color.blue);
        if (isMoving)
        {
            transform.position += movement * speed * Time.deltaTime;
        }
        if (isJumping)
        {
            RaycastHit2D p = Physics2D.Raycast(this.transform.position, Vector2.down, 0.6f, 1 << 3);
            if (p.distance >= 0.1f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            Debug.Log("Entre en salto");

        }
    }

}
