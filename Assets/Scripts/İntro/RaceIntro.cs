using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class RaceIntro : MonoBehaviour
{
    public Camera introCamera; // �ntro sahnesinde kullan�lacak kamera.
    public TextMeshProUGUI countdownText; // Geri say�m s�resini g�stermek i�in kullan�lan UI metin nesnesi.
    public TextMeshProUGUI IntroText; // Geri say�m s�resini g�stermek i�in kullan�lan UI metin nesnesi.
    public float countdownDuration; // Geri say�m s�resini belirlemek i�in kullan�lan de�i�ken.
    private float countdownTimer; // Geri say�m s�resi boyunca zamanlay�c� olarak kullan�lacak de�i�ken.
    public GameObject IntroButton; // �ntro s�ras�nda g�sterilecek buton.

    void Start()
    {
        StartCoroutine(IntroPark());
        countdownTimer = countdownDuration;
    }

    void Update()
    {
        if (countdownTimer <= 50)
        {
            IntroButton.SetActive(true);
        }
        else
        {
            IntroButton.SetActive(false);
        }

        countdownTimer -= Time.deltaTime;
        countdownText.text = Mathf.Ceil(countdownTimer).ToString();

            if (countdownTimer <= 2f)
            { 
                LoadSceneAsync(2);
            }
    }

    IEnumerator IntroPark()
    {
        string[] introMessages = {
            "Ho� geldiniz! Bu g�revde 4 drone ile yar��acaks�n�z. Hedefiniz, belirlenen 16 kareyi ge�erek biti� �izgisine ula�mak.",
            "Her kareyi ge�tik�e, o karenin rengi k�rm�z�dan ye�ile d�necek. Dikkatli olun, bir karenin i�inden ge�meyi unutmay�n!",
            "E�er bir kareyi ge�mezseniz, biti� �izgisi a��lmaz ve g�revi kaybedersiniz. Her kareyi ge�meye �zen g�sterin!",
            "Yar�� s�ras�nda dronunuzu �arpmalardan ka��nd�rmal�s�n�z. �arp��ma, dronun dengesini bozacak ve kontrol etmek zorla�acakt�r!",
            "E�er dronunuz bir engele �arparsa, oyuna ba�tan ba�laman�z gerekecek. Dikkatli s�r�� �ok �nemli!",
            "Yar��a ba�lamak i�in haz�r m�s�n�z? Ok i�aretini takip ederek drone�unuzu y�nlendirmeye ba�lay�n.",
            "Unutmay�n, her ge�i�te k�rm�z� kareler ye�ile d�necek. Bu ilerledi�inizi g�sterir, ama t�m kareleri ge�mek zorundas�n�z!",
            "Biti� �izgisine ula�madan �nce, t�m karelerden ge�melisiniz. T�m kareler ye�ile d�nmeli!",
            "Yar��ta her �arp��ma ve ge�i�i dikkatlice takip edin. Zorlu engelleri a�arak ba�ar�ya ula�abilirsiniz!",
            "Yar�� ba�l�yor! �imdi dronunuzu y�nlendirmeye ve t�m kareleri ge�meye odaklan�n!"
        };

        foreach (string message in introMessages)
        {
            IntroText.text = message;
            yield return new WaitForSeconds(6);
        }
    }

    public void Sk�pIntroRace()
    {
        LoadSceneAsync(2);
    }

    private void LoadSceneAsync(int sceneIndex)
    {
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); 

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
                operation.allowSceneActivation = true;

            yield return null;
        }
    }
}
