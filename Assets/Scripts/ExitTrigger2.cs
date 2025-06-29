using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ExitTrigger2 : MonoBehaviour
{
    public GameObject YouWinText;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Drone"))
        {
            StartCoroutine(WinCoroutine());
        }
    }

    private IEnumerator WinCoroutine()
    {
        YouWinText.SetActive(true);
        yield return new WaitForSeconds(3);
        LoadSceneAsync(5);
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
