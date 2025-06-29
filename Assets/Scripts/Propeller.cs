    using Unity.VisualScripting;  // Unity'de görsel scripting araçlarýný kullanmak için gerekli kütüphane
using UnityEngine;
using UnityEngine.UI;  // Unity'nin temel fonksiyonlarýný kullanmak için gerekli kütüphane

public class Propeller : MonoBehaviour
{
    [SerializeField] GameObject  Çalistir;
    [SerializeField] GameObject  Durdur;
    [SerializeField] GameObject[] Buttons;
    public GameObject[] propellers; // Pervaneler
    public float maxRotationSpeed = 500f; // Maksimum dönüþ hýzý
    public float minRotationSpeed = 0f; // Minimum dönüþ hýzý
    private float currentRotationSpeed = 0f; // Þu anki dönüþ hýzý
    private bool isRotating = false; // Pervane durumu

    public float smoothTime = 1f; // Hýz geçiþi için zaman
    private float targetRotationSpeed = 0f; // Hedef dönüþ hýzý
    private float velocity = 0f; // SmoothDamp için gerekli hýz

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
        // Dönüþ hýzýný SmoothDamp ile geçiþ yaparak ayarla
        currentRotationSpeed = Mathf.SmoothDamp(currentRotationSpeed, targetRotationSpeed, ref velocity, smoothTime);

        // Eðer pervaneler dönüyorsa
        if (isRotating)
        {
            foreach (GameObject propeller in propellers)
            {
                
                // Pervaneleri döndür
                propeller.transform.Rotate(Vector3.up * currentRotationSpeed * Time.deltaTime, Space.Self);
            }
        }
    }

    // Pervaneleri baþlat
    public void StartPropellers()
    {
        foreach (GameObject buton in Buttons)
        {
            buton.SetActive(true );
        }
        Çalistir.SetActive(false);
        Durdur.SetActive(true);
        isRotating = true;
        targetRotationSpeed = maxRotationSpeed; // Hedef hýz maksimum dönüþ hýzý
    }

    // Pervaneleri durdur
    public void StopPropellers()
    {
        Çalistir.SetActive(true);
        Durdur.SetActive(false);
        foreach (GameObject buton in Buttons)
        {
            buton.SetActive(false);
        }
        // Durdurma sýrasýnda hedef hýz sýfýr olmalý
        targetRotationSpeed = minRotationSpeed; // Yavaþça sýfýrlanmasý için minRotationSpeed sýfýr yapýlýr
    }
}

