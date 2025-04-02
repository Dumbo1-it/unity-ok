using UnityEngine;

public class Chunk : MonoBehaviour
{
    public void Initialize(Vector3Int chunkPos, int size, GameObject blockPrefab, float noiseScale, int maxHeight)
    {
        transform.position = new Vector3(chunkPos.x * size, 0, chunkPos.z * size);

        // Generate blocks in this chunk
        for (int x = 0; x < size; x++)
        for (int z = 0; z < size; z++)
        {
            // Use Perlin noise for terrain height
            float xCoord = (chunkPos.x * size + x) * noiseScale;
            float zCoord = (chunkPos.z * size + z) * noiseScale;
            int height = Mathf.FloorToInt(Mathf.PerlinNoise(xCoord, zCoord) * maxHeight);

            // Spawn blocks from bottom to height
            for (int y = 0; y <= height; y++)
            {
                Vector3 blockPos = new Vector3(x, y, z);
                Instantiate(blockPrefab, transform.position + blockPos, Quaternion.identity, transform);
            }
        }
    }
}