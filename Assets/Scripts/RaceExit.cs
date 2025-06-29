using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceExit : MonoBehaviour
{
    public GameObject Wasted;

    private void OnTriggerEnter(Collider other)
    {
        // E�er �arp���lan nesne "Robot" etiketi ta��yorsa
        if (other.gameObject.CompareTag("Robot"))
        {
            StartCoroutine(WastedScreen());  // Wasted ekran�n� g�steren Coroutine ba�lat
        }
    }

    // Wasted ekran�n� g�steren Coroutine
    public IEnumerator WastedScreen()
    {
        Wasted.SetActive(true);  // Wasted ekran�n� aktif hale getir
        yield return new WaitForSeconds(3);  // 3 saniye bekle

        // Time.timeScale kontrol�
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        // Bellek temizleme i�lemi
        System.GC.Collect();

        // Asenkron sahne y�kleme
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));  // Mevcut sahneyi asenkron olarak yeniden y�kle
    }

    // Sahne y�klemek i�in asenkron i�lem
    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;  // Y�kleme tamamlanana kadar sahne de�i�mesin

        // Sahne y�klenirken herhangi bir animasyon veya i�leme yap�labilir (iste�e ba�l�)
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;  // Y�kleme tamamland���nda sahneyi aktif et
            }
            yield return null;
        }
    }
}
