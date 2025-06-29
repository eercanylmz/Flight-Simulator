using UnityEngine;

public class CameraFollow1 : MonoBehaviour
{
    public Transform drone; // Takip edilecek drone nesnesi
    public Vector3 offset; // Drone ile kamera arasýndaki mesafe
    public float smoothSpeed = 0.125f; // Kamera hareketinin yumuþaklýk derecesi
    public float rotationSmoothSpeed = 5f; // Kameranýn yumuþak dönüþ hýzý

    void LateUpdate()
    {
        // Hedef pozisyon: Drone'un arkasýnda duracak þekilde offset ile ayarlanmýþ pozisyon
        Vector3 desiredPosition = drone.position + drone.rotation * offset;

        // Kameranýn pozisyonunu yumuþak geçiþ ile drone'a yaklaþtýr
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kamerayý bu yeni pozisyona taþý
        transform.position = smoothedPosition;

        // Drone'un Y eksenindeki rotasyonunu takip et (saða-sola dönüþlerde)
        Quaternion currentRotation = transform.rotation;

        // Drone'un Z rotasyonunu sýfýrlayýn
        float droneYRotation = drone.eulerAngles.y;

        // Kameranýn rotasyonunu yumuþak bir þekilde drone'un Y eksenine eþitle
        Quaternion targetRotation = Quaternion.Euler(currentRotation.eulerAngles.x, droneYRotation, currentRotation.eulerAngles.z);

        // Kameranýn rotasyonunu yumuþak bir þekilde ayarlama
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}
