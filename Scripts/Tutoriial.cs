using System.Collections.Generic;
using UnityEngine;

public class Tutoriial : MonoBehaviour
{
    List<GameObject> slides;
    int i = 0;
    void Start()
    {
        foreach(Transform child in transform)
        {
            slides.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        slides[0].SetActive(true);
    }
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
