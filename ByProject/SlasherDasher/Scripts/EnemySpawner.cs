using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    List<Transform> spawnPoint;
    public List<GameObject> enemies;
    public float spawnTime;
    public int spawnAmount, spawnCap;
    public float spawnRadius = 10f;
    public int maxAttempts = 30;
    public Transform referenceObject;
    Camera cam;
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            spawnPoint.Add(transform.GetChild(i));
        }
        referenceObject = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main;
    }
    [ContextMenu("Start Waves")]
    public void StartWaves()
    {
        StartCoroutine(SpawnEnemies());
    }
    IEnumerator SpawnEnemies()
    {
        while(spawnAmount < spawnCap)
        {
            for(int i = 0; i < spawnAmount; i++)
            {
                Spawn();
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }
    public void Spawn()
    {
        Vector2 spawnPos;

        if (TryGetSpawnPosition(out spawnPos))
        {
            foreach (GameObject enemy in enemies)
            {
                if (!enemy.activeSelf)
                {
                    enemy.transform.position = spawnPos;
                    enemy.SetActive(true);
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("Could not find a valid spawn position out of view.");
        }
    }
    bool TryGetSpawnPosition(out Vector2 position)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector2 candidate = (Vector2)referenceObject.position + randomOffset;

            if (!IsInCameraView(candidate))
            {
                position = candidate;
                return true;
            }
        }

        position = Vector2.zero;
        return false;
    }

    bool IsInCameraView(Vector2 worldPosition)
    {
        Vector3 viewportPoint = cam.WorldToViewportPoint(worldPosition);

        return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
               viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
               viewportPoint.z > 0;
    }
}
