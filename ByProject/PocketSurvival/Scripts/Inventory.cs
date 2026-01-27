using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public int maxItems = 9,takenSpace;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        takenSpace = transform.childCount;
    }
    void Update()
    {
        takenSpace = transform.childCount;
    }
}
