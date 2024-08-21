using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float maxEnemySpawnDelay = 10f;

    [SerializeField]
    private float minEnemySpawnDelay = 1f;

    [SerializeField]
    private float spawnDelayDecreaseRate = 1f;

    [SerializeField]
    private Enemy enemyPrefab;

    [SerializeField]
    private float xSpawnRange = 5f;

    [SerializeField]
    private float zSpawnRange = 50f;

    [SerializeField]
    private GameManager gameManager;

    private float currentEnemySpawnDelay;
    private Coroutine spawnDecreaseRateCoroutine;

    private void Start()
    {
        currentEnemySpawnDelay = maxEnemySpawnDelay;
        StartCoroutine(EnemySpawnCoroutine());
        spawnDecreaseRateCoroutine = StartCoroutine(SpawnRateDecreaseCoroutine());
    }

    void Update()
    {
        if (gameManager.TimeLeftToWin <= 30)
        {
            currentEnemySpawnDelay = .9f;
        }
    }

    private IEnumerator EnemySpawnCoroutine()
    {
        while (true)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(currentEnemySpawnDelay);
        }
    }

    private IEnumerator SpawnRateDecreaseCoroutine()
    {
        while (true)
        {
            if (currentEnemySpawnDelay <= minEnemySpawnDelay)
            {
                StopCoroutine(spawnDecreaseRateCoroutine);
            }

            currentEnemySpawnDelay -= spawnDelayDecreaseRate;

            yield return new WaitForSeconds(1);
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

        spawnedEnemy.Setup(player, gameManager);
    }
}
