using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    Vector3 movement;
    [SerializeField]float speed = 2;
    CharacterController characterController;
    bool isMoving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
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
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            characterController.Move(movement * speed * Time.deltaTime);
        }
    }
}
