using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Header("Player Bullet Settings")]
    public float speed = 15f;
    public int damage = 5;
    public float lifetime = 2.5f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Start()
    {
        if (rb != null)
            rb.linearVelocity = new Vector2(0, speed);

        Destroy(gameObject, lifetime);   // Otomatis hancur setelah waktu
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Optional: Hancur jika kena musuh lain atau apapun
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}