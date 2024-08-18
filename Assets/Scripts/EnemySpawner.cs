using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float enemySpawnDelay = 1f;

    [SerializeField]
    private Enemy enemyPrefab;

    [SerializeField]
    private float xSpawnRange = 5f;

    [SerializeField]
    private float zSpawnRange = 50f;

    private void Start()
    {
        StartCoroutine(EnemySpawnCoroutine());
    }

    private IEnumerator EnemySpawnCoroutine()
    {
        while (true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition =
            transform.position
            + new Vector3(
                Random.Range(-xSpawnRange, xSpawnRange),
                0f,
                Random.Range(-zSpawnRange, zSpawnRange)
            );

        Enemy spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        spawnedEnemy.Setup(player);
    }
}
