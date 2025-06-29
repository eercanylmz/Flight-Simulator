using UnityEngine;

public class Prop  : MonoBehaviour
{
    public float rotationSpeed = 5000f; // Pervane d�n�� h�z�

    void Update()
    {
        // Pervaneyi y ekseni (Vector3.up) etraf�nda d�nd�r
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
