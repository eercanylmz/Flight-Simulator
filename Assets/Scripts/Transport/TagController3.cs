
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TagController3 : MonoBehaviour
{
    public GameObject image3;
    public GameObject cargo3; // Kargo objesi      
    public GameObject btn3;
    public GameObject winText; // Kazand���n�zda g�sterilecek metin veya panel  
    public int levelToUnlock;
    int NumberOfUnlockedLevels;
    public GameObject wastedText;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Container3"))
        {
            image3.SetActive(true);
            StartCoroutine(Winn());
            NumberOfUnlockedLevels = PlayerPrefs.GetInt("TransportCurrentLevel");
            if (NumberOfUnlockedLevels <= levelToUnlock)
            {
                PlayerPrefs.SetInt("TransportCurrentLevel", NumberOfUnlockedLevels + 1);
            }
        }
        else
        {
            StartCoroutine(Wasted());
        }
    }

    public void DropCargo()
    {
        cargo3 = GameObject.Find("container3");
        if (cargo3 != null) // Hata �nleme
        {
            cargo3.transform.SetParent(null);
            Rigidbody rb = cargo3.AddComponent<Rigidbody>(); // Kargo objesine Rigidbody ekler (d��mesini sa�lar) 
            rb.useGravity = true; // Kargoya yer �ekimi etkisi ekler  
        }
    }

    IEnumerator Winn()
    {
        winText.SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine(LoadSceneAsync(2));
    }

    IEnumerator Wasted()
    {
        wastedText.SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false; // Sahneyi hemen de�i�tirme, y�klenmesini bekle

        // Sahne y�klenirken bir y�kleme animasyonu oynatabilirsin (Opsiyonel)
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true; // Y�kleme tamamland���nda sahneyi de�i�tir
            }
            yield return null;
        }
    }
}
