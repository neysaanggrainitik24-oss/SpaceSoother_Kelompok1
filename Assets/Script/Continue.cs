using UnityEngine;

public class Continue : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;

    // 1. Fungsi untuk melanjutkan game dari Pause Menu
    public void ContinueGame()
    {
        Time.timeScale = 1f; // Jalankan kembali waktu game

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false); 
        }
        
        Debug.Log("Game Continued!");
    }

    // 2. Fungsi untuk kembali ke Main Menu (HomeScreen)
    public void MainMenuGame()
    {
        Time.timeScale = 1f; 

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false); 
        }

        UIManager uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager != null)
        {
            uiManager.GoToMainMenu();
            Debug.Log("Back to HomeScreen!");
        }
    }

    // 3. PERBAIKAN: Fungsi Restart Game (Sama seperti retryclick tanpa load scene)
    public void RstartGame()
    {
        // Tutup dulu panel pause menu agar tidak menghalangi layar gameplay
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false); 
        }

        // Cari UIManager di dalam scene
        UIManager uiManager = FindFirstObjectByType<UIManager>();

        if (uiManager != null)
        {
            // Langsung panggil PlayGame() untuk mereset skor, nyawa, dan menyalakan gameplayWorld
            uiManager.PlayGame();
            Debug.Log("Game di-restart langsung dari Pause Menu tanpa reload scene!");
        }
        else
        {
            Debug.LogWarning("UIManager tidak ditemukan!");
        }
    }
}