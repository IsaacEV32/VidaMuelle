using UnityEngine;

public class tppelican : MonoBehaviour
{
        public Transform destino;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "pelican")
            {
                col.gameObject.transform.position = destino.position;
            }
        }
    }
