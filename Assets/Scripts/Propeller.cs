    using Unity.VisualScripting;  // Unity'de g�rsel scripting ara�lar�n� kullanmak i�in gerekli k�t�phane
using UnityEngine;
using UnityEngine.UI;  // Unity'nin temel fonksiyonlar�n� kullanmak i�in gerekli k�t�phane

public class Propeller : MonoBehaviour
{
    [SerializeField] GameObject  �alistir;
    [SerializeField] GameObject  Durdur;
    [SerializeField] GameObject[] Buttons;
    public GameObject[] propellers; // Pervaneler
    public float maxRotationSpeed = 500f; // Maksimum d�n�� h�z�
    public float minRotationSpeed = 0f; // Minimum d�n�� h�z�
    private float currentRotationSpeed = 0f; // �u anki d�n�� h�z�
    private bool isRotating = false; // Pervane durumu

    public float smoothTime = 1f; // H�z ge�i�i i�in zaman
    private float targetRotationSpeed = 0f; // Hedef d�n�� h�z�
    private float velocity = 0f; // SmoothDamp i�in gerekli h�z

    private void Start()
    {
        foreach (GameObject buton in Buttons)
        {
            buton.SetActive(false);
        }
        Durdur.SetActive(false);
    }
    void Update()
    {
        // D�n�� h�z�n� SmoothDamp ile ge�i� yaparak ayarla
        currentRotationSpeed = Mathf.SmoothDamp(currentRotationSpeed, targetRotationSpeed, ref velocity, smoothTime);

        // E�er pervaneler d�n�yorsa
        if (isRotating)
        {
            foreach (GameObject propeller in propellers)
            {
                
                // Pervaneleri d�nd�r
                propeller.transform.Rotate(Vector3.up * currentRotationSpeed * Time.deltaTime, Space.Self);
            }
        }
    }

    // Pervaneleri ba�lat
    public void StartPropellers()
    {
        foreach (GameObject buton in Buttons)
        {
            buton.SetActive(true );
        }
        �alistir.SetActive(false);
        Durdur.SetActive(true);
        isRotating = true;
        targetRotationSpeed = maxRotationSpeed; // Hedef h�z maksimum d�n�� h�z�
    }

    // Pervaneleri durdur
    public void StopPropellers()
    {
        �alistir.SetActive(true);
        Durdur.SetActive(false);
        foreach (GameObject buton in Buttons)
        {
            buton.SetActive(false);
        }
        // Durdurma s�ras�nda hedef h�z s�f�r olmal�
        targetRotationSpeed = minRotationSpeed; // Yava��a s�f�rlanmas� i�in minRotationSpeed s�f�r yap�l�r
    }
}

