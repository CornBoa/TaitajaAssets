using UnityEngine;

public class SlotLerp : MonoBehaviour
{
    public Vector3 lerpTarget;
    public bool lerping;
    float timer;
    void Start()
    {
        
    }
    void Update()
    {
        if (transform.childCount == 0) gameObject.SetActive(false);
        else if (!transform.GetChild(0).gameObject.activeSelf) transform.GetChild(0).transform.parent = null;
        if (lerping && Vector3.Distance(transform.position, lerpTarget) > 0.01)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, lerpTarget, timer);
        }
        else if (lerping)
        {
            lerping = false;
            timer = 0;
        }
    }
}
