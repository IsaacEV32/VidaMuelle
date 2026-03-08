using UnityEngine;

public class Liana : MonoBehaviour
{
    [SerializeField] Transform liana;
    [SerializeField] float maxRotationDuration = 3;
    bool left = false;
    float currentRotation;
    Vector3 RotationMax = new Vector3(0, 0, 45);
    Vector3 RotationMin = new Vector3(0, 0, -45);

    // Update is called once per frame
    void Update()
    {
        currentRotation += Time.deltaTime;
        float t = currentRotation / maxRotationDuration;
        if (!left)
        {
            Debug.Log("Estoy rotando hacia la derecha");
            transform.rotation = Quaternion.Slerp(liana.rotation, Quaternion.Euler(RotationMax), t);
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
            transform.rotation = Quaternion.Slerp(liana.rotation, Quaternion.Euler(RotationMin), t);
            if (liana.rotation.z <= Quaternion.Euler(RotationMin).z)
            {
                currentRotation = 0;
                Debug.Log("Cambio rotacion derecha");
                left = false;
            }
        }
    }
}
