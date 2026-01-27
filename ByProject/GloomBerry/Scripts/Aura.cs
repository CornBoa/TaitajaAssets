using UnityEngine;

public class Aura : MonoBehaviour
{
    public Vector3 scale;
    void Start()
    {
        scale = transform.localScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Berry"))
        {
            collision.GetComponent<Berry>().onPickup();
        }
    }
}
