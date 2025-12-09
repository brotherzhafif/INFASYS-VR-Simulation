using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [Header("UI Komponen")]
    public TextMeshProUGUI textLayar;

    [Header("Indikator Panah")]
    public GameObject[] listPanah;

    [Header("Audio Efek")]              // <--- TAMBAHAN BARU
    public AudioSource audioSource;     // Speaker
    public AudioClip suaraMisiSelesai;  // Suara "Ting!" (Normal)
    public AudioClip suaraTamat;        // Suara "Victory!" (Akhir)

    [Header("Status")]
    public int currentQuestState = 0;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        UpdateQuestUI();
    }

    public void UpdateQuestUI()
    {
        // 1. Matikan SEMUA panah dulu biar bersih
        foreach (GameObject panah in listPanah)
        {
            if (panah != null) panah.SetActive(false);
        }

        // 2. Nyalakan panah state sekarang
        if (currentQuestState < listPanah.Length && listPanah[currentQuestState] != null)
        {
            listPanah[currentQuestState].SetActive(true);
        }

        // 3. Update Teks Sesuai Format Baru (Judul + Enter 2x + Instruksi)
        switch (currentQuestState)
        {
            case 0:
                textLayar.text = "Masuk ke Area Infasys" +
                    "\n \n" +
                    "Silakan masuk melewati gerbang depan.";
                break;

            case 1:
                textLayar.text = "Ambil HP Pintar" +
                    "\n \n" +
                    "Ambil HP di meja depan (Gunakan tombol Grip).";
                break;

            case 2:
                textLayar.text = "Simpan HP" +
                    "\n \n" +
                    "Tekan Trigger (Telunjuk Kiri) untuk menyimpan HP.";
                break;

            case 3:
                textLayar.text = "Masuk Greenhouse" +
                    "\n \n" +
                    "Masuklah ke Ruang Sanitasi (Pintu Depan GH).";
                break;

            case 4:
                textLayar.text = "Ambil Pakan" +
                    "\n \n" +
                    "Ambil karung pakan yang ada di dalam Ruang Sanitasi.";
                break;

            case 5:
                textLayar.text = "Beri Makan Lele" +
                    "\n \n" +
                    "Bawa pakan ke kolam belakang & lempar ke air.";
                break;

            case 6:
                textLayar.text = "Cek Tanaman" +
                    "\n \n" +
                    "Kembali ke depan dan dekati tanaman cabai.";
                break;

            case 7:
                textLayar.text = "Nyalakan Pompa" +
                    "\n \n" +
                    "Buka HP -> Tekan Grip untuk menyalakan air.";
                break;

            case 8:
                textLayar.text = "Hemat Air" +
                    "\n \n" +
                    "Biarkan tersiram, lalu tekan Grip lagi untuk mematikan air.";
                break;

            case 9:
                textLayar.text = "SELAMAT!" +
                    "\n \n" +
                    "Semua sistem INFASYS berjalan lancar.";
                break;
        }
    }

    public void NextQuest()
    {
        currentQuestState++;
        UpdateQuestUI();
        Debug.Log("Lanjut ke Quest: " + currentQuestState);

        // --- LOGIKA SUARA ---
        if (audioSource != null)
        {
            // Cek apakah ini misi terakhir (State 9)?
            if (currentQuestState == 9)
            {
                // Mainkan suara Victory!
                if (suaraTamat) audioSource.PlayOneShot(suaraTamat);
            }
            else
            {
                // Mainkan suara Ting biasa
                if (suaraMisiSelesai) audioSource.PlayOneShot(suaraMisiSelesai);
            }
        }
    }

    // --- FUNGSI EVENT ---
    public void CekAmbilHP() { if (currentQuestState == 1) NextQuest(); }
    public void CekSimpanHP() { if (currentQuestState == 2) NextQuest(); }
    public void CekAmbilPakan() { if (currentQuestState == 4) NextQuest(); }
}