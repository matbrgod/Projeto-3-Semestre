using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject platformPrefab;
    public Vector3 platformSpawn;
    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnPlatform), 0f, spawnInterval);
    }

    void SpawnPlatform()
    {
        Instantiate(platformPrefab, platformSpawn, Quaternion.identity);
    }
}
