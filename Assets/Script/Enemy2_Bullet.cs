using UnityEngine;

public class Enemy2_Bullet : MonoBehaviour
{
    [Header("Enemy2 Bullet Settings")]
    public float speed = 9f;
    public int damage = 1;
    public float lifetime = 6f;

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

    public void Initialize(Vector2 direction)
    {
        if (rb != null && direction.sqrMagnitude > 0.01f)
        {
            rb.linearVelocity = direction.normalized * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
                player.TakeDamage(damage);

            Destroy(gameObject);
            return;
        }

        if (!other.CompareTag("Enemy") && !other.CompareTag("Enemy2") && !other.CompareTag("Enemy3"))
        {
            Destroy(gameObject);
        }
    }
}