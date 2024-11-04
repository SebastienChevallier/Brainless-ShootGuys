using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject origin;
    public LayerMask layerCanTouch;
    public Rigidbody rb;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != origin && collision.gameObject.layer == layerCanTouch) {
            OnTouch();
        }
    }

    public virtual void OnTouch()
    {
        //Je touche
    }
}
