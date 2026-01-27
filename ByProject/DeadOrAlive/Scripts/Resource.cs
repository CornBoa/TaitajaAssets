using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{
    public enum KinOfResource
    {
        Ammo,Health
    }
    [SerializeField]KinOfResource kinOfResource;
    [SerializeField] UnityEvent collect;
    Shooting gun;
    Movement movement;
    private void Start()
    {
        gun = FindFirstObjectByType<Shooting>();
        movement = FindFirstObjectByType<Movement>();

    }
    public void ColledRes()
    {
        switch (kinOfResource)
        {
            case KinOfResource.Health:
                movement.Heal(50);
                break;
            case KinOfResource.Ammo:
                gun.ammoOwned += 50;
                break;           
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player")) ColledRes();
    }
    public void SetRandomPos(Bounds bounds)
    {

        transform.position = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0
        );
    }
}
