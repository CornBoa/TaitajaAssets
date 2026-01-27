using UnityEngine;
using UnityEngine.Events;

public class DropLocation : MonoBehaviour
{
    public static DropLocation Instance;
    [SerializeField]LayerMask layerMask;
    CardBehaviour card;
    public UnityEvent Sound;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<CardBehaviour>() != null)
        {
            card = other.GetComponent<CardBehaviour>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<CardBehaviour>() != null)
        {
            card = null;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (card != null&& !card.draging && !card.inHand)
        {
            CheckCards();
        }
    }

    public void CheckCards()
    {
        Sound.Invoke();
        GameManager.instance.BasicCardUse(card);
        if(card != null)card.gameObject.SetActive(false);         
    }
}
