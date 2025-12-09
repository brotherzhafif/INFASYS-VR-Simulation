using UnityEngine;
using UnityEngine.InputSystem; // Wajib buat baca tombol controller

public class HPInventory : MonoBehaviour
{
    [Header("Setting HP")]
    public GameObject hpDiTangan; // HP yang nempel di tangan kiri
    public GameObject hpDiMeja;   // HP fisik yang mau diambil

    [Header("Tombol Controller")]
    // Ini untuk referensi tombol X (Primary Button) di Controller Kiri
    public InputActionProperty toggleButton;

    private bool sudahPunyaHP = false;
    private bool hpSedangAktif = false;

    void Start()
    {
        // Pastikan HP tangan mati duluan
        if (hpDiTangan != null) hpDiTangan.SetActive(false);
    }

    void Update()
    {
        // Kalau belum punya HP, hentikan fungsi (tombol X gak guna)
        if (!sudahPunyaHP) return;

        // Cek apakah tombol X baru saja ditekan (WasPressedThisFrame)
        if (toggleButton.action.WasPressedThisFrame())
        {
            ToggleHP();
        }
    }

    // Fungsi ini dipanggil saat HP di meja diklik
    public void AmbilHP()
    {
        sudahPunyaHP = true;
        hpDiMeja.SetActive(false); // Hilangkan HP di meja

        // Opsional: Langsung munculkan HP di tangan biar player tau udah dapet
        hpSedangAktif = true;
        hpDiTangan.SetActive(true);

        Debug.Log("HP Masuk Inventory!");
    }

    void ToggleHP()
    {
        hpSedangAktif = !hpSedangAktif;
        hpDiTangan.SetActive(hpSedangAktif);

        // TAMBAHAN: Lapor Quest Manager kalau misinya sedang "Simpan HP"
        if (QuestManager.instance.currentQuestState == 2)
        {
            QuestManager.instance.CekSimpanHP();
        }
    }
}