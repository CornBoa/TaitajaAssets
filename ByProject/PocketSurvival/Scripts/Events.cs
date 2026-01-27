using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Events : MonoBehaviour
{
    public float minTime, maxTime;
    int eventIndex = 0;
    public List<Enemy> enemyPool;
    public List<Transform> spawnPoints, destinations;
    void Start()
    {
        StartCoroutine(SpawnEvent());
        GameObject.Find("EventText").GetComponent<TextMeshProUGUI>().text = "";
    }
    void Update()
    {
        
    }
    IEnumerator SpawnEvent()
    {
        float waitTime = Random.Range(minTime, maxTime);
        yield return new WaitForSeconds(waitTime);
        eventIndex = Random.Range(0, 2);
        switch(eventIndex)
            {
            case 0:
                StartCoroutine(ConferenceBad());
                break;
            case 1:
                
                StartCoroutine(GoodConf());
                break;
        }
    }
    IEnumerator ConferenceBad()
    {
        GameObject.Find("EventText").GetComponent<TextMeshProUGUI>().text = "Beware the attendees herd! \n They make you tired!";
        Transform destination = destinations[Random.Range(0,destinations.Count)];
        float timer = 0;
        float savedTimer = 0;
        while (timer < 20)
        {
            if (timer - savedTimer >= 0.5f)
            {
                savedTimer = timer;
                foreach (Enemy enemy in enemyPool)
                {
                    if (!enemy.gameObject.activeSelf)
                    {
                        enemy.transform.position = spawnPoints[Random.Range(0,spawnPoints.Count)].transform.position;
                        enemy.gameObject.SetActive(true);
                        enemy.followSomewhere = true;
                        enemy.destination = destination;
                        break;
                    }
                }
            }
            timer += Time.deltaTime;
            yield return null;
        }
        GameObject.Find("EventText").GetComponent<TextMeshProUGUI>().text = "";
        StartCoroutine(SpawnEvent());
    }
    IEnumerator GoodConf()
    {
        foreach(ConferenceDoor door in FindObjectsByType<ConferenceDoor>(FindObjectsSortMode.None))
        {
            door.IsOpen = true;
        }
        GameObject.Find("EventText").GetComponent<TextMeshProUGUI>().text = "Speech has started! \n Visit it to gain knowledge!";
        Transform destination = destinations[Random.Range(0, destinations.Count)];
        float timer = 0;
        float savedTimer = 0;
        while (timer < 15)
        {
            if (timer - savedTimer >= 1f)
            {
                savedTimer = timer;
                foreach (Enemy enemy in enemyPool)
                {
                    if (!enemy.gameObject.activeInHierarchy)
                    {
                        enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
                        enemy.gameObject.SetActive(true);
                        enemy.followSomewhere = true;
                        enemy.destination = destination;
                        break;
                    }
                }
            }
            timer += Time.deltaTime;
            yield return null;
        }
        foreach (ConferenceDoor door in FindObjectsByType<ConferenceDoor>(FindObjectsSortMode.None))
        {
            door.IsOpen = false;
        }
        StartCoroutine(SpawnEvent());
    }
}
