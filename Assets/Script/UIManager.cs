using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Pastikan ini ada

public class UIManager : MonoBehaviour
{
    [Header("UI Screens")]
    public GameObject homeScreen;
    public GameObject inGame;
    public GameObject gameOver;
    public GameObject victoryScreen;

    [Header("Score & HUD - WAJIB DI ASSIGN")]
    public TextMeshProUGUI scoreTextDisplay; 
    public TextMeshProUGUI lifeTextDisplay; // ← TAMBAHKAN INI UNTUK TEKS NYAWA

    [Header("Gameplay Group")]
    [SerializeField] private GameObject gameplayWorld;

    private Player player;
    private ScoreManager scoreManager;

    void Start()
    {
        GoToMainMenu();
    }

    private void GetReferences()
    {
        if (player == null) player = FindFirstObjectByType<Player>();
        if (scoreManager == null) scoreManager = ScoreManager.instance;
    }

    // FUNGSI BARU: Untuk mengupdate tampilan nyawa di layar
    public void UpdateLifeUI(int currentLives)
    {
        if (lifeTextDisplay != null)
        {
            // Kamu bisa pakai teks biasa:
            lifeTextDisplay.text = "" + currentLives;

            // ATAU kalau mau pakai simbol hati/karakter (opsional):
            // string hearts = "";
            // for(int i = 0; i < currentLives; i++) hearts += "❤️";
            // lifeTextDisplay.text = hearts;
        }
    }

    public void PlayGame()
    {
        homeScreen.SetActive(false);
        inGame.SetActive(true);
        gameOver.SetActive(false);
        victoryScreen.SetActive(false);
        
        if (scoreTextDisplay != null) scoreTextDisplay.gameObject.SetActive(true);
        if (lifeTextDisplay != null) lifeTextDisplay.gameObject.SetActive(true); // Nyalakan UI Nyawa

        if (gameplayWorld != null) gameplayWorld.SetActive(true);

        Time.timeScale = 1f;
        ResetGameToStart();
    }

    public void ShowGameOver()
    {
        homeScreen.SetActive(false);
        inGame.SetActive(false);
        gameOver.SetActive(true);
        victoryScreen.SetActive(false);
        
        if (scoreTextDisplay != null) scoreTextDisplay.gameObject.SetActive(true); 
        if (lifeTextDisplay != null) lifeTextDisplay.gameObject.SetActive(false); // Matikan saat game over

        if (gameplayWorld != null) gameplayWorld.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ShowVictory()
    {
        homeScreen.SetActive(false);
        inGame.SetActive(false);
        gameOver.SetActive(false);
        if (victoryScreen != null) victoryScreen.SetActive(true);

        if (scoreTextDisplay != null) scoreTextDisplay.gameObject.SetActive(true);
        if (lifeTextDisplay != null) lifeTextDisplay.gameObject.SetActive(false);

        if (gameplayWorld != null) gameplayWorld.SetActive(false);
        Time.timeScale = 0f;
    }

    public void GoToMainMenu()
    {
        homeScreen.SetActive(true);
        inGame.SetActive(false);
        gameOver.SetActive(false);
        victoryScreen.SetActive(false);
        
        if (scoreTextDisplay != null) scoreTextDisplay.gameObject.SetActive(false);
        if (lifeTextDisplay != null) lifeTextDisplay.gameObject.SetActive(false); // Matikan di menu utama

        if (gameplayWorld != null) gameplayWorld.SetActive(false);
        Time.timeScale = 0f;

        DestroyAllObjectsWithTag("Enemy");
        DestroyAllObjectsWithTag("EnemyBullet");
        DestroyAllObjectsWithTag("Bullet");
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameToStart()
    {
        GetReferences();

        if (scoreManager != null) scoreManager.ResetScore();

        if (player != null)
        {
            player.maxLives = 3; // Batasi maksimum nyawa = 3
            player.currentLives = player.maxLives;
            player.isDead = false;
            player.gameObject.SetActive(true);
            player.transform.position = player.spawnPosition;

            // Update UI Nyawa pertama kali saat start game (jadi angka 3)
            UpdateLifeUI(player.currentLives); 

            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }

        DestroyAllObjectsWithTag("Enemy");
        DestroyAllObjectsWithTag("EnemyBullet");
        DestroyAllObjectsWithTag("Bullet");
    }

    private void DestroyAllObjectsWithTag(string tag)
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag(tag))
            Destroy(obj);
    }
}