using UnityEngine;
using UnityEngine.InputSystem; // Wajib

public class WristController : MonoBehaviour
{
    [Header("Komponen")]
    public GameObject layarJam; // Panel Background yang mau di hide/show

    [Header("Input")]
    public InputActionProperty tombolToggle; // Tombol Trigger Kanan

    private bool isVisible = true; // Awalnya nyala

    void Start()
    {
        if (layarJam) layarJam.SetActive(true);
    }

    void Update()
    {
        // Cek kalau tombol Trigger Kanan ditekan
        if (tombolToggle.action.WasPressedThisFrame())
        {
            ToggleJam();
        }
    }

    void ToggleJam()
    {
        isVisible = !isVisible;
        layarJam.SetActive(isVisible);
    }
}