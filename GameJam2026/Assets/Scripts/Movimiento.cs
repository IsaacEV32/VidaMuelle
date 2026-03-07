using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    Vector3 movement;
    [SerializeField] float speed = 10;
    [SerializeField]float jumpForce = 10;
    [SerializeField]float maxHealth = 100;
    bool isMoving = false;
    public LayerMask layerRay;
    bool isJumping = false;
    float jumpHolding = 0;
    float maxJumpHolding = 5;
    Rigidbody2D rb;

    [SerializeField] Image healthbar;

    float health;
    bool lifeIsDecreasing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }
    internal Rigidbody2D GetRigidBody()
    {
        return rb;
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
           
            
                isJumping = false;
                rb.linearVelocity = Vector2.zero;
            
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
            ////if (Time.time - jumpHolding < maxJumpHolding)
            //{
            //    isJumping = false;
            //    rb.linearVelocity = Vector2.zero;
            //}
            RaycastHit2D p = Physics2D.Raycast(this.transform.position, Vector2.down, 0.6f, layerRay);
            if (p.collider != null && p.distance <= 0.2f)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                Debug.Log(p.distance);
            }
        }
        if (!lifeIsDecreasing)
        {
            StartCoroutine(DecreaseLife());
        }
        //CheatCode for recover health
        if (Input.GetKeyDown(KeyCode.B))
        {
            health = maxHealth;
            healthbar.fillAmount = health / maxHealth;
        }
    }
    internal void RecoverHealth()
    {
        health = maxHealth;
        healthbar.fillAmount = health/maxHealth; 
    }
    IEnumerator DecreaseLife()
    {
        lifeIsDecreasing = true;
        health -= 2;
        healthbar.fillAmount = health / maxHealth;
        yield return new WaitForSeconds(0.5f);
        lifeIsDecreasing = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Water>(out Water recover))
        {
            Debug.Log("Detecte agua");
            RecoverHealth();
            recover.Deactivate();
        }
        else if (collision.TryGetComponent<TriggerForPelican>(out TriggerForPelican pelican))
        {
            pelican.enabled = true;
            pelican.PelicanSet(this);
        }
        if (collision.gameObject.tag == "Matarjugador")
        {
            Debug.Log(" aqui muere el jugador"); //AQUI SE ENLAZARÍA A LA ANIMACIÓN Y PENDIENTE DE ESPECIFICAR LA MUERTE DEL JUGADOR CON CADA TRIGGER
        }
    }
}
