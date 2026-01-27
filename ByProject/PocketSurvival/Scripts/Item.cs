using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject InvItem;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && Inventory.instance.takenSpace < Inventory.instance.maxItems)
        {
            Instantiate(InvItem, GameObject.Find("Inventory").transform.position, Quaternion.identity, GameObject.Find("Inventory").transform);
            Destroy(gameObject);
        }
    }
}
