using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    public GameObject winText; // Kazandýðýnýzda gösterilecek metin veya panel  
    public int levelToUnlock;
    private int NumberOfUnlockedLevels;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Drone"))
        {
            StartCoroutine(RaceTrigger());
            NumberOfUnlockedLevels = PlayerPrefs.GetInt("LevelsUnlocked", 1);

            if (NumberOfUnlockedLevels <= levelToUnlock)
            {
                PlayerPrefs.SetInt("LevelsUnlocked", NumberOfUnlockedLevels + 1);
                PlayerPrefs.Save();
            }
        }
    }

    private IEnumerator RaceTrigger()
    {
        winText.SetActive(true);
        yield return new WaitForSeconds(3);
        LoadSceneAsync(2);
    }

    private void LoadSceneAsync(int sceneIndex)
    {
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
                operation.allowSceneActivation = true;

            yield return null;
        }
    }
}
