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
    
    public void OnMovePelican(InputAction.CallbackContext contextPelican)
    {
        if (contextPelican.performed)
        {
            movement = contextPelican.ReadValue<Vector2>().normalized;
            isMoving = true;
        }
        else if (contextPelican.canceled)
        {
            isMoving = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            this.transform.position += movement * speed * Time.deltaTime;
            this.transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else 
        {
            this.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<TriggerForPelican>(out TriggerForPelican tP))
        {
            Movimiento player = this.GetComponentInChildren<Movimiento>();
            Debug.Log(player);
            tP.PlayerSet(player);
            this.enabled = false;
        }
    }
}
