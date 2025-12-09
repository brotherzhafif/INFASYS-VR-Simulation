using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public QuestManager manager;

    // Kita bikin variabel ini biar bisa diisi angka beda-beda di Inspector
    public int triggerUntukState = 0;

    private bool sudahAktif = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !sudahAktif)
        {
            // CEK: Apakah Quest Manager sekarang sedang di state yang sesuai dengan trigger ini?
            // (Contoh: Kalau trigger ini buat state 3, dia cuma aktif pas questState == 3)
            if (manager.currentQuestState == triggerUntukState)
            {
                Debug.Log("Trigger Aktif untuk State: " + triggerUntukState);

                manager.NextQuest(); // Lanjut ke misi berikutnya
                sudahAktif = true;   // Matikan trigger

                // Opsional: Hancurkan objek trigger biar bersih
                // Destroy(gameObject); 
            }
        }
    }
}