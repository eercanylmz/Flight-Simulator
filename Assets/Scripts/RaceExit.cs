using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceExit : MonoBehaviour
{
    public GameObject Wasted;

    private void OnTriggerEnter(Collider other)
    {
        // Eðer çarpýþýlan nesne "Robot" etiketi taþýyorsa
        if (other.gameObject.CompareTag("Robot"))
        {
            StartCoroutine(WastedScreen());  // Wasted ekranýný gösteren Coroutine baþlat
        }
    }

    // Wasted ekranýný gösteren Coroutine
    public IEnumerator WastedScreen()
    {
        Wasted.SetActive(true);  // Wasted ekranýný aktif hale getir
        yield return new WaitForSeconds(3);  // 3 saniye bekle

        // Time.timeScale kontrolü
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        // Bellek temizleme iþlemi
        System.GC.Collect();

        // Asenkron sahne yükleme
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));  // Mevcut sahneyi asenkron olarak yeniden yükle
    }

    // Sahne yüklemek için asenkron iþlem
    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;  // Yükleme tamamlanana kadar sahne deðiþmesin

        // Sahne yüklenirken herhangi bir animasyon veya iþleme yapýlabilir (isteðe baðlý)
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;  // Yükleme tamamlandýðýnda sahneyi aktif et
            }
            yield return null;
        }
    }
}
