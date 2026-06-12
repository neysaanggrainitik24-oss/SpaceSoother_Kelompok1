using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 12f;
    public int damage = 5;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BaseEnemy enemy = other.GetComponent<BaseEnemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}