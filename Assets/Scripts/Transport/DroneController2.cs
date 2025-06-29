using System.Collections;
using TMPro;
using UnityEngine;

public class DroneController2 : MonoBehaviour
{
    // Pervane sesi i�in AudioSource bile�eni
    public AudioSource propellerAudioSource;
    // Pervane sesi dosyas�
    public AudioClip propellerSound;
    // Hedefe olan mesafeyi saklamak i�in static bir de�i�ken
    public static float distanceFromTarget;
    // Hedefe olan mesafeyi g�steren de�i�ken
    public float toTarget;
    // UI'de hedef mesafesini g�stermek i�in TextMeshPro de�i�keni
    public TextMeshProUGUI hText;
    // UI'de h�z bilgisini g�stermek i�in TextMeshPro de�i�keni
    public TextMeshProUGUI speedText;

    // Sabit joystick i�in de�i�ken
    public FixedJoystick joystick;
    // �kinci joystick i�in de�i�ken
    public FixedJoystick joystick2;

    // Maksimum h�z de�eri
    public float maxHiz = 20f;
    // H�zlanma miktar�
    public float HizlanmaMiktari = 2f;
    // Yava�lama miktar�
    public float YavaslamaMiktari = 50f;
    // Mevcut h�z
    private float MevcutHiz = 0f;
    // Hedef h�z
    private float HedefHiz = 0f;

    // D�n�� h�z�
    public float D�n�sHizi = 100f;
    // Y�kselme h�z�
    public float Y�kselmeHizi = 5f;
    // �ni� h�z�
    public float �nisH�z� = 5f;
    // Son rotasyonu saklamak i�in de�i�ken
    private Quaternion SonRotasyon;

    // Son pozisyonu saklayan de�i�ken
    private Vector3 lastPosition;
    // Mevcut pozisyonu saklayan de�i�ken
    private Vector3 currentPosition;
    // Rigidbody bile�eni
    private Rigidbody rb;

    // PERVANE S�STEM�
    [SerializeField] GameObject �alistir;
    [SerializeField] GameObject Durdur;
    [SerializeField] GameObject[] Buttons;
    public GameObject[] propellers; // Pervaneler
    public float maxRotationSpeed = 500f; // Maksimum d�n�� h�z�
    public float minRotationSpeed = 0f; // Minimum d�n�� h�z�
    private float currentRotationSpeed = 0f; // �u anki d�n�� h�z�
    private bool isRotating = false; // Pervane durumu

    public float smoothTime = 1f; // H�z ge�i�i i�in zaman
    private float targetRotationSpeed = 0f; // Hedef d�n�� h�z�
    private float velocity = 0f; // SmoothDamp i�in gerekli h�z 

    void Start()
    {
        foreach (GameObject buton in Buttons)
        {
            buton.SetActive(false);
        }
        Durdur.SetActive(false);

        rb = GetComponent<Rigidbody>(); // Rigidbody bile�enini al
        rb.drag = 1; // Hava direnci (hareketi engelleyen kuvvet)
        rb.angularDrag = 1; // A��sal hava direnci (d�nmeyi engelleyen kuvvet)
        // X ve Z eksenlerinde rotasyonu kilitle (Y ekseni serbest b�rak)
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

        SonRotasyon = transform.rotation; // Ba�lang��ta rotasyonu kaydet
        lastPosition = transform.position; // Ba�lang��ta pozisyonu kaydet

        // Pervane sesini ayarla
        propellerAudioSource.clip = propellerSound;
        propellerAudioSource.Stop();
    }

    void Update()
    {
        // H�z�n ge�i�i s�ras�nda donmalar�n olmamas� i�in her bir fonksiyonu belirli zaman dilimlerinde �al��t�r�yoruz.
        float deltaTime = Time.deltaTime;

        // Pervane sesinin pitch de�erini h�zla ili�kilendir
        propellerAudioSource.pitch = Mathf.Lerp(1.0f, 1.7f, MevcutHiz / maxHiz);

        // D�n�� h�z�n� SmoothDamp ile ge�i� yaparak ayarla
        currentRotationSpeed = Mathf.SmoothDamp(currentRotationSpeed, targetRotationSpeed, ref velocity, smoothTime);

        // E�er pervaneler d�n�yorsa
        if (isRotating)
        {
            foreach (GameObject propeller in propellers)
            {
                // Pervaneleri d�nd�r
                propeller.transform.Rotate(Vector3.up * currentRotationSpeed * deltaTime, Space.Self);
            }
        }

        // �lk joystick'ten yatay ve dikey giri�
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // �kinci joystick'ten yatay ve dikey giri�
        float horizontalInput2 = joystick2.Horizontal;
        float verticalInput2 = joystick2.Vertical;

        // Mevcut rotasyon hedefini mevcut rotasyon olarak ayarla
        Quaternion targetRotation = transform.rotation;
        RaycastHit hit;

        // A�a��ya do�ru bir ���n atarak hedefe olan mesafeyi �l�
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            toTarget = hit.distance; // Mesafeyi al
            distanceFromTarget = toTarget; // Mesafeyi g�ncelle
        }

