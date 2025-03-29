using UnityEngine;
using System.Collections;

public class RandomSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnInterval = 1f;
    public Vector2 spawnAreaMin = new Vector2(-8f, -4f);
    public Vector2 spawnAreaMax = new Vector2(8f, 4f);
    public float objectLifetime = 2f; // All objects will disappear after 2 seconds

    private void Start()
    {
        StartCoroutine(SpawnObjectsRoutine());
    }

    IEnumerator SpawnObjectsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y),
            0f);

        GameObject newObj = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        
        // Set the lifetime for the new object
        ClickableObject clickable = newObj.GetComponent<ClickableObject>();
        if (clickable != null)
        {
            clickable.lifetime = objectLifetime;
        }
    }

    // Visualize spawn area in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = new Vector3(
            (spawnAreaMin.x + spawnAreaMax.x) / 2,
            (spawnAreaMin.y + spawnAreaMax.y) / 2,
            0);
        Vector3 size = new Vector3(
            spawnAreaMax.x - spawnAreaMin.x,
            spawnAreaMax.y - spawnAreaMin.y,
            0.1f);
        Gizmos.DrawWireCube(center, size);
    }
}