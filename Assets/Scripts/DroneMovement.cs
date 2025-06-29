
using System.Collections;
using TMPro;
using UnityEngine; 
using UnityEngine.SceneManagement; 

public class DroneMovement : MonoBehaviour
{
    public AudioSource propellerAudioSource; // Pervane sesi için AudioSource
    public AudioClip propellerSound; // Pervane sesi 
    public static float distanceFromTarget;
    public float toTarget;
    public TextMeshProUGUI hText; // Hedef mesafesini göstermek için UI metni
    public TextMeshProUGUI speedText; // Hızı göstermek için UI metni
    private float currentSpeed = 0f; // Mevcut hız  
    public float maxSpeed = 50f; // Maksimum hız 
    [SerializeField] GameObject WastedText;



    private DroneControls controls;
    private Vector2 moveInput;   // Sol joystick
    private Vector2 lookInput;   // Sağ joystick

    public float moveSpeed = 5f;
    public float ascendSpeed = 3f;
    public float turnSpeed = 100f;
    public float tiltAmount = 10f;  // Drone'un ne kadar eğileceğini kontrol eder
    public float tiltSpeed = 5f;    // Eğilme hızını kontrol eder
    public float acceleration = 5f; // Hızlanma için ayar
    public float deceleration = 3f; // Yavaşlama için ayar

    private Vector3 currentVelocity = Vector3.zero;
    private float currentAscendSpeed = 0f; // Yükselme/Alçalma hızını kontrol etmek için
    private float targetTiltX = 0f;  // X eksenindeki eğilme miktarı
    private float targetTiltZ = 0f;  // Z eksenindeki eğilme miktarı

    // Sol ve Sağ Joystick'ler
    public FixedJoystick fixedJoystickLeft; // Sol joystick referansı
    public FixedJoystick fixedJoystickRight; // Sağ joystick referansı

    private void Awake()
    {
        controls = new DroneControls();

        // Sol Joystick -> Hareket (WASD veya Joystick Left Stick)
        controls.Drone.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Drone.Move.canceled += ctx => moveInput = Vector2.zero;

        // Sağ Joystick -> Yükselme / Alçalma ve Dönüş
        controls.Drone.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Drone.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void Start()
    {
        // AudioSource ayarları
        propellerAudioSource.clip = propellerSound; // Pervane sesi ataması  
    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            toTarget = hit.distance; // Hedefe olan mesafeyi al
            distanceFromTarget = toTarget; // Uzaktan ölçülen mesafeyi güncelle
        }
        hText.text = toTarget.ToString("F1")   ; // Hedef mesafesini UI metnine yazdır  

        // Drone'un hızını hesapla
        float horizontalMovement = fixedJoystickLeft.Horizontal * moveSpeed; // Sol joystick X ekseni
        float verticalMovement = fixedJoystickLeft.Vertical * moveSpeed; // Sol joystick Y ekseni
        float ascendDescendInput = fixedJoystickRight.Vertical; // Sağ joystick Y ekseni (yükselme/ alçalma)

        // Yumuşak hızlanma ve yavaşlama
        Vector3 targetVelocity = transform.forward * verticalMovement + transform.right * horizontalMovement;
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * (moveInput.magnitude > 0 ? acceleration : deceleration));

        // Hareketi uygula
        transform.position += currentVelocity * Time.deltaTime;

        // Yükselme / Alçalma hızını uygula
        currentAscendSpeed = Mathf.Lerp(currentAscendSpeed, ascendDescendInput * ascendSpeed, Time.deltaTime * 5f);
        transform.position += Vector3.up * currentAscendSpeed * Time.deltaTime;

        // Hızı hesapla (iki boyutlu hareket ve yükselme/ alçalma dahil)
        currentSpeed = currentVelocity.magnitude + Mathf.Abs(currentAscendSpeed);

        // Hızı UI'ye yazdır
        speedText.text = currentSpeed.ToString("F0") + " m/s"; // Hızı UI metnine yazdır

        // Pervane sesinin pitch değerini hız ile ilişkilendir
        propellerAudioSource.pitch = Mathf.Lerp(1.0f, 1.7f, currentSpeed / maxSpeed); // Hız arttıkça pitch değerini arttır

        // Sağ Joystick ile dönüş (X ekseni)
        float turnAmount = fixedJoystickRight.Horizontal * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turnAmount, 0);

        // Drone'un eğilmesini hesapla (Artık hareket yönüne göre eğilecek)
        if (verticalMovement > 0) // İleri hareket
        {
            targetTiltX = tiltAmount;  // İleri doğru eğilme (Pitch)
        }
        else if (verticalMovement < 0) // Geri hareket
        {
            targetTiltX = -tiltAmount;   // Geri doğru eğilme (Pitch)
        }
        else
        {
            targetTiltX = 0f; // Hareket yoksa eğilme olmasın
        }

        targetTiltZ = -horizontalMovement * tiltAmount;

        // Yumuşak eğilme efekti
        Quaternion targetRotation = Quaternion.Euler(targetTiltX, transform.eulerAngles.y, targetTiltZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other .gameObject.CompareTag("Wasted"))
        {
            StartCoroutine(Wastedd());
        }
    } 
    IEnumerator  Wastedd()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        WastedText.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(currentSceneName);
    } 
}
