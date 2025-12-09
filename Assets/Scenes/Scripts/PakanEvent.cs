using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Wajib biar kenal Grab

public class PakanEvent : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        // Cari komponen Grab di diri sendiri
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        // Pasang kuping: Kalau dipegang (SelectEntered), jalankan fungsi LaporBos
        grabInteractable.selectEntered.AddListener(LaporBos);
    }

    void OnDisable()
    {
        // Copot kuping kalau objek mati (biar gak error)
        grabInteractable.selectEntered.RemoveListener(LaporBos);
    }

    // Ini fungsi yang dijalankan pas di-Grab
    private void LaporBos(SelectEnterEventArgs args)
    {
        // Panggil QuestManager lewat jalur statis (Singleton)
        // Jadi gak perlu tarik-tarik kabel di Inspector
        if (QuestManager.instance != null)
        {
            QuestManager.instance.CekAmbilPakan();
        }
    }
}