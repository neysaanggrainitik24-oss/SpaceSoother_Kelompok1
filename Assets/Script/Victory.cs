using UnityEngine;
using UnityEngine.UI; // 👈 Wajib ada ini untuk mendeteksi komponen Button

public class menuVictory : MonoBehaviour
{
    private UIManager uiManager;

    void Start()
    {
        // 1. Otomatis cari UIManager di scene
        uiManager = FindFirstObjectByType<UIManager>();

        // 2. OTOMATIS PASANG EVENT KLIK TANPA DRAG-DROP DI EDITOR!
        Button tombolIni = GetComponent<Button>();
        if (tombolIni != null)
        {
            // Menghapus klik lama (biar ga ganda) lalu memasang fungsi klik baru
            tombolIni.onClick.RemoveAllListeners();
            tombolIni.onClick.AddListener(OnMainMenuButtonClicked);
            Debug.Log($"✅ Sukses memasang sistem klik otomatis pada tombol: {gameObject.name}");
        }
        else
        {
            Debug.LogError($"❌ Script menuVictory dipasang di [{gameObject.name}], tapi objek ini BUKAN komponen Button UI! Pindahkan script ini tepat di dalam objek Tombol UI-mu.");
        }
    }

    public void OnMainMenuButtonClicked()
    {
        if (uiManager == null)
        {
            uiManager = FindFirstObjectByType<UIManager>();
        }

        if (uiManager != null)
        {
            uiManager.GoToMainMenu();
            Debug.Log("🔘 Tombol Main Menu sukses dieksekusi!");
        }
        else
        {
            Debug.LogError("❌ UIManager tidak ditemukan saat tombol diklik!");
        }
    }
}