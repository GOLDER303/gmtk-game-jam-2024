using UnityEngine;

public class MinableResource : MonoBehaviour
{
    [SerializeField]
    private GameObject[] resourcesToSpawn;

    [SerializeField]
    private float spawnRange = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Projectile"))
        {
            return;
        }

        GameObject resourceToSpawn = resourcesToSpawn[Random.Range(0, resourcesToSpawn.Length)];

        Vector2 randomPlaceInsideUnityCircle = Random.insideUnitCircle * spawnRange;

        Vector3 spawnLocation = new Vector3(
            randomPlaceInsideUnityCircle.x,
            .1f,
            randomPlaceInsideUnityCircle.y
        );

        GameObject spawnedResource = Instantiate(
            resourceToSpawn,
            spawnLocation,
            Quaternion.Euler(Constants.CameraXRotation, 0f, 0f)
        );
    }
}
