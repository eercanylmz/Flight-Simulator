using UnityEngine;

public class CameraFollow1 : MonoBehaviour
{
    public Transform drone; // Takip edilecek drone nesnesi
    public Vector3 offset; // Drone ile kamera aras�ndaki mesafe
    public float smoothSpeed = 0.125f; // Kamera hareketinin yumu�akl�k derecesi
    public float rotationSmoothSpeed = 5f; // Kameran�n yumu�ak d�n�� h�z�

    void LateUpdate()
    {
        // Hedef pozisyon: Drone'un arkas�nda duracak �ekilde offset ile ayarlanm�� pozisyon
        Vector3 desiredPosition = drone.position + drone.rotation * offset;

        // Kameran�n pozisyonunu yumu�ak ge�i� ile drone'a yakla�t�r
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kameray� bu yeni pozisyona ta��
        transform.position = smoothedPosition;

        // Drone'un Y eksenindeki rotasyonunu takip et (sa�a-sola d�n��lerde)
        Quaternion currentRotation = transform.rotation;

        // Drone'un Z rotasyonunu s�f�rlay�n
        float droneYRotation = drone.eulerAngles.y;

        // Kameran�n rotasyonunu yumu�ak bir �ekilde drone'un Y eksenine e�itle
        Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles.x, droneYRotation, currentRotation.eulerAngles.z);

        // Kameran�n rotasyonunu yumu�ak bir �ekilde ayarlama
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}
