using UnityEngine;

public class CameraJoystickControl : MonoBehaviour
{
    public Transform cameraTransform;  // Kontrol edilecek kamera
    public float rotateSpeed = 3f;     // Kameranýn dönüþ hýzý
    public Joystick cameraJoystick;    // Joystick referansý
    public float minVerticalAngle = -90f; // Minimum dikey dönme açýsý (aþaðý bakýþ)
    public float maxVerticalAngle = 0f;   // Maksimum dikey dönme açýsý (düz karþýya bakýþ)

    private float horizontalInput;
    private float verticalInput;
    private float currentVerticalRotation = 0f; // Kameranýn mevcut dikey açýsý

    void Update()
    {
        // Joystick'ten yatay ve dikey eksen verilerini al
        horizontalInput = cameraJoystick.Horizontal;
        verticalInput = cameraJoystick.Vertical;

        // Kamerayý joystick hareketine göre döndür
        RotateCamera(horizontalInput, verticalInput);
    }

    void RotateCamera(float horizontal, float vertical)
    {
        // Yatay eksende saða ve sola döndürme (360 derece serbest dönüþ)
        cameraTransform.Rotate(Vector3.up, horizontal * rotateSpeed, Space.World);

        // Dikey eksende yukarý (maxVerticalAngle) ve aþaðý (minVerticalAngle) döndürme
        currentVerticalRotation -= vertical * rotateSpeed;
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, minVerticalAngle, maxVerticalAngle);

        // Kameranýn dikey açýsýný sýnýrlý bir þekilde güncelle
        cameraTransform.localEulerAngles = new Vector3(currentVerticalRotation, cameraTransform.localEulerAngles.y, 0f);
    }
}
