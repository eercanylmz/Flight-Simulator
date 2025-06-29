using UnityEngine;

public class CameraJoystickControl : MonoBehaviour
{
    public Transform cameraTransform;  // Kontrol edilecek kamera
    public float rotateSpeed = 3f;     // Kameran�n d�n�� h�z�
    public Joystick cameraJoystick;    // Joystick referans�
    public float minVerticalAngle = -90f; // Minimum dikey d�nme a��s� (a�a�� bak��)
    public float maxVerticalAngle = 0f;   // Maksimum dikey d�nme a��s� (d�z kar��ya bak��)

    private float horizontalInput;
    private float verticalInput;
    private float currentVerticalRotation = 0f; // Kameran�n mevcut dikey a��s�

    void Update()
    {
        // Joystick'ten yatay ve dikey eksen verilerini al
        horizontalInput = cameraJoystick.Horizontal;
        verticalInput = cameraJoystick.Vertical;

        // Kameray� joystick hareketine g�re d�nd�r
        RotateCamera(horizontalInput, verticalInput);
    }

    void RotateCamera(float horizontal, float vertical)
    {
        // Yatay eksende sa�a ve sola d�nd�rme (360 derece serbest d�n��)
        cameraTransform.Rotate(Vector3.up, horizontal * rotateSpeed, Space.World);

        // Dikey eksende yukar� (maxVerticalAngle) ve a�a�� (minVerticalAngle) d�nd�rme
        currentVerticalRotation -= vertical * rotateSpeed;
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, minVerticalAngle, maxVerticalAngle);

        // Kameran�n dikey a��s�n� s�n�rl� bir �ekilde g�ncelle
        cameraTransform.localEulerAngles = new Vector3(currentVerticalRotation, cameraTransform.localEulerAngles.y, 0f);
    }
}
