using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class RaceIntro : MonoBehaviour
{
    public Camera introCamera; // Ýntro sahnesinde kullanýlacak kamera.
    public TextMeshProUGUI countdownText; // Geri sayým süresini göstermek için kullanýlan UI metin nesnesi.
    public TextMeshProUGUI IntroText; // Geri sayým süresini göstermek için kullanýlan UI metin nesnesi.
    public float countdownDuration; // Geri sayým süresini belirlemek için kullanýlan deðiþken.
    private float countdownTimer; // Geri sayým süresi boyunca zamanlayýcý olarak kullanýlacak deðiþken.
    public GameObject IntroButton; // Ýntro sýrasýnda gösterilecek buton.

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
            "Hoþ geldiniz! Bu görevde 4 drone ile yarýþacaksýnýz. Hedefiniz, belirlenen 16 kareyi geçerek bitiþ çizgisine ulaþmak.",
            "Her kareyi geçtikçe, o karenin rengi kýrmýzýdan yeþile dönecek. Dikkatli olun, bir karenin içinden geçmeyi unutmayýn!",
            "Eðer bir kareyi geçmezseniz, bitiþ çizgisi açýlmaz ve görevi kaybedersiniz. Her kareyi geçmeye özen gösterin!",
            "Yarýþ sýrasýnda dronunuzu çarpmalardan kaçýndýrmalýsýnýz. Çarpýþma, dronun dengesini bozacak ve kontrol etmek zorlaþacaktýr!",
            "Eðer dronunuz bir engele çarparsa, oyuna baþtan baþlamanýz gerekecek. Dikkatli sürüþ çok önemli!",
            "Yarýþa baþlamak için hazýr mýsýnýz? Ok iþaretini takip ederek drone’unuzu yönlendirmeye baþlayýn.",
            "Unutmayýn, her geçiþte kýrmýzý kareler yeþile dönecek. Bu ilerlediðinizi gösterir, ama tüm kareleri geçmek zorundasýnýz!",
            "Bitiþ çizgisine ulaþmadan önce, tüm karelerden geçmelisiniz. Tüm kareler yeþile dönmeli!",
            "Yarýþta her çarpýþma ve geçiþi dikkatlice takip edin. Zorlu engelleri aþarak baþarýya ulaþabilirsiniz!",
            "Yarýþ baþlýyor! Þimdi dronunuzu yönlendirmeye ve tüm kareleri geçmeye odaklanýn!"
        };

        foreach (string message in introMessages)
        {
            IntroText.text = message;
            yield return new WaitForSeconds(6);
        }
    }

    public void SkýpIntroRace()
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
