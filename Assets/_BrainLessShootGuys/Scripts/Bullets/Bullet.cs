using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject origin;
    public LayerMask layerCanTouch;
    public Rigidbody rb;
    public WeaponType weaponType;

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject != origin && (layerCanTouch.value & (1 << collision.transform.gameObject.layer)) > 0) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                OnTouch(collision);

            Destroy(gameObject);
        }
    }

    public void Start()
    {
        Destroy(gameObject, 5f);
    }

    public virtual void OnTouch(Collider collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        player.Dammage(weaponType.damage, origin);

        //gameObject.SetActive(false);
    }
}
