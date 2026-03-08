using UnityEngine;
using UnityEngine.InputSystem;

public class Liana : MonoBehaviour
{
    [SerializeField] Transform liana;
    [SerializeField] Movimiento player;
    [SerializeField] float maxRotationDuration = 3;
    [SerializeField] float forceImpulse = 1000;
    bool left = false;
    float currentRotation;
    Vector3 RotationMax = new Vector3(0, 0, 45);
    Vector3 RotationMin = new Vector3(0, 0, -45);
    PlayerInput input;

    private void Awake()
    {
        input = GetComponentInChildren<PlayerInput>();
        input.enabled = false;
    }
    public void OnLiana(InputAction.CallbackContext contextLiana)
    {
        if (contextLiana.performed && !left)
        {
            input.enabled = false;
            player.transform.parent = null;
            player.GetRigidBody().bodyType = RigidbodyType2D.Dynamic;
            player.GetRigidBody().AddForce(Vector2.right * forceImpulse, ForceMode2D.Impulse);
            player.transform.rotation = Quaternion.identity;
            player.enabled = true;
        }
        else if(contextLiana.performed && left)
        {
            input.enabled = false;
            player.transform.parent = null;
            player.GetRigidBody().bodyType = RigidbodyType2D.Dynamic;
            player.GetRigidBody().AddForce(Vector2.left * forceImpulse, ForceMode2D.Impulse);
            player.transform.rotation = Quaternion.identity;
            player.enabled = true;
        }
    }
    internal void SetPlayer(Movimiento _player)
    {
        player = _player;
        player.transform.parent = liana.transform;
        input.enabled = true;
        player.GetRigidBody().bodyType = RigidbodyType2D.Kinematic;
        player.GetRigidBody().linearVelocity = Vector3.zero;
        player.enabled = false;
    }
    void Update()
    {
        currentRotation += Time.deltaTime;
        float t = currentRotation / maxRotationDuration;
        if (!left)
        {
            Debug.Log("Estoy rotando hacia la derecha");
            liana.rotation = Quaternion.Slerp(liana.rotation, Quaternion.Euler(RotationMax), t);
            if (liana.rotation.z >= Quaternion.Euler(RotationMax).z)
            {
                currentRotation = 0;
                Debug.Log("Cambio rotacion izquierda");
                left = true;
            }
        }
        else 
        {
            Debug.Log("Estoy rotando hacia la izquierda");
            liana.rotation = Quaternion.Slerp(liana.rotation, Quaternion.Euler(RotationMin), t);
            if (liana.rotation.z <= Quaternion.Euler(RotationMin).z)
            {
                currentRotation = 0;
                Debug.Log("Cambio rotacion derecha");
                left = false;
            }
        }
    }
}
