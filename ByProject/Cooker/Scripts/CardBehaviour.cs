using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CardBehaviour : MonoBehaviour
{
    public enum CardCategory
    {
        Ingredient,Flavor,Equipment,Technique
    }
    //Stat stuff
    public float saltyMult, sweetMult, bitterMult, sourMult;
    public float addSalt, addSweet, addSour, addBitter;
    public Vector3 desiredScale, ogSCale,ogPos;
    public TextMeshProUGUI descriptionStats;
    //Main
    public bool draging, inHand,lerping,lerpScaleUp,lerpScaleDown;
    [SerializeField]float lerpTimer,scaleLerpTimer,scaleSPeed,moveSpeed;
    public Transform lerpTarget;
    public UnityEvent onUse, onGrab, onGrab2;
    public CardCategory category;
    BoxCollider2D coll2D;
    
    void Start()
    {
        
        ogSCale = transform.localScale;
        coll2D = GetComponent<BoxCollider2D>();
        descriptionStats = GetComponentsInChildren<TextMeshProUGUI>()[1];
        descriptionStats.text = "";
        descriptionStats.paragraphSpacing = -30f;
        if (saltyMult > 1 || saltyMult < 1) descriptionStats.text += "Saltyness:" + saltyMult + "\n"; 
        if(sweetMult > 1 || sweetMult < 1)descriptionStats.text += "Sweetness:" + sweetMult + "\n";
        if (bitterMult > 1 || bitterMult < 1) descriptionStats.text += "Bitterness:" + bitterMult + "\n";
        if (sourMult > 1 || sourMult < 1) descriptionStats.text += "Sourness:" + sourMult + "\n";
        if (addSalt > 0 || addSalt < 0) descriptionStats.text += "Saltyness:" + addSalt + "\n";
        if (addSweet > 0 || addSweet < 0) descriptionStats.text += "Sweetness:" + addSweet + "\n";
        if (addSour > 0 || addSour < 0) descriptionStats.text += "Bitterness:" + addBitter + "\n";
        if (addBitter > 0 || addBitter < 0) descriptionStats.text += "Sourness:" + addSour + "\n";
    }
    void Update()
    {
        if (lerping && Vector3.Distance(transform.position, lerpTarget.position) > 0.01)
        {
            lerpTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position,lerpTarget.position,lerpTimer * moveSpeed);
        }
        else if (lerping)
        {
            ogPos = transform.localPosition;
            lerping = false;
            inHand = true;
            lerpTimer = 0;
        }
        if (lerpScaleUp && !CompareScale())
        {
            scaleLerpTimer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, scaleLerpTimer * scaleSPeed);
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(ogPos.x,ogPos.y +2,ogPos.z), scaleLerpTimer);
        }
        else if (lerpScaleUp)
        {
            lerpScaleUp = false;
            scaleLerpTimer= 0;
        }
        else if (lerpScaleDown && !lerpScaleUp && transform.localScale.x > ogSCale.x)
        {
            coll2D.enabled = false;
            scaleLerpTimer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, ogSCale, scaleLerpTimer);
            transform.localPosition = Vector3.Lerp(transform.localPosition,ogPos, scaleLerpTimer);
        }
        else if (lerpScaleDown && !lerpScaleUp)
        {
            coll2D.enabled = true;
            lerpScaleDown = false;
            scaleLerpTimer = 0;
        }
    }
    bool CompareScale()
    {
        if (transform.localScale.x < desiredScale.x && transform.localScale.z < desiredScale.z && transform.localScale.z < desiredScale.z)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void OnMouseEnter()
    {
        lerpScaleUp = true;
        lerpScaleDown = false;
    }
    private void OnMouseExit()
    {
        lerpScaleUp = false;
        lerpScaleDown = true;
    }
    private void OnMouseDown()
    {
        if (transform.parent != null) Hand.instance.CardOut(transform.parent,this);
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject.FindGameObjectWithTag("Anchor").transform.position = newPos;
        transform.parent = GameObject.FindGameObjectWithTag("Anchor").transform;
        draging = true;
        inHand = false;
        onGrab.Invoke();
    }
    private void OnMouseDrag()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.parent.position = new Vector3(newPos.x, newPos.y, 0);
    }
    private void OnMouseUp()
    {
        draging = false;
        transform.parent = null;
        Hand.instance.PutCardIn(this);
        onGrab.Invoke();
    }
    public void LerpTo(Transform slot)
    {
        onGrab2.Invoke();
        transform.parent = slot;
        lerpTarget = slot;
        lerping = true;
    }
    private void OnDisable()
    {       
        ProperDisableCard();
    }
    public void ProperDisableCard()
    {
        Hand.instance.CardOut(transform.parent, this);
        //if(transform.parent.gameObject.activeSelf)transform.SetParent(transform);
        if(gameObject.activeSelf)gameObject.SetActive(false);      
    }
}
