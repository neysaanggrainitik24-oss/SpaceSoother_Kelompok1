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

    [Header("Enemy3 (Boss) Settings")]
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

        switch (ScoreManager.instance.currentPhase)
        {
            case ScoreManager.GamePhase.Phase1:
                SpawnEnemy1();
                break;
            case ScoreManager.GamePhase.Phase2:
                SpawnEnemy2();
                break;
            case ScoreManager.GamePhase.Phase3:
                SpawnEnemy3();
                break;
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

        EnemyBase enemy = enemy3Obj.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.SetMoveSpeed(enemy3Speed);   // ← Pakai method ini
        }

        nextEnemy3Time = Time.time + enemy3SpawnRate;
    }
}