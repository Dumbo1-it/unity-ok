using UnityEngine;
using System.Collections.Generic;

public class ChunkGenerator : MonoBehaviour
{
    [Header("World Settings")]
    public int chunkSize = 16; // Blocks per chunk (x, y, z)
    public int renderDistance = 2; // How many chunks to load around the player
    public GameObject blockPrefab; // Prefab of the block to spawn

    [Header("Terrain Noise")]
    public float noiseScale = 0.1f; // Adjust terrain roughness
    public int worldHeight = 64; // Max terrain height

    private Dictionary<Vector3Int, Chunk> chunks = new Dictionary<Vector3Int, Chunk>();
    private Vector3Int lastPlayerChunkPos;

    void Start()
    {
        lastPlayerChunkPos = GetChunkPositionFromWorld(transform.position);
        GenerateChunksAroundPlayer();
    }

    void Update()
    {
        Vector3Int currentChunkPos = GetChunkPositionFromWorld(transform.position);
        if (currentChunkPos != lastPlayerChunkPos)
        {
            lastPlayerChunkPos = currentChunkPos;
            GenerateChunksAroundPlayer();
        }
    }

    // Convert world position to chunk coordinates
    private Vector3Int GetChunkPositionFromWorld(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / chunkSize);
        int y = Mathf.FloorToInt(worldPos.y / chunkSize);
        int z = Mathf.FloorToInt(worldPos.z / chunkSize);
        return new Vector3Int(x, y, z);
    }

    // Load/unload chunks based on player position
    private void GenerateChunksAroundPlayer()
    {
        List<Vector3Int> chunksToKeep = new List<Vector3Int>();

        for (int x = -renderDistance; x <= renderDistance; x++)
        for (int z = -renderDistance; z <= renderDistance; z++)
        {
            Vector3Int chunkPos = new Vector3Int(
                lastPlayerChunkPos.x + x,
                0, // Assuming flat terrain (adjust for caves)
                lastPlayerChunkPos.z + z
            );

            chunksToKeep.Add(chunkPos);

            if (!chunks.ContainsKey(chunkPos))
            {
                GenerateChunk(chunkPos);
            }
        }

        // Unload distant chunks (optional)
        List<Vector3Int> chunksToRemove = new List<Vector3Int>();
        foreach (var chunk in chunks)
        {
            if (!chunksToKeep.Contains(chunk.Key))
            {
                Destroy(chunk.Value.gameObject);
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var chunkPos in chunksToRemove)
        {
            chunks.Remove(chunkPos);
        }
    }

    // Create a single chunk
    private void GenerateChunk(Vector3Int chunkPos)
    {
        GameObject chunkObj = new GameObject($"Chunk_{chunkPos.x}_{chunkPos.z}");
        Chunk chunk = chunkObj.AddComponent<Chunk>();
        chunk.Initialize(chunkPos, chunkSize, blockPrefab, noiseScale, worldHeight);
        chunks.Add(chunkPos, chunk);
    }
}