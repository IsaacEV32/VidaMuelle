using UnityEngine;
using UnityEngine.InputSystem;

public class PelicanControl : MonoBehaviour
{
    Vector3 movement;
    bool isMoving = false;
    [SerializeField] float speed = 3f;
    [SerializeField]Movimiento playerRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    //Controlas la direccion en la que mueves el pelicano
    public void OnMovePelican(InputAction.CallbackContext contextPelican)
    {
        //Si se utiliza el control del pelicano 
        if (contextPelican.performed)
        {
            //Se lee el vector de movimiento y se activa el movimiento
            movement = contextPelican.ReadValue<Vector2>().normalized;
            isMoving = true;
        }
        else if (contextPelican.canceled)
        {
            //Se desactiva el movimiento
            isMoving = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Se activa  el movimiento y se mueve hacia delante automáticamente siempre con el pelicano
        Vector3 nuevaPos = transform.position;

        if (isMoving)
        {
            //Se movera dependiendo del input dado
            nuevaPos += movement * speed * Time.deltaTime;
            nuevaPos += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            nuevaPos += Vector3.right * speed * Time.deltaTime;
        }


        nuevaPos.y = Mathf.Clamp(nuevaPos.y, -1.1f, 6f);

        transform.position = nuevaPos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si llega al trigger final se desactivará el script del pelicano
        if(collision.TryGetComponent<TriggerForPelican>(out TriggerForPelican tP))
        {
            Movimiento player = this.GetComponentInChildren<Movimiento>();
            Debug.Log(player);
            tP.PlayerSet(player);
            this.enabled = false;
        }
    }
}
