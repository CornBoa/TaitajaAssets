using UnityEngine;

public class SquishAnim : MonoBehaviour
{
    public float squishAmount = 0.2f;
    public float speed = 3f;
    public bool toSquish = false;
    public float rotationAmount = 5f;
    private Vector3 baseScale;
    private Vector3 baseRotation;
    void Start()
    {
        baseScale = transform.localScale;
    }
    void Update()
    {
        if (toSquish)
        {
            float yOffset = Mathf.Sin(Time.time * speed) * squishAmount;
            float rotOffset = Mathf.Sin(Time.time * speed) * rotationAmount;
            transform.localScale = new Vector3(baseScale.x, baseScale.y + yOffset, baseScale.z);
            transform.localEulerAngles = new Vector3(baseRotation.x, baseRotation.y, baseRotation.z + rotOffset);
        }      
    }
}

