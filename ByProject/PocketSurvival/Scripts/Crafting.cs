using UnityEngine;

public class Crafting : MonoBehaviour
{
    public int needRes;
    public InventoryItem ins;
    public InventoryItem.ItemType item;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (ResourcesGa.instance.Cook(needRes,item))
        {
            Instantiate(ins, GameObject.Find("Inventory").transform);
        }
        else 
        {
            Debug.Log("Not enough leftovers to cook.");
        }
    }
}
