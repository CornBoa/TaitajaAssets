using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    
    public enum ItemType
    {
        Food,
        Water,WaterLeftover,Coffee,leftovers,Money
    }
    public float restoreValue;  
    public ItemType itemType;
    public Sprite sanvich, yummySalad;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnUse);
        restoreValue = Mathf.Clamp(restoreValue, 10, 50);
        if (itemType == ItemType.Food)
        {
            if (restoreValue >= 25)
            {
                GetComponentInChildren<Image>().sprite = yummySalad;
            }
            else
            {
                GetComponentInChildren<Image>().sprite = sanvich;
            }
        }
    }
    void OnUse()
    {

        switch (itemType)
        {
            case ItemType.Food:
                ResourcesGa.instance.Eat(restoreValue);
                Destroy(gameObject);
                break;
                case ItemType.Water:
                ResourcesGa.instance.Drink(restoreValue);
                Destroy(gameObject);
                break;
                case ItemType.Coffee:
                ResourcesGa.instance.Rest(restoreValue);
                ResourcesGa.instance.Drink(restoreValue * 0.75f);
                Destroy(gameObject);
                break;
                case ItemType.leftovers:    
                ResourcesGa.instance.Eat(restoreValue * 0.25f);
                Destroy(gameObject);
                break;
        }
       
    }

}
