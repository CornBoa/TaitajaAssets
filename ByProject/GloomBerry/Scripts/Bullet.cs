using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dmg;
    void Start()
    {
        Transform target = FindFirstObjectByType<PlayerMovement>().transform;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position);
    }
    void Update()
    {
        transform.position += transform.up * 10f * Time.deltaTime;
        if(Vector3.Distance(transform.position, FindFirstObjectByType<PlayerMovement>().transform.position) < 0.5f)
        {
            FindFirstObjectByType<PlayerMovement>().TakeDamage(1);
            Destroy(gameObject);
        }   
    }
}
