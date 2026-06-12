using UnityEngine;
using System.Collections;

public class Enemy2 : BaseEnemy
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform  firePoint;
    public float      fireRate = 2.3f;

    [Header("Summon Settings")]
    public GameObject enemy1Prefab;
    public float      summonInterval = 14f;
    public float      summonOffsetY  = 1.5f;

    protected override void OnStart()
    {
        StartCoroutine(ShootRoutine());
        StartCoroutine(SummonRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Shoot();
        }
    }

private void Shoot()
{
    if (bulletPrefab == null || firePoint == null) return;

    GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

    Enemy2_Bullet bulletScript = bulletObj.GetComponent<Enemy2_Bullet>();
    
    if (bulletScript != null)
    {
        // === TEMBAK KE ARAH PLAYER ===
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObj != null)
        {
            Vector2 direction = (playerObj.transform.position - firePoint.position).normalized;
            bulletScript.Initialize(direction);
        }
        else
        {
            // Fallback kalau player tidak ketemu
            bulletScript.Initialize(Vector2.left);
        }
    }
}

    private IEnumerator SummonRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(summonInterval);
            SummonEnemy();
        }
    }

    private void SummonEnemy()
    {
        if (enemy1Prefab == null) return;

        Vector3 spawnPos = transform.position + new Vector3(0, summonOffsetY, 0);
        Instantiate(enemy1Prefab, spawnPos, Quaternion.identity);
    }
}