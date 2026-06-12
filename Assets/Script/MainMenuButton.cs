using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public UIManager uiManager;

    private void Start()
    {
        // Otomatis cari UIManager saat game mulai, biar kamu ga perlu seret-seret objek
        if (uiManager == null)
        {
            uiManager = FindFirstObjectByType<UIManager>();
        }
    }

    // PENGAMAN 1: Untuk Tombol di Defeat (yang pakai Sprite/Fisik 2D)
    private void OnMouseDown()
    {
        EksekusiKeMenu();
    }

    // PENGAMAN 2: Untuk Tombol di Victory (yang pakai Canvas UI Button)
    public void OnMainMenuButtonClicked()
    {
        EksekusiKeMenu();
    }

    private void EksekusiKeMenu()
    {
        if (uiManager == null)
        {
            uiManager = FindFirstObjectByType<UIManager>();
        }

        if (uiManager != null)
        {
            uiManager.GoToMainMenu();
            Debug.Log("🔘 Tombol Main Menu Sukses Diklik!");
        }
        else
        {
            Debug.LogError("UIManager belum diassign di MainMenuButton!");
        }
    }
}