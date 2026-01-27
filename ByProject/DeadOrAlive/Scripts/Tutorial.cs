using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]List<GameObject> gameObjects;
    int i = 0;
    private void Start()
    {
        gameObjects[i].SetActive(true);
    }
    public void NextSlide()
    {
        gameObjects[i].SetActive(false);
        i++;
        if(i < gameObjects.Count)gameObjects[i].SetActive(true);
    }
}
