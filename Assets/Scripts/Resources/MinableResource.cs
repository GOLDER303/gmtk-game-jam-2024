using UnityEngine;

public class MinableResource : MonoBehaviour
{
    [SerializeField]
    private GameObject[] resourcesToSpawn;

    [SerializeField]
    private float minSpawnLocalX = -5f;

    [SerializeField]
    private float maxSpawnLocalX = 5f;

    [SerializeField]
    private float minSpawnLocalZ = -1.5f;

    [SerializeField]
    private float maxSpawnLocalZ = -5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Projectile"))
        {
            return;
        }

        GameObject resourceToSpawn = resourcesToSpawn[Random.Range(0, resourcesToSpawn.Length)];

        float spawnLocationX = Random.Range(minSpawnLocalX, maxSpawnLocalX);
        float spawnLocationZ = Random.Range(minSpawnLocalZ, maxSpawnLocalZ);

        Vector3 spawnLocation =
            transform.position + new Vector3(spawnLocationX, .1f, spawnLocationZ);

        GameObject spawnedResource = Instantiate(
            resourceToSpawn,
            spawnLocation,
            Quaternion.Euler(Constants.CameraXRotation, 0f, 0f)
        );
    }
}
