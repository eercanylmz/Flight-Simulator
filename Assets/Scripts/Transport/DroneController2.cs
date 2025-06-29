using System.Collections;
using TMPro;
using UnityEngine;

public class DroneController2 : MonoBehaviour
{
    // Pervane sesi için AudioSource bileþeni
    public AudioSource propellerAudioSource;
    // Pervane sesi dosyasý
    public AudioClip propellerSound;
    // Hedefe olan mesafeyi saklamak için static bir deðiþken
    public static float distanceFromTarget;
    // Hedefe olan mesafeyi gösteren deðiþken
    public float toTarget;
    // UI'de hedef mesafesini göstermek için TextMeshPro deðiþkeni
    public TextMeshProUGUI hText;
    // UI'de hýz bilgisini göstermek için TextMeshPro deðiþkeni
    public TextMeshProUGUI speedText;

    // Sabit joystick için deðiþken
    public FixedJoystick joystick;
    // Ýkinci joystick için deðiþken
    public FixedJoystick joystick2;

    // Maksimum hýz deðeri
    public float maxHiz = 20f;
    // Hýzlanma miktarý
    public float HizlanmaMiktari = 2f;
    // Yavaþlama miktarý
    public float YavaslamaMiktari = 50f;
    // Mevcut hýz
    private float MevcutHiz = 0f;
    // Hedef hýz
    private float HedefHiz = 0f;

    // Dönüþ hýzý
    public float DönüsHizi = 100f;
    // Yükselme hýzý
    public float YükselmeHizi = 5f;
    // Ýniþ hýzý
    public float ÝnisHýzý = 5f;
    // Son rotasyonu saklamak için deðiþken
    private Quaternion SonRotasyon;

    // Son pozisyonu saklayan deðiþken
    private Vector3 lastPosition;
    // Mevcut pozisyonu saklayan deðiþken
    private Vector3 currentPosition;
    // Rigidbody bileþeni
    private Rigidbody rb;

    // PERVANE SÝSTEMÝ
    [SerializeField] GameObject Çalistir;
    [SerializeField] GameObject Durdur;
    [SerializeField] GameObject[] Buttons;
    public GameObject[] propellers; // Pervaneler
    public float maxRotationSpeed = 500f; // Maksimum dönüþ hýzý
    public float minRotationSpeed = 0f; // Minimum dönüþ hýzý
    private float currentRotationSpeed = 0f; // Þu anki dönüþ hýzý
    private bool isRotating = false; // Pervane durumu

    public float smoothTime = 1f; // Hýz geçiþi için zaman
    private float targetRotationSpeed = 0f; // Hedef dönüþ hýzý
    private float velocity = 0f; // SmoothDamp için gerekli hýz 

    void Start()
    {
        foreach (GameObject buton in Buttons)
        {
            buton.SetActive(false);
        }
        Durdur.SetActive(false);

        rb = GetComponent<Rigidbody>(); // Rigidbody bileþenini al
        rb.drag = 1; // Hava direnci (hareketi engelleyen kuvvet)
        rb.angularDrag = 1; // Açýsal hava direnci (dönmeyi engelleyen kuvvet)
        // X ve Z eksenlerinde rotasyonu kilitle (Y ekseni serbest býrak)
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

        SonRotasyon = transform.rotation; // Baþlangýçta rotasyonu kaydet
        lastPosition = transform.position; // Baþlangýçta pozisyonu kaydet

        // Pervane sesini ayarla
        propellerAudioSource.clip = propellerSound;
        propellerAudioSource.Stop();
    }

    void Update()
    {
        // Hýzýn geçiþi sýrasýnda donmalarýn olmamasý için her bir fonksiyonu belirli zaman dilimlerinde çalýþtýrýyoruz.
        float deltaTime = Time.deltaTime;

        // Pervane sesinin pitch deðerini hýzla iliþkilendir
        propellerAudioSource.pitch = Mathf.Lerp(1.0f, 1.7f, MevcutHiz / maxHiz);

        // Dönüþ hýzýný SmoothDamp ile geçiþ yaparak ayarla
        currentRotationSpeed = Mathf.SmoothDamp(currentRotationSpeed, targetRotationSpeed, ref velocity, smoothTime);

        // Eðer pervaneler dönüyorsa
        if (isRotating)
        {
            foreach (GameObject propeller in propellers)
            {
                // Pervaneleri döndür
                propeller.transform.Rotate(Vector3.up * currentRotationSpeed * deltaTime, Space.Self);
            }
        }

        // Ýlk joystick'ten yatay ve dikey giriþ
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Ýkinci joystick'ten yatay ve dikey giriþ
        float horizontalInput2 = joystick2.Horizontal;
        float verticalInput2 = joystick2.Vertical;

        // Mevcut rotasyon hedefini mevcut rotasyon olarak ayarla
        Quaternion targetRotation = transform.rotation;
        RaycastHit hit;

        // Aþaðýya doðru bir ýþýn atarak hedefe olan mesafeyi ölç
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            toTarget = hit.distance; // Mesafeyi al
            distanceFromTarget = toTarget; // Mesafeyi güncelle
        }

