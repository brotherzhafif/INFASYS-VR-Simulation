using UnityEngine;

public class PompaManager : MonoBehaviour
{
    [Header("Komponen Air")]
    // Kita ganti jadi GameObject biasa (si Kotak Biru)
    public GameObject[] semuaAir;

    public AudioSource suaraAir; // Tetap pake suara biar enak

    [Header("Koneksi ke HP")]
    public GameObject hpDiTangan;

    private bool isNyala = false;

    void Start()
    {
        // Pas game mulai, pastiin semua air mati (hilang) dulu
        MatikanAir();
    }

    public void TogglePompa()
    {
        // Cek HP Aktif gak?
        if (hpDiTangan != null && !hpDiTangan.activeSelf)
        {
            Debug.Log("Gagal: Buka HP dulu!");
            return;
        }

        isNyala = !isNyala;

        if (isNyala) HidupkanAir();
        else MatikanAir();
    }

    void HidupkanAir()
    {
        if (suaraAir) suaraAir.Play();

        // Munculkan semua kotak biru
        foreach (GameObject air in semuaAir)
        {
            if (air != null) air.SetActive(true);
        }
    }

    void MatikanAir()
    {
        if (suaraAir) suaraAir.Stop();

        // Sembunyikan semua kotak biru
        foreach (GameObject air in semuaAir)
        {
            if (air != null) air.SetActive(false);
        }
    }
}