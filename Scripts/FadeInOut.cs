using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    Image image;
    bool fadeIn,fadeOut;
    [SerializeField]UnityEvent onFadeIn,onFadeOut;
    [SerializeField] float fadingTime,Timer;
    [SerializeField] Color transparent,og;
    void Start()
    {
        image = GetComponent<Image>();
        og = image.color;
    }
    void Update()
    {
        if (fadeIn)
        {
            Timer += Time.deltaTime * fadingTime;
            image.color = Color.Lerp(image.color,transparent,Timer);
            if (image.color == transparent)
            {
                fadeIn = false;
                onFadeIn.Invoke();
            }
        }
        else if (fadeOut)
        {
            Timer += Time.deltaTime * fadingTime;
            image.color = Color.Lerp(image.color, og, Timer);
            if (image.color == og)
            {
                fadeOut = false;
                onFadeOut.Invoke();
            }
        }
    }
    public void FadeIn()
    {
        Debug.Log("Fading");
        Timer = 0;
        fadeIn = true;
    }
    public void FadeOut()
    {
        Timer = 0;
        fadeOut = true;
    }
}
