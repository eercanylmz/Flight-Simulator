using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public Camera introCamera; // Intro sahnesinde kullan�lacak kamera
    public TextMeshProUGUI countdownText; // Geri say�m s�resini g�stermek i�in kullan�lan UI metin nesnesi
    public TextMeshProUGUI IntroText; // Intro metinlerini g�stermek i�in kullan�lan UI metin nesnesi
    public float countdownDuration; // Geri say�m s�resini belirlemek i�in kullan�lan de�i�ken
    private float countdownTimer; // Geri say�m s�resi boyunca zamanlay�c� olarak kullan�lacak de�i�ken
    public GameObject IntroButton; // Intro s�ras�nda g�sterilecek buton

    void Start()
    {
        StartCoroutine(IntroPark()); // Intro metinlerini g�stermek i�in Coroutine ba�lat�l�r
        countdownTimer = countdownDuration; // Geri say�m timer'�n� ba�lat�r
    }

    void Update()
    {
        // Geri say�m 50'den k���k veya e�itse butonu aktif eder
        if (countdownTimer <= 50)
        {
            IntroButton.SetActive(true);
        }
        else
        {
            IntroButton.SetActive(false);
        }

        countdownTimer -= Time.deltaTime; // Geri say�m zamanlay�c�s�n� azalt�r
        countdownText.text = Mathf.Ceil(countdownTimer).ToString(); // Geri say�m� UI �zerinde g�nceller

        // Geri say�m s�f�r veya alt�na d��erse, intro'yu bitirir ve yeni sahneye ge�er
        if (countdownTimer <= 2f)
        { 
            LoadSceneAsync(2); // Yeni sahneye ge�i�
        }
    }

    IEnumerator IntroPark()
    {
        string[] introMessages = {
            "Ho� geldiniz! �imdi drone'unuzu park etme zaman�. Hedefiniz, ok i�aretiyle g�sterilen park yerine ula�mak.",
            "Ekranda g�rd���n�z ok i�aretini takip ederek drone'unuzu do�ru park alan�na y�nlendirin.",
            "Drone'u dikkatli bir �ekilde park yerine y�nlendirmek i�in sol joystick�i kullan�n. H�z�n�z� kontrol edin!",
            "Park alan�na yakla��rken, sa� joystick ile drone'un y�n�n� ayarlay�n ve dikkatlice park etmeye ba�lay�n.",
            "Ok i�areti park yerini g�sterecek. O noktaya do�ru ilerleyin ve drone'unuzu yerle�tirin.",
            "Y�ksek h�zda park etmek zor olabilir. H�z�n�z� iyi ayarlay�n ve dikkatli hareket edin!",
            "Drone'unuzu park ederken, engellerden ka��nmak i�in �evrenizi iyi g�zlemleyin.",
            "Park alan�na ba�ar�l� bir �ekilde girdi�inizde g�rev tamamlanm�� olacakt�r. �imdi drone'unuzu g�venli bir �ekilde park edin!",
            "\"Drone'unuzu park ederken, hareketlerinizi dikkatli yap�n. Y�ksek h�zla gitmek park etmeyi zorla�t�rabilir.\"",
            "�imdi yeni g�revinize ba�lamak i�in haz�r olun!"
        };

        foreach (string message in introMessages)
        {
            IntroText.text = message;
            yield return new WaitForSeconds(6); // Her mesajdan sonra 6 saniye bekler
        }
    }

    // Skip butonuna t�kland���nda sahneyi y�kler
    public void SkipIntroPark()
    {
        LoadSceneAsync(2); // Yeni sahneye ge�i�
    }

    private void LoadSceneAsync(int sceneIndex)
    {
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); 

        // Sahne y�klenene kadar bekler
        while (!operation.isDone)
        {
            // Sahne %90'l�k ilerlemeyi ge�tikten sonra sahneye ge�i�e izin verir
            if (operation.progress >= 0.9f)
                operation.allowSceneActivation = true;

            yield return null; // Bir sonraki frame'e kadar bekler
        }
    }
}
