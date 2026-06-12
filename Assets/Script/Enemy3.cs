using UnityEngine;
using System.Collections;

public class Enemy3 : BaseEnemy
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    public float fireRate = 1.2f;
    public int bulletDamage = 2;
    public float bulletSpeed = 8f;

    [Header("Summon Settings")]
    public GameObject enemy1Prefab;
    public float summonInterval = 8f;

    protected override void OnStart()
    {
        moveSpeed = 3f;

        if (rb != null)
            rb.linearVelocity = new Vector2(0, -moveSpeed);

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
        if (bulletPrefab == null) return;

        SpawnBullet(firePoint1, Vector2.down + Vector2.left);
        SpawnBullet(firePoint2, Vector2.down + Vector2.right);
    }

    private void SpawnBullet(Transform firePoint, Vector2 direction)
    {
        if (firePoint == null) return;

        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Enemy3_Bullet bullet = bulletObj.GetComponent<Enemy3_Bullet>();

        if (bullet != null)
        {
            bullet.damage = bulletDamage;
            bullet.speed = bulletSpeed;
            bullet.SetDirection(direction);
        }
    }

    private IEnumerator SummonRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(summonInterval);
            SummonEnemies();
        }
    }

    private void SummonEnemies()
    {
        if (enemy1Prefab == null) return;

        Instantiate(enemy1Prefab, transform.position + new Vector3(-2f, -1f, 0), Quaternion.identity);
        Instantiate(enemy1Prefab, transform.position + new Vector3(2f, -1f, 0), Quaternion.identity);
    }
}