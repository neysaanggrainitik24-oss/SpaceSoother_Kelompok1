using UnityEngine;
using System.Collections;

/// <summary>
/// Base class untuk semua enemy. Berisi logika umum yang digunakan
/// oleh Enemy1, Enemy2, dan Enemy3.
/// </summary>
public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Base Enemy Stats")]
    public int hp          = 10;
    public int scoreValue  = 10;
    public float moveSpeed = 4f;

    protected Rigidbody2D    rb;
    protected SpriteRenderer sr;
    protected Color          originalColor;

    protected virtual void Start()
    {
        rb            = GetComponent<Rigidbody2D>();
        sr            = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        // Gerakan default: turun ke bawah
        if (rb != null)
            rb.linearVelocity = new Vector2(0, -moveSpeed);

        OnStart(); // hook untuk child class
    }

    protected virtual void Update()
    {
        OnUpdate(); // hook untuk child class
    }

    protected virtual void OnStart()  { }
    protected virtual void OnUpdate() { }

    protected virtual void OnDeath()
    {
        // Lebih aman menggunakan singleton daripada FindFirstObjectByType setiap kali mati
        if (ScoreManager.instance != null)
            ScoreManager.instance.AddScore(scoreValue);

        Destroy(gameObject);
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        hp -= damage;
        StartCoroutine(HitFlash());

        if (hp <= 0)
            OnDeath();
    }

    protected IEnumerator HitFlash()
    {
        if (sr == null) yield break;

        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColor;
    }
}