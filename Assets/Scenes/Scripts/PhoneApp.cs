using UnityEngine;
using UnityEngine.InputSystem; // Wajib buat baca tombol VR

public class PhoneApp : MonoBehaviour
{
    [Header("Tombol Kontrol")]
    // Kita pakai tombol Grip (Genggam) buat nyalain air
    public InputActionProperty tombolAksi;

    [Header("Koneksi")] 
    public WaterSystem sistemAir; // Sambungin ke script air tadi

    void Update()
    {
        // Logika:
        // Script ini cuma jalan kalau HP_HAND sedang aktif (muncul).
        // Kalau HP disimpen (SetActive false), Update gak jalan.

        // Cek apakah tombol Grip baru saja ditekan?
        if (tombolAksi.action.WasPressedThisFrame())
        {
            // Panggil fungsi di script sebelah
            if (sistemAir != null) sistemAir.ToggleWater();
        }
    }
}