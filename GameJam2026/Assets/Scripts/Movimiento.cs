using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    //Guarda la direccion en la que se puede mover
    Vector3 movement;
    //Velocidad del jugador
    [SerializeField] float speed = 10;
    //Fuerza de salto del jugador
    [SerializeField] float jumpForce = 10;
    //La vida maxima del jugador
    [SerializeField] float maxHealth = 100;
    //Comprueba que se pueda mover el jugador
    bool isMoving = false;
    //Layer para detectar el suelo
    public LayerMask layerRay;

    //Boleano para comprobar que se pueda saltar
    bool isJumping = false;
    //float jumpHolding = 0;
    //float maxJumpHolding = 5;

    //
    Rigidbody2D rb;

    [SerializeField] Image healthbar;

    float health;
    bool lifeIsDecreasing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Se consigue el rigidbody del jugador
        rb = GetComponent<Rigidbody2D>();
        //La salud maxima para el jugador
        health = maxHealth;
    }
    //Funcion para acceder del rigidbody2D del jugador
    internal Rigidbody2D GetRigidBody()
    {
        return rb;
    }
    //Sirve para llamar al Player Input en el jugador y acceder al movimiento del jugador
    public void OnMove(InputAction.CallbackContext contextMovement)
    {
        //Si se ha llamado a la accion
        if (contextMovement.performed)
        {
            //Se lee en que direccion se ha movido y se le habilita mover al jugador
            movement = contextMovement.action.ReadValue<Vector2>();
            isMoving = true;
        }
        else if (contextMovement.canceled)
        {
            //Se deshabilita el movimiento cuando se cancela el input
            isMoving = false;
        }
    }//Sirve para llamar al Player Input en el jugador y acceder al salto del jugador
    public void OnJump(InputAction.CallbackContext contextJump)
    {
        //Si se ha llamado a la accion
        if (contextJump.performed)
        {
            //Se habilita el salto
            isJumping = true;
            //jumpHolding = Time.time;

        }
        else if (contextJump.canceled)
        {
            //Se deshabilita el salto y se hace que el rigidbody tenga una fuerza de 0
            isJumping = false;
            rb.linearVelocity = Vector2.zero;

        }
    }
    // Update is called once per frame
    void Update()
    {
        //Dibuja una linea hacia abajo para representar la linea
        Debug.DrawLine(this.transform.position, transform.position + Vector3.down, Color.blue);
        //Se comprueba si se puede mover 
        if (isMoving)
        {
            Vector3 nuevaPos = transform.position + movement * speed * Time.deltaTime;
            transform.position = nuevaPos;//Se mueve a una velocidad cada frame
        }
        //Se comprueba si se puede saltar
        if (isJumping)
        {
            ////if (Time.time - jumpHolding < maxJumpHolding)
            //{
            //    isJumping = false;
            //    rb.linearVelocity = Vector2.zero;
            //}
            //Se guarda lo que ha colisionado con el raycast
            RaycastHit2D p = Physics2D.Raycast(this.transform.position, Vector2.down, 0.6f, layerRay);
            //Si la distancia entre el punto de impacto y el objeto
            if (p.collider != null && p.distance <= 0.2f)
            {
                //La velocidad lineal del rigidbody se aplica para que se pueda mover en el salto
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                Debug.Log(p.distance);
            }
        }
        //Sirve para que decrezca la salud durante el tiempo
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
    //Permite recuperar la salud del jugador
    public void RecoverHealth()
    {
        health = maxHealth;
        healthbar.fillAmount = health / maxHealth;
    }
    //Decrece la salud del jugador
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
        //Comprueba si hay un componente de agua para realizar las funciones de agua
        if (collision.TryGetComponent<Water>(out Water recover))
        {
            Debug.Log("Detecte agua");
            RecoverHealth();
            recover.Deactivate();
        }
        //Comprueba si hay un componente de pelicano para realizar las funciones de pelicano
        else if (collision.TryGetComponent<TriggerForPelican>(out TriggerForPelican pelican))
        {
            pelican.enabled = true;
            pelican.PelicanSet(this);
        }
        //Comprueba si hay un componente de liana para realizar las funciones de liana
        else if (collision.TryGetComponent<Liana>(out Liana liana))
        {
            liana.enabled = true;
            liana.SetPlayer(this);
        }
        if (collision.gameObject.tag == "Matarjugador")
        {
            health = 0;
            Debug.Log(" aqui muere el jugador"); //AQUI SE ENLAZARÍA A LA ANIMACIÓN Y PENDIENTE DE ESPECIFICAR LA MUERTE DEL JUGADOR CON CADA TRIGGER
        }
        if (collision.gameObject.tag == "Reducirvida")
        {
            health =health - 10;
            Debug.Log("los pajaros dañan al jugador"); //Aquí recibe 10 de daño por cada pajaro que le golpea
        }
    }
}
