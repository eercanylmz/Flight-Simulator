using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public Camera introCamera; // Intro sahnesinde kullanýlacak kamera
    public TextMeshProUGUI countdownText; // Geri sayým süresini göstermek için kullanýlan UI metin nesnesi
    public TextMeshProUGUI IntroText; // Intro metinlerini göstermek için kullanýlan UI metin nesnesi
    public float countdownDuration; // Geri sayým süresini belirlemek için kullanýlan deðiþken
    private float countdownTimer; // Geri sayým süresi boyunca zamanlayýcý olarak kullanýlacak deðiþken
    public GameObject IntroButton; // Intro sýrasýnda gösterilecek buton

    void Start()
    {
        StartCoroutine(IntroPark()); // Intro metinlerini göstermek için Coroutine baþlatýlýr
        countdownTimer = countdownDuration; // Geri sayým timer'ýný baþlatýr
    }

    void Update()
    {
        // Geri sayým 50'den küçük veya eþitse butonu aktif eder
        if (countdownTimer <= 50)
        {
            IntroButton.SetActive(true);
        }
        else
        {
            IntroButton.SetActive(false);
        }

        countdownTimer -= Time.deltaTime; // Geri sayým zamanlayýcýsýný azaltýr
        countdownText.text = Mathf.Ceil(countdownTimer).ToString(); // Geri sayýmý UI üzerinde günceller

        // Geri sayým sýfýr veya altýna düþerse, intro'yu bitirir ve yeni sahneye geçer
        if (countdownTimer <= 2f)
        { 
            LoadSceneAsync(2); // Yeni sahneye geçiþ
        }
    }

    IEnumerator IntroPark()
    {
        string[] introMessages = {
            "Hoþ geldiniz! Þimdi drone'unuzu park etme zamaný. Hedefiniz, ok iþaretiyle gösterilen park yerine ulaþmak.",
            "Ekranda gördüðünüz ok iþaretini takip ederek drone'unuzu doðru park alanýna yönlendirin.",
            "Drone'u dikkatli bir þekilde park yerine yönlendirmek için sol joystick’i kullanýn. Hýzýnýzý kontrol edin!",
            "Park alanýna yaklaþýrken, sað joystick ile drone'un yönünü ayarlayýn ve dikkatlice park etmeye baþlayýn.",
            "Ok iþareti park yerini gösterecek. O noktaya doðru ilerleyin ve drone'unuzu yerleþtirin.",
            "Yüksek hýzda park etmek zor olabilir. Hýzýnýzý iyi ayarlayýn ve dikkatli hareket edin!",
            "Drone'unuzu park ederken, engellerden kaçýnmak için çevrenizi iyi gözlemleyin.",
            "Park alanýna baþarýlý bir þekilde girdiðinizde görev tamamlanmýþ olacaktýr. Þimdi drone'unuzu güvenli bir þekilde park edin!",
            "\"Drone'unuzu park ederken, hareketlerinizi dikkatli yapýn. Yüksek hýzla gitmek park etmeyi zorlaþtýrabilir.\"",
            "Þimdi yeni görevinize baþlamak için hazýr olun!"
        };

        foreach (string message in introMessages)
        {
            IntroText.text = message;
            yield return new WaitForSeconds(6); // Her mesajdan sonra 6 saniye bekler
        }
    }

    // Skip butonuna týklandýðýnda sahneyi yükler
    public void SkipIntroPark()
    {
        LoadSceneAsync(2); // Yeni sahneye geçiþ
    }

    private void LoadSceneAsync(int sceneIndex)
    {
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); 

        // Sahne yüklenene kadar bekler
        while (!operation.isDone)
        {
            // Sahne %90'lýk ilerlemeyi geçtikten sonra sahneye geçiþe izin verir
            if (operation.progress >= 0.9f)
                operation.allowSceneActivation = true;

            yield return null; // Bir sonraki frame'e kadar bekler
        }
    }
}
