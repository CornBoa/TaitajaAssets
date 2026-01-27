using System.Collections.Generic;
using UnityEngine;

public class Tutoriial : MonoBehaviour
{
    public List<GameObject> slides;
    int i = 0;
    public void NextSlide()
    {
        slides[i].SetActive(false);
        i++;
        if (i < slides.Count)
        {
            slides[i].SetActive(true);
        }
    }
}
