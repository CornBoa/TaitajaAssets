using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CustomerStuff : MonoBehaviour
{
    public enum TastePref
    {
        Salty,Sweet,Bitter,Sour,PourBalance
    }
    public float salty, sweet, bitter, soury;
    public TextMeshProUGUI orderText,orderRemember;
    public TastePref tastepreferance;
    public GameObject ordersmolbubble;
    public UnityEvent onSomethingSOmething; // Say hello to Olli
    string add;
    private void Start()
    {
        
    }
    public void RandomOrder()
    {
        onSomethingSOmething.Invoke();
        ordersmolbubble.SetActive(true);
        tastepreferance = (TastePref)Random.Range(0, 3);
        switch (tastepreferance)
        {
            case TastePref.Sweet:
                orderText.text = "I would like something sweet"; sweet = Random.Range(10, 20);
                add = "(sweetness of " + sweet + ")";
                
                break;
            case TastePref.Bitter:
                orderText.text = "I would like something bitter"; bitter = Random.Range(10, 20);
                add = "(bitterness of " + bitter + ")";
                
                break;
            case TastePref.Sour:
                orderText.text = "I would like something sour";soury = Random.Range(10, 20);
                add = "(sourness of " + soury + ")";
                
                break;
            case TastePref.Salty:
                orderText.text = "I would like something salty"; salty = Random.Range(10, 20);
                add = "(saltyness of " + salty + ")";
               
                break;
        }
        orderRemember.text = orderText.text+add ;
    }
}
