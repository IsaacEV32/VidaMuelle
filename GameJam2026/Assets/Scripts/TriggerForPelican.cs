using UnityEngine;

public class TriggerForPelican : MonoBehaviour
{
    [SerializeField] Transform pelican;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void PelicanSet(Movimiento player)
    {
        player.transform.position = pelican.position + new Vector3(0, 1, 0);
        player.transform.parent = pelican.transform;
        player.GetRigidBody().bodyType = RigidbodyType2D.Kinematic;
        player.GetRigidBody().linearVelocity = Vector3.zero;
        player.enabled = false;
        PelicanControl p = pelican.gameObject.GetComponent<PelicanControl>();
        p.enabled = true;

    }
    public void PlayerSet(Movimiento player)
    {
        player.transform.parent = null;
        player.GetRigidBody().bodyType = RigidbodyType2D.Dynamic;
        player.enabled = true;
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