        // UI'de hedef mesafesini g�ncelle
        hText.text = toTarget.ToString("F1") + " m";

        // E�er joystick hareket ediyorsa, hedef h�z� maksimum yap
        if (horizontalInput != 0 || verticalInput != 0 || horizontalInput2 != 0 || verticalInput2 != 0)
        {
            HedefHiz = maxHiz;
        }
        else
        {
            HedefHiz = 0; // E�er joystick hareket etmiyorsa, hedef h�z� s�f�rla
        }

        // H�z�n artt�r�lmas� i�in h�zlanma mant���
        if (HedefHiz > MevcutHiz)
        {
            MevcutHiz = Mathf.MoveTowards(MevcutHiz, HedefHiz, HizlanmaMiktari * deltaTime);
        }
        // H�z�n yava�lat�lmas� i�in yava�lama mant���
        else
        {
            MevcutHiz = Mathf.MoveTowards(MevcutHiz, HedefHiz, YavaslamaMiktari * deltaTime);
        }

        // �leri-geri ve sa�-sol hareket
        Vector3 forwardMovement = transform.forward * verticalInput2 * MevcutHiz * deltaTime;
        Vector3 strafeMovement = transform.right * horizontalInput2 * MevcutHiz * deltaTime;

        // Drone'u hareket ettir
        rb.MovePosition(rb.position + forwardMovement + strafeMovement);

        // Y�kselme i�lemi
        if (verticalInput > 0)
        {
            transform.Translate(Vector3.up * Y�kselmeHizi * deltaTime);
        }
        // �ni� i�lemi
        else if (verticalInput < 0)
        {
            transform.Translate(Vector3.down * �nisH�z� * deltaTime);
        }

        // Yatay hareket
        Vector3 tiltDirection = transform.right * Mathf.Sign(horizontalInput) * Mathf.Abs(horizontalInput);
        transform.Translate(tiltDirection * MevcutHiz * deltaTime, Space.World);

        // �kinci joystick ile drone'un y�n�n� de�i�tir
        float turnAmount = horizontalInput2 * D�n�sHizi * deltaTime;
        transform.Rotate(Vector3.up, turnAmount);

        // Mevcut pozisyonu g�ncelle
        currentPosition = transform.position;

        // UI'de h�z� g�ncelle
        speedText.text = MevcutHiz.ToString("F0") + " m/s";

        lastPosition = currentPosition; // Son pozisyonu g�ncelle
    }

    // Pervaneleri ba�lat
    public void StartPropellers()
    {
        propellerAudioSource.Play();
        isRotating = true;
        foreach (GameObject buton in Buttons)
        {
            buton.SetActive(true);
        }
        �alistir.SetActive(false);
        Durdur.SetActive(true);

        targetRotationSpeed = maxRotationSpeed; // Hedef h�z maksimum d�n�� h�z�

        // Pervane sesinin h�zla ili�kilendirilmesi
        StartCoroutine(AdjustPropellerSoundAndSpeed());
    }

    // Pervaneleri durdur
    public void StopPropellers()
    {
        propellerAudioSource.pitch = Mathf.Lerp(.5f, 1f, MevcutHiz);
        �alistir.SetActive(true);
        Durdur.SetActive(false);
        foreach (GameObject buton in Buttons)
        {
            buton.SetActive(false);
        }

        // Durdurma s�ras�nda hedef h�z s�f�r olmal�
        targetRotationSpeed = minRotationSpeed; // Yava��a s�f�rlanmas� i�in minRotationSpeed s�f�r yap�l�r

        // Pervane sesinin h�zla ili�kilendirilmesi
        StartCoroutine(AdjustPropellerSoundAndSpeed());
    }

    // Pervane sesi ve h�z�n� kademeli olarak ayarlama coroutine'i 
    private IEnumerator AdjustPropellerSoundAndSpeed()
    {
        // Pervane h�z�n� kademeli olarak art�rmak veya azaltmak i�in smooth bir ge�i� yap�yoruz
        float currentSpeed = currentRotationSpeed;
        while (Mathf.Abs(currentRotationSpeed - targetRotationSpeed) > 0.1f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetRotationSpeed, Time.deltaTime * 2);
            currentRotationSpeed = currentSpeed;

            // Pervane sesinin pitch de�erini h�zla de�i�tirmek i�in
            propellerAudioSource.pitch = Mathf.Lerp(1.0f, 1.7f, Mathf.Abs(currentRotationSpeed) / maxRotationSpeed);

            yield return null; // Her frame'de ge�i�i sa�lamak i�in bekleyin
        }

        // Son h�z� ve sesi do�rulama
        currentRotationSpeed = targetRotationSpeed;
        propellerAudioSource.pitch = Mathf.Lerp(1.0f, 1.7f, Mathf.Abs(currentRotationSpeed) / maxRotationSpeed);
    }

    // Drone'un hareketini durdurma
    public void StopDroneMovement()
    {
        // T�m hareketi durdur
        HedefHiz = 0;
        MevcutHiz = 0;
        isRotating = false;
    }
}
