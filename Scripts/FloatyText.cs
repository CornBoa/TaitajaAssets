using TMPro;
using UnityEngine;

public class FloatyText : MonoBehaviour
{
    public float floatSpeed = 1f;
    public TextMeshProUGUI Ugui;
    public void SetScore(int i)
    {
        Ugui.text = "+" + i.ToString();
    }
    void Update()
    {
        transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
        Ugui.color = new Color(Ugui.color.r, Ugui.color.g, Ugui.color.b, Ugui.color.a - (floatSpeed * Time.deltaTime));
        if (Ugui.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
