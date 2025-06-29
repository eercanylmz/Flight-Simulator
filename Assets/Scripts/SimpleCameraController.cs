using UnityEngine;
using UnityEngine.UI;

public class SimpleCameraController : MonoBehaviour
{
    public Camera[] cameras; // Tüm kameralarý içeren bir dizi
    private int currentCameraIndex; // Þu anda aktif olan kameranýn indeksi

    public Button switchCameraButton; // Kamera deðiþtirme butonu
    public float transitionSpeed; // Geçiþ hýzý

    private bool isTransitioning = false; // Geçiþ yapýlýp yapýlmadýðýný kontrol eder
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    public GameObject CameraJoistckButton;
    void Start()
    { 
        // Baþlangýçta tüm kameralarý kapat
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        // Ýlk kamerayý aç
        currentCameraIndex = 0;
        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
        }

        // Butonun týklama olayýna geçiþ yapma iþlevini ekle
        if (switchCameraButton != null)
        {
            switchCameraButton.onClick.AddListener(SwitchCamera);
        }
    }

    void Update()
    {
        if (currentCameraIndex == 1)
        {
            CameraJoistckButton.SetActive(true);
        }
        else
        {
            CameraJoistckButton.SetActive(false);
        }

        if (isTransitioning)
        {
            cameras[currentCameraIndex].transform.position = Vector3.Lerp(cameras[currentCameraIndex].transform.position, targetPosition, Time.deltaTime * transitionSpeed);
            cameras[currentCameraIndex].transform.rotation = Quaternion.Lerp(cameras[currentCameraIndex].transform.rotation, targetRotation, Time.deltaTime * transitionSpeed);

            if (Vector3.Distance(cameras[currentCameraIndex].transform.position, targetPosition) < 0.01f)
            {
                isTransitioning = false;
                cameras[currentCameraIndex].gameObject.SetActive(true);
            }
        }
    }

  public   void SwitchCamera()
    {
        if (!isTransitioning)
        {
            // Þu anki kamerayý kapat
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // Kamera indeksini artýr (dizi sonuna geldiðinde baþa dön)
            currentCameraIndex++;
            if (currentCameraIndex >= cameras.Length)
            {
                currentCameraIndex = 0;
            }

            // Yeni kameranýn hedef pozisyonunu ve rotasyonunu belirle
            targetPosition = cameras[currentCameraIndex].transform.position;
            targetRotation = cameras[currentCameraIndex].transform.rotation;

            // Geçiþi baþlat
            isTransitioning = true;
        }
    } 
}
