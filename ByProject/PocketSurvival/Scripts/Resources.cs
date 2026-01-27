using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesGa : MonoBehaviour
{
    public static ResourcesGa instance;
    public int knowledge;
    public float tiredness,hunger,thirst;
    public float tirdnessRate,hungerRate,thirstRate;    
    public Slider tiredMeter,hungerMeter,thirstMeter;
    public TextMeshProUGUI knowledgeText;
    public bool lost = false;
    private void Awake()
    {
        if (instance == null)
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
        tiredMeter.maxValue = 100;
        hungerMeter.maxValue = 100;
        thirstMeter.maxValue = 100;
        tiredMeter.value = tiredness;
        hungerMeter.value = hunger;
        thirstMeter.value = thirst;
    }
    public void Study(int i)
    {
        knowledge += i;
        knowledgeText.text = "Knowledge: " + knowledge;
    }
    void Update()
    {
        tiredness -= tirdnessRate * Time.deltaTime;
        if (hunger <= 0)
        {
            hunger = 0;
            tiredness -= tirdnessRate * Time.deltaTime * 0.5f;
        }
        if (thirst <= 0)
        {
            thirst = 0;
            tiredness -= tirdnessRate * Time.deltaTime * 0.5f;
        }
        if (tiredness < 0)
        {
            tiredness = 0;
            FellAsleep();   
            lost = true;
        }
        hunger -= hungerRate * Time.deltaTime;
        thirst -= thirstRate * Time.deltaTime;
        tiredMeter.value = tiredness;
        hungerMeter.value = hunger;
        thirstMeter.value = thirst;
        knowledgeText.text = "Knowledge: " + knowledge;
    }
    void FellAsleep()
    {
        FindFirstObjectByType<FadeInOut>().FadeOut();
    }
    public bool Cook(int left, InventoryItem.ItemType type)
    {
        int lefOvrs = 0;
        foreach(Transform child in GameObject.Find("Inventory").transform)
        {
            InventoryItem item = child.GetComponent<InventoryItem>();
            if(item.itemType == type)
            {
                lefOvrs++;
            }
        }
        if(lefOvrs >= left)
        {
            int used = 0;
            for(int i = GameObject.Find("Inventory").transform.childCount - 1; i >= 0; i--)
            {
                Transform child = GameObject.Find("Inventory").transform.GetChild(i);
                InventoryItem item = child.GetComponent<InventoryItem>();
                if(item.itemType == type && used < left)
                {
                    Destroy(child.gameObject);
                    used++;
                }
            }
            return true;
        }
        return false;
    }
    public void Eat(float amount)
    {
        hunger += amount;
        if(hunger > 100)
        {
            hunger = 100;
        }
        hungerMeter.value = hunger;
    }
    public void Drink(float amount)
    {
        thirst += amount;
        if(thirst > 100)
        {
            thirst = 100;
        }
        thirstMeter.value = thirst;
    }
    public void Rest(float amount)
    {
        tiredness += amount;
        if(tiredness > 100)
        {
            tiredness = 100;
        }
        tiredMeter.value = tiredness;
    }
}
