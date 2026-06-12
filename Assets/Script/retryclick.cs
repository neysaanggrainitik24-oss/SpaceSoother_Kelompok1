using UnityEngine;

public class retryclick : MonoBehaviour
{
    private void OnMouseDown()
    {
        // 1. Cari komponen UIManager di dalam scene
        UIManager uiManager = FindFirstObjectByType<UIManager>();

        if (uiManager != null)
        {
            // 2. Langsung panggil fungsi PlayGame (ini akan otomatis mereset score & menyalakan panel InGame)
            uiManager.PlayGame();
            Debug.Log("Game di-retry langsung tanpa reload scene!");
        }
    }
}