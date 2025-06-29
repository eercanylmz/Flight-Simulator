using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DroneController : MonoBehaviour
{
    
   
 

   
    public float acceleration = 10f; // H�zlanma miktar�
    public float deceleration = 20f; // Yava�lama miktar�
   
    public float RigtLeftSlopeAngle; // Sa�-sol e�im a��s�
    public float FrontBackSlopeAngle; // �n-arka e�im a��s�

    public float D�n�sHizi = 50f; // D�n�� h�z�
    public float ascendSpeed = 5f; // Y�kselme h�z�
    public float descendSpeed = 2f; // �ni� h�z�
    private Quaternion lastRotation; // Son rotasyon

    private Vector3 lastPosition; // Son pozisyon
    private Vector3 currentPosition; // Mevcut pozisyon
    private Rigidbody rb; // Rigidbody bile�eni


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 1; // Hava direnci
        rb.angularDrag = 1; // A��sal hava direnci
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX; // X ve Z eksenlerinde rotasyonu kilitle

        lastRotation = transform.rotation; // Ba�lang��ta son rotasyonu kaydet
        lastPosition = transform.position; // Ba�lang��ta son pozisyonu kaydet

        
    }
    void Update()
    {  
         
        currentPosition = transform.position;

       

        lastPosition = currentPosition; // Son pozisyonu g�ncelle

      
    }

    void FixedUpdate()
    {
        // Dronun dengede kalmas�n� sa�la
        Vector3 predictedUp = Quaternion.AngleAxis(
            rb.angularVelocity.magnitude * Mathf.Rad2Deg * 0.5f / rb.drag,
            rb.angularVelocity
        ) * transform.up;

        Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
        rb.AddTorque(torqueVector * rb.drag * rb.drag);
    }

}
