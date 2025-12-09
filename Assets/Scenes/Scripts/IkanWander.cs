using UnityEngine;
using System.Collections;

public class IkanWander : MonoBehaviour
{
    [Header("Setting Santai")]
    public float speedSantai = 0.5f;
    public float rotSantai = 50f;

    [Header("Setting Bar-Bar (Makan)")]
    public float speedRusuh = 8f;   // Kecepatan gila
    public float rotRusuh = 800f;   // Muter gila
    public float getaranRusuh = 0.05f; // Seberapa kuat getarannya
    public float durasiRusuh = 1f;  // Berapa detik gilanya

    // Variable internal
    private float currentSpeed;
    private float currentRot;
    private Vector3 startPos;
    private float randomOffset;
    private bool sedangRusuh = false;

    void Start()
    {
        startPos = transform.position;
        // Biar geraknya gak barengan kayak robot
        randomOffset = Random.Range(0f, 10f);

        // Mulai mode santai
        KembaliTenang();
    }

    void Update()
    {
        // 1. Gerakan Naik Turun (Sinus)
        // Kalau rusuh, frekuensi naik turunnya jadi cepet banget (x15)
        float freq = sedangRusuh ? 15f : 1f;
        float amplitude = sedangRusuh ? 0.1f : 0.05f;

        float newY = startPos.y + Mathf.Sin(Time.time * currentSpeed * freq + randomOffset) * amplitude;

        // Simpan posisi dasar
        Vector3 targetPos = new Vector3(transform.position.x, newY, transform.position.z);

        // 2. Gerakan Muter
        transform.Rotate(Vector3.up, currentRot * Time.deltaTime * Mathf.Sin(Time.time + randomOffset));

        // 3. Efek Getar (Jitter) pas Rusuh
        if (sedangRusuh)
        {
            // Tambah posisi acak biar kelihatan kejang-kejang
            transform.position = targetPos + (Random.insideUnitSphere * getaranRusuh);
        }
        else
        {
            transform.position = targetPos;
        }
    }

    // Fungsi ini dipanggil Script Manager nanti
    public void KasiMakan()
    {
        if (!sedangRusuh) StartCoroutine(ModeRusuh());
    }

    IEnumerator ModeRusuh()
    {
        sedangRusuh = true;
        currentSpeed = speedRusuh;
        currentRot = rotRusuh;
        yield return new WaitForSeconds(durasiRusuh); // Tunggu 1 detik
        KembaliTenang();
    }

    void KembaliTenang()
    {
        sedangRusuh = false;
        currentSpeed = speedSantai;
        currentRot = rotSantai;
    }
}