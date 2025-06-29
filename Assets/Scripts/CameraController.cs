using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Camera[] cameras; // T�m kameralar� i�eren bir dizi
    private int currentCameraIndex; // �u anda aktif olan kameran�n indeksi

    public Button switchCameraButton; // Kamera de�i�tirme butonu
    public float transitionSpeed; // Ge�i� h�z�
    private bool isTransitioning = false; // Ge�i� yap�l�p yap�lmad���n� kontrol eder
    private Vector3 targetPosition;
    private Quaternion targetRotation;  
    void Start()
    {

        // �rnek olarak, diziyi dolduralim. 


        // Ba�lang��ta tam kameralar� kapat
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        // �lk kameray� a�
        currentCameraIndex = 0;
        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true);
        }

        // Butonun t�klama olay�na ge�i� yapma i�levini ekle
        if (switchCameraButton != null)
        {
            switchCameraButton.onClick.AddListener(SwitchCamera);
        }
    }

    void Update()
    { 

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

    void SwitchCamera()
    {
        if (!isTransitioning)
        {
            // �u anki kameray� kapat
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // Kamera indeksini art�r (dizi sonuna geldi�inde ba�a d�n)
            currentCameraIndex++;
            if (currentCameraIndex >= cameras.Length)
            {
                currentCameraIndex = 0;
            }

            // Yeni kameran�n hedef pozisyonunu ve rotasyonunu belirle
            targetPosition = cameras[currentCameraIndex].transform.position;
            targetRotation = cameras[currentCameraIndex].transform.rotation;

            // Ge�i�i ba�lat
            isTransitioning = true;
        }
    } 
}