        // UI'de hedef mesafesini güncelle
        hText.text = toTarget.ToString("F1") + " m";

        // Eðer joystick hareket ediyorsa, hedef hýzý maksimum yap
        if (horizontalInput != 0 || verticalInput != 0 || horizontalInput2 != 0 || verticalInput2 != 0)
        {
            HedefHiz = maxHiz;
        }
        else
        {
            HedefHiz = 0; // Eðer joystick hareket etmiyorsa, hedef hýzý sýfýrla
        }

        // Hýzýn arttýrýlmasý için hýzlanma mantýðý
        if (HedefHiz > MevcutHiz)
        {
            MevcutHiz = Mathf.MoveTowards(MevcutHiz, HedefHiz, HizlanmaMiktari * deltaTime);
        }
        // Hýzýn yavaþlatýlmasý için yavaþlama mantýðý
        else
        {
            MevcutHiz = Mathf.MoveTowards(MevcutHiz, HedefHiz, YavaslamaMiktari * deltaTime);
        }

        // Ýleri-geri ve sað-sol hareket
        Vector3 forwardMovement = transform.forward * verticalInput2 * MevcutHiz * deltaTime;
        Vector3 strafeMovement = transform.right * horizontalInput2 * MevcutHiz * deltaTime;

        // Drone'u hareket ettir
        rb.MovePosition(rb.position + forwardMovement + strafeMovement);

        // Yükselme iþlemi
        if (verticalInput > 0)
        {
            transform.Translate(Vector3.up * YükselmeHizi * deltaTime);
        }
        // Ýniþ iþlemi
        else if (verticalInput < 0)
        {
            transform.Translate(Vector3.down * ÝnisHýzý * deltaTime);
        }

        // Yatay hareket
        Vector3 tiltDirection = transform.right * Mathf.Sign(horizontalInput) * Mathf.Abs(horizontalInput);
        transform.Translate(tiltDirection * MevcutHiz * deltaTime, Space.World);

        // Ýkinci joystick ile drone'un yönünü deðiþtir
        float turnAmount = horizontalInput2 * DönüsHizi * deltaTime;
        transform.Rotate(Vector3.up, turnAmount);

        // Mevcut pozisyonu güncelle
        currentPosition = transform.position;

        // UI'de hýzý güncelle
        speedText.text = MevcutHiz.ToString("F0") + " m/s";

        lastPosition = currentPosition; // Son pozisyonu güncelle
    }

    // Pervaneleri baþlat
    public void StartPropellers()
    {
        propellerAudioSource.Play();
        isRotating = true;
        foreach (GameObject buton in Buttons)
        {
            buton.SetActive(true);
        }
        Çalistir.SetActive(false);
        Durdur.SetActive(true);

        targetRotationSpeed = maxRotationSpeed; // Hedef hýz maksimum dönüþ hýzý

        // Pervane sesinin hýzla iliþkilendirilmesi
        StartCoroutine(AdjustPropellerSoundAndSpeed());
    }

    // Pervaneleri durdur
    public void StopPropellers()
    {
        propellerAudioSource.pitch = Mathf.Lerp(.5f, 1f, MevcutHiz);
        Çalistir.SetActive(true);
        Durdur.SetActive(false);
        foreach (GameObject buton in Buttons)
        {
            buton.SetActive(false);
        }

        // Durdurma sýrasýnda hedef hýz sýfýr olmalý
        targetRotationSpeed = minRotationSpeed; // Yavaþça sýfýrlanmasý için minRotationSpeed sýfýr yapýlýr

        // Pervane sesinin hýzla iliþkilendirilmesi
        StartCoroutine(AdjustPropellerSoundAndSpeed());
    }

    // Pervane sesi ve hýzýný kademeli olarak ayarlama coroutine'i 
    private IEnumerator AdjustPropellerSoundAndSpeed()
    {
        // Pervane hýzýný kademeli olarak artýrmak veya azaltmak için smooth bir geçiþ yapýyoruz
        float currentSpeed = currentRotationSpeed;
        while (Mathf.Abs(currentRotationSpeed - targetRotationSpeed) > 0.1f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetRotationSpeed, Time.deltaTime * 2);
            currentRotationSpeed = currentSpeed;

            // Pervane sesinin pitch deðerini hýzla deðiþtirmek için
            propellerAudioSource.pitch = Mathf.Lerp(1.0f, 1.7f, Mathf.Abs(currentRotationSpeed) / maxRotationSpeed);

            yield return null; // Her frame'de geçiþi saðlamak için bekleyin
        }

        // Son hýzý ve sesi doðrulama
        currentRotationSpeed = targetRotationSpeed;
        propellerAudioSource.pitch = Mathf.Lerp(1.0f, 1.7f, Mathf.Abs(currentRotationSpeed) / maxRotationSpeed);
    }

    // Drone'un hareketini durdurma
    public void StopDroneMovement()
    {
        // Tüm hareketi durdur
        HedefHiz = 0;
        MevcutHiz = 0;
        isRotating = false;
    }
}
