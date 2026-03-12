using Unity.VisualScripting;
using UnityEngine;

public class Tp : MonoBehaviour
{
    public Transform destino;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position=destino.position;
        }
    }
}
