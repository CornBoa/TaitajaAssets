using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelectVisual : MonoBehaviour
{
    bool lerp;
    float timer;
    [SerializeField] Vector3 desiredScale;
    public UnityEvent onPress;
    Vector3 ogScale;
    private void OnMouseEnter()
    {
        timer = 0;
        lerp = true;
    }
    private void OnMouseExit()
    {
        timer = 0;
        lerp = false;
    }
    private void OnMouseDown()
    {
        onPress.Invoke();
    }
    void Start()
    {
        ogScale = transform.localScale;
    }
    void Update()
    {
        if(lerp) 
        {
            transform.localScale = Vector3.Lerp(transform.localScale,desiredScale,timer);
            timer += Time.deltaTime;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, ogScale, timer);
            timer += Time.deltaTime;
        }
    }
}
