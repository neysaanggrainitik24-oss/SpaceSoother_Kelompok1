using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;

    [Header("Shooting - Auto")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;

    [Header("Lives System")]
    public int maxLives = 3;
    public int currentLives = 3;

    [Header("Respawn Settings")]
    public float respawnTime = 2f;
    public Vector3 spawnPosition = new Vector3(0, -3f, 0);

    public bool isDead = false; 

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float nextFire = 0f;
    private bool isInvulnerable = false;
    private float invulnerableTime = 1.5f;

   void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentLives = maxLives;
        transform.position = spawnPosition;

        // TAMBAHAN: Update UI nyawa saat game pertama kali dijalankan
        UIManager uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateLifeUI(currentLives);
        }
    }
    void Update()
    {
        if (isDead) return;

        HandleMovement();
        HandleShooting();
        ClampPosition();
    }

    void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))  moveX = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveX = 1;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))    moveY = 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))  moveY = -1;

        // Antisipasi versi Unity: Menggunakan .velocity agar aman di semua versi Unity
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
        }
    }

    void HandleShooting()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void ClampPosition()
    {
        float clampedX = Mathf.Clamp(transform.position.x, -8f, 8f);
        float clampedY = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector3(clampedX, clampedY, 0);
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }

  public void TakeDamage(int damage = 1)
    {
        if (isDead || isInvulnerable) return;

        currentLives -= damage;
        Debug.Log($"Player kena damage! Nyawa tersisa: {currentLives}/{maxLives}");

        // TAMBAHAN: Update teks UI Nyawa setiap kali Player kena hit
        UIManager uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateLifeUI(currentLives);
        }

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerableFlash());
        }
    }

    private IEnumerator InvulnerableFlash()
    {
        isInvulnerable = true;

        for (float i = 0; i < invulnerableTime; i += 0.1f)
        {
            if (sr != null) sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
        }

        if (sr != null) sr.enabled = true;
        isInvulnerable = false;
    }

    void Die()
    {
        isDead = true;
        Debug.Log("GAME OVER - Player Mati!");
        gameObject.SetActive(false);

        UIManager uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager != null)
            uiManager.ShowGameOver();
    }

    private void LoadGameOverScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    public void Respawn()
    {
        if (currentLives <= 0) return;

        isDead = false;
        gameObject.SetActive(true);
        transform.position = spawnPosition;
        
        if (rb != null) rb.linearVelocity = Vector2.zero;
        
        StartCoroutine(InvulnerableFlash());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead || isInvulnerable) return;

        // Kena Badan Enemy
        if (other.CompareTag("Enemy") || 
            other.CompareTag("Enemy2") || 
            other.CompareTag("Enemy3"))
        {
            TakeDamage(1);

            // Hancurkan enemy saat tabrakan
            if (other.TryGetComponent<Enemy>(out Enemy e1)) e1.TakeDamage(999);
            if (other.TryGetComponent<Enemy2>(out Enemy2 e2)) e2.TakeDamage(999);
            if (other.TryGetComponent<Enemy3>(out Enemy3 e3)) e3.TakeDamage(999);
        }

        // Kena Peluru Enemy
        if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}