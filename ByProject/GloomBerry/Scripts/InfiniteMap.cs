using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject chunkPrefab;

    [Header("Settings")]
    public float chunkSize = 20;      
    public int renderDistance = 2;    

    private Dictionary<Vector2Int, GameObject> spawnedChunks = new Dictionary<Vector2Int, GameObject>();
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector2Int playerChunk = GetChunkCoord(player.position);
        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int y = -renderDistance; y <= renderDistance; y++)
            {
                Vector2Int coord = new Vector2Int(
                    playerChunk.x + x,
                    playerChunk.y + y
                );

                if (!spawnedChunks.ContainsKey(coord))
                {
                    SpawnChunk(coord);
                }
            }
        }

        List<Vector2Int> toRemove = new List<Vector2Int>();

        foreach (var chunk in spawnedChunks)
        {
            float distance = Vector2Int.Distance(chunk.Key, playerChunk);
            if (distance > renderDistance + 1)
                toRemove.Add(chunk.Key);
        }

        foreach (var coord in toRemove)
        {
            Destroy(spawnedChunks[coord]);
            spawnedChunks.Remove(coord);
        }
    }

    Vector2Int GetChunkCoord(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / chunkSize);
        int y = Mathf.FloorToInt(position.y / chunkSize);
        return new Vector2Int(x, y);
    }

    void SpawnChunk(Vector2Int coord)
    {
        Vector3 worldPos = new Vector3(
            coord.x * chunkSize,
            coord.y * chunkSize,
            0f
        );

        GameObject chunk = Instantiate(chunkPrefab, worldPos, Quaternion.identity, transform);
        chunk.name = $"Chunk {coord.x}, {coord.y}";

        spawnedChunks.Add(coord, chunk);
    }
}