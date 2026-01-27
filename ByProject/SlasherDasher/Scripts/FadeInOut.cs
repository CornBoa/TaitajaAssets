using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    Image image;
    bool fadeIn,fadeOut;
    public bool fadeInOnStart;
    public UnityEvent onFadeIn,onFadeOut;
    public float fadingTime = 0.5f,Timer;
    [SerializeField] Color transparent,og;
    void Start()
    {
        image = GetComponent<Image>();
        image.raycastTarget = false;
        og = Color.black;
        image.color = og;
        if (fadeInOnStart)
        {
            FadeIn();
        }
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
        Timer = 0;
        fadeIn = true;
    }
    public void FadeOut()
    {
        Timer = 0;
        fadeOut = true;
    }
}
