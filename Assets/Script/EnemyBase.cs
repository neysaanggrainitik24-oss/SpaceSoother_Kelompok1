using UnityEngine;
using System.Collections;

public abstract class EnemyBase : MonoBehaviour
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

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if (sr != null) originalColor = sr.color;
    }

    protected virtual void Start()
    {
        if (rb != null)
            rb.linearVelocity = new Vector2(0, -moveSpeed);

        OnStart();
    }

    protected virtual void Update() => OnUpdate();

    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }

    protected virtual void OnDeath()
    {
        if (ScoreManager.instance != null)
            ScoreManager.instance.AddScore(scoreValue);

        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

public virtual void TakeDamage(int damage)
{
    if (damage <= 0) return;

    hp -= damage;

    if (hp > 0)
        StartCoroutine(HitFlash());
    else
        OnDeath();
}

    protected IEnumerator HitFlash()
    {
        if (sr == null) yield break;

        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        if (sr != null) sr.color = originalColor;
    }

    public void SetVelocity(float speed)
{
    if (rb != null)
        rb.linearVelocity = new Vector2(0, -speed);
}

public void SetMoveSpeed(float newSpeed)
{
    moveSpeed = newSpeed;
    SetVelocity(newSpeed);
}
protected virtual void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("PlayerBullet"))
    {
        PlayerBullet bullet = other.GetComponent<PlayerBullet>();
        int damageAmount = bullet != null ? bullet.damage : 1;
        TakeDamage(damageAmount);
    }
}

}