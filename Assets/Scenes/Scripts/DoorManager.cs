using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [Header("Komponen")]
    public Animator pintuAnimator;
    public AudioSource sumberSuara; // Speaker di pintu

    [Header("File Suara")]
    public AudioClip suaraBuka;  // File suara pas buka
    public AudioClip suaraTutup; // File suara pas tutup

    private bool statusTerbuka = false;

    public void TogglePintu()
    {
        // 1. Balik Status
        statusTerbuka = !statusTerbuka;

        // 2. Gerakkan Animasi
        pintuAnimator.SetBool("isOpen", statusTerbuka);

        // 3. Mainkan Suara (Cek statusnya)
        if (statusTerbuka)
        {
            // Kalau lagi BUKA, mainkan suara buka
            if (sumberSuara && suaraBuka)
                sumberSuara.PlayOneShot(suaraBuka);
        }
        else
        {
            // Kalau lagi TUTUP, mainkan suara tutup
            if (sumberSuara && suaraTutup)
                sumberSuara.PlayOneShot(suaraTutup);
        }
    }
}