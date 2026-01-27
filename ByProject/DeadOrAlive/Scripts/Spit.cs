using System.Collections;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Spit : MonoBehaviour
{
    [SerializeField] float DMG, timeToDie, speed;
    [SerializeField] GameObject puddlePrefab;
    GameObject player;
    void Start()
    {     
        Vector3 diff = FindFirstObjectByType<Movement>().transform.position- transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        StartCoroutine(Death());
    }
    void Update()
    {
        transform.position +=transform.up * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            Impact();
        }

    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(timeToDie);
        Impact();
    }
    void Impact()
    {
        if(player != null)FindFirstObjectByType<Movement>().TakeDmg(DMG);
        Instantiate(puddlePrefab,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
