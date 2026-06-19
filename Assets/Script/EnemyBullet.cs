using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Enemy Bullet Settings")]
    public float speed = 8f;
    public int damage = 1;
    public float lifetime = 5f;

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

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void Initialize(Vector2 direction, float customSpeed = -1f, int customDamage = -1)
    {
        if (customSpeed > 0) speed = customSpeed;
        if (customDamage > 0) damage = customDamage;

        if (rb != null && direction.sqrMagnitude > 0.01f)
        {
            rb.linearVelocity = direction.normalized * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Player player = collision.collider.GetComponent<Player>();
            if (player != null)
                player.TakeDamage(damage);

            Destroy(gameObject);
            return;
        }

        // Hancur jika bukan enemy atau boundary
        if (!collision.collider.CompareTag("Enemy") && 
            !collision.collider.CompareTag("Enemy2") && 
            !collision.collider.CompareTag("Enemy3"))
        {
            Destroy(gameObject);
        }
    }
}