using UnityEngine;
using System.Collections;

public class PakanSpawner : MonoBehaviour
{
    [Header("Setting Spawner")]
    public GameObject prefabPakan;          // Masukin file Prefab Pakan disini
    public Transform posisiSpawn;           // Titik munculnya (meja)

    [Header("Aturan Main")]
    public float waktuMuncul = 2f;          // Berapa detik baru muncul lagi
    public float batasJatuh = -10f;         // Kalau jatuh di bawah Y ini, reset

    private GameObject pakanAktif;          // Pakan yang lagi ada di dunia
    private bool sedangRespawn = false;

    void Start()
    {
        SpawnPakan();
    }

    void Update()
    {
        // KONDISI 1: Pakan sudah dimakan (Destroyed by KolamManager)
        // Kalau pakanAktif jadi null (hilang) dan kita belum proses respawn...
        if (pakanAktif == null && !sedangRespawn)
        {
            StartCoroutine(ProsesRespawn());
        }

        // KONDISI 2: Pakan jatuh kejauhan / Terlempar jauh
        // Kalau pakannya ada TAPI posisinya nyemplung ke bawah tanah...
        else if (pakanAktif != null && pakanAktif.transform.position.y < batasJatuh)
        {
            // Hancurkan pakannya, nanti otomatis masuk ke KONDISI 1
            Destroy(pakanAktif);
        }
    }

    IEnumerator ProsesRespawn()
    {
        sedangRespawn = true;
        // Tunggu sebentar biar gak kaget tiba-tiba muncul
        yield return new WaitForSeconds(waktuMuncul);
        SpawnPakan();
        sedangRespawn = false;
    }

    void SpawnPakan()
    {
        // Munculin (Instantiate) pakan baru dari cetakan prefab
        pakanAktif = Instantiate(prefabPakan, posisiSpawn.position, posisiSpawn.rotation);

        // Pastikan namanya tetep "Pakan" (kadang Unity nambahin "(Clone)")
        // Ini penting biar KolamManager tetep ngenalin Tag-nya
        pakanAktif.name = "Pakan";
    }
}