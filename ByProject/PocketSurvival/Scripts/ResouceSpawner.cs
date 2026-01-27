using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResouceSpawner : MonoBehaviour
{
    public List<GameObject> resourcePrefabs;
    public List<Transform> spawnPoints;
    public float spawnRate = 5f;
    void Start()
    {
        StartCoroutine(repeater());
    }
    IEnumerator repeater()
    {
        SpawnResources();
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(repeater());
    }
    public void SpawnResources()
    {
        int spawnIndex = 0;
        while (true)
        {
            spawnIndex = Random.Range(0, spawnPoints.Count);
            if(!WitihinScreen(spawnPoints[spawnIndex].position))
            {
                break;
            }
        }
        if (spawnPoints[spawnIndex].childCount < 2)
        {
            int resourceIndex = Random.Range(0, resourcePrefabs.Count);
            GameObject blyat = Instantiate(resourcePrefabs[resourceIndex], spawnPoints[spawnIndex].position, Quaternion.identity, spawnPoints[spawnIndex]);
            blyat.transform.localScale = Vector3.one / 10;
        }
    }
    bool WitihinScreen(Vector3 pos)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(pos);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }
}
