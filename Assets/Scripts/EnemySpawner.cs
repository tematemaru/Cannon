using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Настройки спавна")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private Transform[] spawnPoints;

    private float nextSpawnTime;
    private int currentEnemies;

    void Update()
    {
        if (Time.time >= nextSpawnTime && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, randomPoint.position, randomPoint.rotation);
        currentEnemies++;
    }

    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}