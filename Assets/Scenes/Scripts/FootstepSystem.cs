using UnityEngine;
using UnityEngine.InputSystem; // WAJIB: Biar bisa baca Joystick

public class FootstepSystem : MonoBehaviour
{
    [Header("Komponen")]
    public AudioSource kakiSource;
    public CharacterController playerBody;

    [Header("Input")]
    // Kita butuh referensi ke Action "Move" di Controller Kiri
    public InputActionProperty inputGerak;

    [Header("Setting")]
    public float stepInterval = 0.5f;
    public float minSpeed = 0.5f;
    public LayerMask layerLantai;

    [Header("Koleksi Suara")]
    public AudioClip[] suaraRumput;
    public AudioClip[] suaraBatu;
    public AudioClip[] suaraDefault;

    private float stepTimer;

    void Start()
    {
        if (playerBody == null)
            playerBody = GetComponentInParent<CharacterController>();
    }

    void Update()
    {
        // 1. Baca data dari Joystick (Vector2: X dan Y)
        Vector2 inputJoystick = inputGerak.action.ReadValue<Vector2>();

        // 2. Cek apakah Joystick sedang didorong? (Magnitude > 0.1 biar gak sensitif drift)
        bool isJoystickGerak = inputJoystick.magnitude > 0.1f;

        // 3. Hitung kecepatan Horizontal badan (Fisika)
        Vector3 horizontalVelocity = new Vector3(playerBody.velocity.x, 0, playerBody.velocity.z);

        // SYARAT BARU: 
        // Bunyi cuma keluar kalau: (Badan Bergerak Cepat) DAN (Joystick Sedang Ditekan)
        if (horizontalVelocity.magnitude > minSpeed && isJoystickGerak)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0)
            {
                BunyikanLangkah();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0.1f;
        }
    }

    void BunyikanLangkah()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, 3.0f, layerLantai))
        {
            switch (hit.collider.tag)
            {
                case "Grass":
                    PlayRandomSound(suaraRumput);
                    break;
                case "Stone":
                    PlayRandomSound(suaraBatu);
                    break;
                default:
                    PlayRandomSound(suaraDefault);
                    break;
            }
        }
    }

    void PlayRandomSound(AudioClip[] clips)
    {
        if (clips != null && clips.Length > 0)
        {
            int index = Random.Range(0, clips.Length);
            kakiSource.pitch = Random.Range(0.9f, 1.1f);
            kakiSource.PlayOneShot(clips[index]);
        }
    }
}