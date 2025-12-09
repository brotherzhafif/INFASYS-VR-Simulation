using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    public AirAnimasi[] semuaAir; 
    public AudioSource suaraAir; 

    private bool isNyala = false;

    public void ToggleWater()
    {
        isNyala = !isNyala;

        if (isNyala)
        {
            // --- AIR MENYALA ---
            HidupkanVisual();
            
            // LOGIKA QUEST (State 7: Nyalakan Pompa)
            // Kalau quest sekarang adalah 7, lanjut ke 8
            if (QuestManager.instance.currentQuestState == 7)
            {
                QuestManager.instance.NextQuest();
            }
        }
        else
        {
            // --- AIR MATI ---
            MatikanVisual();

            // LOGIKA QUEST (State 8: Matikan Pompa)
            // Kalau quest sekarang adalah 8, lanjut ke 9 (TAMAT)
            if (QuestManager.instance.currentQuestState == 8)
            {
                QuestManager.instance.NextQuest();
            }
        }
    }

    void HidupkanVisual()
    {
        if(suaraAir) suaraAir.Play();
        foreach (AirAnimasi air in semuaAir) { if(air != null) air.Muncul(); }
        Debug.Log("AIR MENYALA!");
    }

    void MatikanVisual()
    {
        if(suaraAir) suaraAir.Stop();
        foreach (AirAnimasi air in semuaAir) { if(air != null) air.Mati(); }
        Debug.Log("AIR MATI!");
    }
}