using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] List<GameObject> slides;
    int i = 0;
    void Start()
    {
        
    }
    public void NextSlide(int I)
    {
        i++;
        if (I < slides.Count)
        {
            for (int j = 0; j < slides.Count; j++)
            {
                if (i == I && j == i)
                {
                    slides[j].SetActive(true);
                }
                else slides[j].SetActive(false);
            }
        }
        else
        {
            for (int j = 0; j < slides.Count; j++)
            {
                if (i == j)
                {
                    slides[j].SetActive(true);
                }
                else slides[j].SetActive(false);
            }
        }
    }
}
