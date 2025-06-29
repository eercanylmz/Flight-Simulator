using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BtnController : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    public AudioSource music;
    private bool musicPausedBefore = false;

    private void Start()
    {
        int musicState = PlayerPrefs.GetInt("MusicState", 1); // Varsayýlan açýk (1)

        if (musicState == 1)
        {
            music.Play();
            music.mute = false;
        }
        else
        {
            music.mute = true;
        }
    }

    public void Pause_Btn()
    {
        if (music.isPlaying)
        {
            music.Pause();
            musicPausedBefore = true;
        }
        else
        {
            musicPausedBefore = false;
        }

        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume_Btn()
    {
        if (musicPausedBefore)
        {
            music.Play();
        }

        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenSettings_Btn()
    {
        SettingsPanel.SetActive(true);
    }

    public void Close_Settings_Btn()
    {
        SettingsPanel.SetActive(false);
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

    public void OpenMissionBtn()
    {
        LoadSceneAsync(2);
    }
     

    public void ToggleMusic()
    {
        if (music.mute)
        {
            music.mute = false;
            PlayerPrefs.SetInt("MusicState", 1);
        }
        else
        {
            music.mute = true;
            PlayerPrefs.SetInt("MusicState", 0);
        }
        PlayerPrefs.Save();
    }

    public void LoadSceneAsync(int sceneIndex)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    IEnumerator LoadSceneCoroutine(int sceneIndex)
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
