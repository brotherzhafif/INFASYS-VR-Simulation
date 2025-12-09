using UnityEngine;
using System.Collections;

public class AirAnimasi : MonoBehaviour
{
    // Ini buat nyimpen data "State 2" (Data dari Screenshot kamu)
    private Vector3 scaleFlowing;
    private Vector3 posFlowing;

    public float durasi = 0.3f; // Kecepatan animasi

    void Awake()
    {
        // 1. REKAM KONDISI INSPECTOR (STATE 2)
        // Script akan mengingat posisi & scale yang kamu set di Unity sekarang
        scaleFlowing = transform.localScale;
        posFlowing = transform.localPosition;

        // 2. LANGSUNG UBAH KE STATE 1 (MATI/KOSONG)
        // Kita set scale Y jadi 0 dan posisi geser ke atas (sumber kran)
        // Rumus: Posisi Atas = Posisi Tengah + Scale Y (karena tinggi cylinder Unity itu 2 unit)
        Vector3 posisiAtas = posFlowing + new Vector3(0, scaleFlowing.y, 0);

        transform.localScale = new Vector3(scaleFlowing.x, 0, scaleFlowing.z);
        transform.localPosition = posisiAtas;

        // 3. Matikan objeknya biar gak kelihatan
        gameObject.SetActive(false);
    }

    public void Muncul() // Transisi State 1 -> State 2
    {
        gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(AnimasiMuncul());
    }

    public void Mati() // Transisi State 2 -> State 3
    {
        StopAllCoroutines();
        StartCoroutine(AnimasiJatuh());
    }

    IEnumerator AnimasiMuncul()
    {
        float timer = 0;

        // Titik Awal: Di Atas, Tipis
        Vector3 posisiAtas = posFlowing + new Vector3(0, scaleFlowing.y, 0);

        while (timer < durasi)
        {
            timer += Time.deltaTime;
            float progress = timer / durasi; // 0 sampai 1

            // Animasi Scale: Dari 0 ke Ukuran Asli (Screenshot)
            float currentScaleY = Mathf.Lerp(0, scaleFlowing.y, progress);
            transform.localScale = new Vector3(scaleFlowing.x, currentScaleY, scaleFlowing.z);

            // Animasi Posisi: Dari Atas turun ke Tengah (Screenshot)
            transform.localPosition = Vector3.Lerp(posisiAtas, posFlowing, progress);

            yield return null;
        }

        // Pastikan nilai akhirnya presisi sesuai Screenshot
        transform.localScale = scaleFlowing;
        transform.localPosition = posFlowing;
    }

    IEnumerator AnimasiJatuh()
    {
        float timer = 0;

        // Titik Akhir: Di Bawah, Tipis (Seolah air sisa jatuh ke tanah)
        Vector3 posisiBawah = posFlowing - new Vector3(0, scaleFlowing.y, 0);

        while (timer < durasi)
        {
            timer += Time.deltaTime;
            float progress = timer / durasi;

            // Animasi Scale: Dari Ukuran Asli ke 0
            float currentScaleY = Mathf.Lerp(scaleFlowing.y, 0, progress);
            transform.localScale = new Vector3(scaleFlowing.x, currentScaleY, scaleFlowing.z);

            // Animasi Posisi: Dari Tengah turun ke Bawah
            transform.localPosition = Vector3.Lerp(posFlowing, posisiBawah, progress);

            yield return null;
        }

        gameObject.SetActive(false); // Matikan objek
    }
}