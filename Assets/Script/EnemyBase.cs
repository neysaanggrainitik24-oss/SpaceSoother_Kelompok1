using UnityEngine;
using System.Collections;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Base Enemy Stats")]
    public int hp = 10;
    public int scoreValue = 10;
    public float moveSpeed = 4f;

    [Header("Effects")]
    public GameObject explosionPrefab;

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Color originalColor;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (sr != null)
            originalColor = sr.color;

        // Gerakan default turun ke bawah
        if (rb != null)
            rb.linearVelocity = new Vector2(0, -moveSpeed);

        OnStart();
    }

    protected virtual void Update()
    {
        OnUpdate();
    }

    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }

    protected virtual void OnDeath()
    {
        // Tambah score
        if (ScoreManager.instance != null)
            ScoreManager.instance.AddScore(scoreValue);

        // Spawn animasi ledakan
        if (explosionPrefab != null)
        {
            Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        // Hancurkan enemy
        Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        hp -= damage;

        if (hp > 0)
            StartCoroutine(HitFlash());

        if (hp <= 0)
            OnDeath();
    }

    protected IEnumerator HitFlash()
    {
        if (sr == null) yield break;

        sr.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        if (sr != null)
            sr.color = originalColor;
    }
}