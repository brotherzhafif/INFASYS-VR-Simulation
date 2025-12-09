using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class OrganicTurn : MonoBehaviour
{
    [Header("Setting Putaran")]
    public float maxSpeed = 60f;      // Kecepatan maksimal
    public float smoothness = 5f;     // Semakin kecil semakin licin/lambat berhentinya

    [Header("Input")]
    public InputActionProperty rightHandTurn; // Masukin Reference Joystick Kanan
    public XROrigin rig; // Masukin XR Origin (Player)

    private float currentSpeed = 0f;

    void Start()
    {
        if (rig == null) rig = GetComponentInParent<XROrigin>();
    }

    void Update()
    {
        // 1. Baca Input Joystick Kanan (Nilai -1 sampai 1)
        float inputVal = rightHandTurn.action.ReadValue<Vector2>().x;

        // 2. Hitung Target Kecepatan
        float targetSpeed = inputVal * maxSpeed;

        // 3. EFEK ORGANIC: Lerp (Perubahan Bertahap)
        // Ini yang bikin efek "Slow In - Slow Out" kayak kepala beneran
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * smoothness);

        // 4. Putar Player
        // Kita putar objek Camera Offset atau Origin-nya
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            rig.transform.Rotate(0, currentSpeed * Time.deltaTime, 0);
        }
    }
}