using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DroneController : MonoBehaviour
{
    
   
 

   
    public float acceleration = 10f; // Hýzlanma miktarý
    public float deceleration = 20f; // Yavaþlama miktarý
   
    public float RigtLeftSlopeAngle; // Sað-sol eðim açýsý
    public float FrontBackSlopeAngle; // Ön-arka eðim açýsý

    public float DönüsHizi = 50f; // Dönüþ hýzý
    public float ascendSpeed = 5f; // Yükselme hýzý
    public float descendSpeed = 2f; // Ýniþ hýzý
    private Quaternion lastRotation; // Son rotasyon

    private Vector3 lastPosition; // Son pozisyon
    private Vector3 currentPosition; // Mevcut pozisyon
    private Rigidbody rb; // Rigidbody bileþeni


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = 1; // Hava direnci
        rb.angularDrag = 1; // Açýsal hava direnci
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX; // X ve Z eksenlerinde rotasyonu kilitle

        lastRotation = transform.rotation; // Baþlangýçta son rotasyonu kaydet
        lastPosition = transform.position; // Baþlangýçta son pozisyonu kaydet

        
    }
    void Update()
    {  
         
        currentPosition = transform.position;

       

        lastPosition = currentPosition; // Son pozisyonu güncelle

      
    }

    void FixedUpdate()
    {
        // Dronun dengede kalmasýný saðla
        Vector3 predictedUp = Quaternion.AngleAxis(
            rb.angularVelocity.magnitude * Mathf.Rad2Deg * 0.5f / rb.drag,
            rb.angularVelocity
        ) * transform.up;

        Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
        rb.AddTorque(torqueVector * rb.drag * rb.drag);
    }

}
