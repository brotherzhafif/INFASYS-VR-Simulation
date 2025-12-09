using UnityEngine;

public class KolamManager : MonoBehaviour
{
    [Header("Efek-Efek")]
    public AudioSource audioCipratan;  // Drag Audio Source 1 kesini
    public AudioSource audioRusuh;     // Drag Audio Source 2 kesini
    public ParticleSystem partikelAir; // Drag Partikel kesini (kalo ada)

    [Header("Pasukan Lele")]
    public IkanWander[] pasukanLele; // Kita masukin semua lele disini

    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah benda yg masuk punya Tag "Pakan"?
        if (other.gameObject.CompareTag("Pakan"))
        {
            Debug.Log("Pakan Masuk! GASSPOLL!");

            // 1. Mainkan Suara & Efek
            if (audioCipratan) audioCipratan.Play();
            if (audioRusuh) audioRusuh.Play();
            if (partikelAir) partikelAir.Play();

            // 2. Perintahkan SEMUA lele untuk rusuh
            foreach (IkanWander lele in pasukanLele)
            {
                if (lele != null) lele.KasiMakan();
            }

            // Cek State 5
            if (QuestManager.instance.currentQuestState == 5)
            {
                QuestManager.instance.NextQuest(); // Lanjut ke State 6 (Tanaman)
            }

            Destroy(other.gameObject);

            // 3. Hancurkan Pakan
            Destroy(other.gameObject);
        }
    }
}