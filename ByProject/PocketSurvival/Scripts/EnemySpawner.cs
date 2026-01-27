using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject meleeEnemy, rangedEnemy;
    public float spawnRate = 2f, spawnAmount = 3f;
    int hpMod = 0;
    public List<Transform> spawnPoints;
    public int enemyCap = 50;
    void Start()
    {
        foreach(Transform t in transform)
        {
            spawnPoints.Add(t);
        }
        StartCoroutine(enumerator());
    }    
    IEnumerator enumerator()
    {
        float timer = 0f;
        float upgradeTimer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            upgradeTimer += Time.deltaTime;
            if (timer > spawnRate && FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length < enemyCap)
            {
                for(int i = 0; i < spawnAmount; i++)
                {
                    Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
                    if (Random.value > 0.1f)
                    {
                        Instantiate(meleeEnemy, spawnPosition, Quaternion.identity).GetComponent<Enemy>().TakeDamage(-hpMod);
                    }
                    else
                    {
                        Instantiate(rangedEnemy, spawnPosition, Quaternion.identity).GetComponent<Enemy>().TakeDamage(-hpMod);
                    }
                }
                timer = 0f; 
            }
            if(upgradeTimer > 15f)
            {
                upgradeTimer = 0f;
                if(spawnRate > 0.5f)
                    spawnRate -= 0.05f;
                spawnAmount += 1f;
                hpMod += 1;
            }
            yield return null;
        }
    }
}
