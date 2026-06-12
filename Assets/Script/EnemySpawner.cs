using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

    [Header("Spawn Settings")]
    public float spawnRatePhase1 = 1.1f;
    public float spawnRatePhase2 = 0.85f;

    [Header("Enemy3 Settings")]
    public float enemy3SpawnRate = 0.3f;
    public float enemy3Speed = 2f;

    [Header("Spawn Position")]
    public float minX = -5.5f;
    public float maxX = 5.5f;
    public float spawnY = 7.8f;

    private float nextSpawnTime;
    private float nextEnemy3Time;

    void Update()
    {
        if (ScoreManager.instance == null) return;

        if (ScoreManager.instance.currentPhase == ScoreManager.GamePhase.Phase1)
        {
            SpawnEnemy1();
        }
        else if (ScoreManager.instance.currentPhase == ScoreManager.GamePhase.Phase2)
        {
            SpawnEnemy2();
        }
        else if (ScoreManager.instance.currentPhase == ScoreManager.GamePhase.Phase3)
        {
            SpawnEnemy3();
        }
    }

    void SpawnEnemy1()
    {
        if (Time.time < nextSpawnTime) return;

        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), spawnY, 0);
        Instantiate(enemy1Prefab, spawnPos, Quaternion.identity);

        nextSpawnTime = Time.time + spawnRatePhase1;
    }

    void SpawnEnemy2()
    {
        if (Time.time < nextSpawnTime) return;

        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), spawnY, 0);
        Instantiate(enemy2Prefab, spawnPos, Quaternion.identity);

        nextSpawnTime = Time.time + spawnRatePhase2;
    }

    void SpawnEnemy3()
    {
        if (enemy3Prefab == null) return;
        if (Time.time < nextEnemy3Time) return;

        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), spawnY, 0);

        GameObject enemy3Obj = Instantiate(enemy3Prefab, spawnPos, Quaternion.identity);

        BaseEnemy enemy = enemy3Obj.GetComponent<BaseEnemy>();
        if (enemy != null)
        {
            enemy.moveSpeed = enemy3Speed;
        }

        nextEnemy3Time = Time.time + enemy3SpawnRate;
    }
}