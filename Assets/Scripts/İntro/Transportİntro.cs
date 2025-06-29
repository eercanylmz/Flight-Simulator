using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class TransportIntro : MonoBehaviour
{
    public Camera introCamera; // Intro sahnesinde kullanılacak kamera.
    public TextMeshProUGUI countdownText; // Geri sayım süresini göstermek için kullanılan UI metin nesnesi.
    public TextMeshProUGUI IntroText; // Geri sayım süresini göstermek için kullanılan UI metin nesnesi.
    public float countdownDuration; // Geri sayım süresini belirlemek için kullanılan değişken.
    private float countdownTimer; // Geri sayım süresi boyunca zamanlayıcı olarak kullanılacak değişken.
    public GameObject İntroButton; // İntro sırasında gösterilecek buton.

    void Start()
    {
        StartCoroutine(IntroPark());
        countdownTimer = countdownDuration; // Geri sayım zamanlayıcısını başlatır ve countdownDuration değişkenindeki değeri atar.
    }

    void Update()
    {
        // Geri sayım 50'den küçük veya eşitse butonu aktif eder.
        if (countdownTimer <= 50)
        {
            İntroButton.SetActive(true);
        }
        else
        {
            İntroButton.SetActive(false);
        }

        countdownTimer -= Time.deltaTime; // Geri sayım zamanlayıcısını, geçen zaman kadar azaltır.
        countdownText.text = Mathf.Ceil(countdownTimer).ToString(); // Geri sayım UI metnini günceller.

        // Eğer geri sayım süresi sıfır veya altına düşerse, intro'yu bitirir ve bir sonraki sahneye geçer.
        if (countdownTimer <= 2f)
        { 
            LoadSceneAsync(2); // Sahne 2'yi yükler.
        }
    }

    IEnumerator IntroPark()
    {
        string[] introMessages = {
            "Hoş geldiniz! Konteyner teslimat görevinizi tamamlamanız gerekiyor.",
            "Göreviniz, konteynerleri belirlenen noktalara sorunsuz bir şekilde taşımak.",
            "Ekrandaki ok işareti, konteyneri nereye bırakmanız gerektiğini gösterecek.",
            "Ancak dikkatli olun! Eğer yanlış noktaya teslim ederseniz baştan başlamak zorunda kalırsınız.",
            "Ayrıca, aracınız herhangi bir engele çarparsa görev sıfırlanır!",
            "Üç başarılı teslimat yapmanız gerekiyor. Her teslimat sizi sona bir adım daha yaklaştıracak.",
            "İlk teslimatınızı tamamladıktan sonra yeni bir hedef belirlenecek.",
            "Son teslimatınızı da başarıyla yaparsanız ikinci seviyeye geçeceksiniz!",
            "Dikkatli sürün, yönlendirmeleri iyi takip edin ve konteynerleri güvenli şekilde bırakın!",
            "Hazırsanız görev başlıyor! Başarılar dileriz!"
        };

        foreach (string message in introMessages)
        {
            IntroText.text = message;
            yield return new WaitForSeconds(6); // Her mesajdan sonra 6 saniye bekler.
        }
    }

    // İlk butona tıklandığında sahne 2'yi yükler.
    public void SkipIntro()
    {
        LoadSceneAsync(2); // Sahne 2'yi yükler.
    }

    // Asenkron sahne yükleme işlemi
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
            // Sahne %90'lık ilerlemeyi geçtikten sonra sahneye geçişe izin verir
            if (operation.progress >= 0.9f)
                operation.allowSceneActivation = true;

            yield return null; // Bir sonraki frame'e kadar bekler
        }
    }
}
