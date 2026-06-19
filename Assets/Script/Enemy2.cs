using UnityEngine;
using System.Collections;

public class Enemy2 : EnemyBase
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2.3f;

    [Header("Summon Settings")]
    public GameObject enemy1Prefab;
    public float summonInterval = 14f;
    public float summonOffsetY = 1.5f;

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
        EnemyBullet bullet = bulletObj.GetComponent<EnemyBullet>();

        if (bullet != null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            Vector2 dir = playerObj != null 
                ? (playerObj.transform.position - firePoint.position).normalized 
                : Vector2.down;

            bullet.Initialize(dir, 9f, 1);
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
        Vector3 pos = transform.position + new Vector3(0, summonOffsetY, 0);
        Instantiate(enemy1Prefab, pos, Quaternion.identity);
    }
}