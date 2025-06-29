using UnityEngine;

public class Prop  : MonoBehaviour
{
    public float rotationSpeed = 5000f; // Pervane dönüþ hýzý

    void Update()
    {
        // Pervaneyi y ekseni (Vector3.up) etrafýnda döndür
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
