
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagerOptimized : MonoBehaviour
{
    public GameObject RacePanel;
    public GameObject ParkingPanel;
    public GameObject MissionPanel;
    public GameObject TransportPanel;
    public GameObject AbautPanel;

    public void OpenRacePanel()
    {
        Time.timeScale = 1;
        RacePanel.SetActive(true);
        MissionPanel.SetActive(false);
    }
    public void OpenParkingPanel()
    {
        Time.timeScale = 1;
        ParkingPanel.SetActive(true);
        MissionPanel.SetActive(false);
    }
    public void OpenTransportPanel()
    {
        Time.timeScale = 1;
        TransportPanel.SetActive(true);
        MissionPanel.SetActive(false);
    }
    public void OpenMissionPanel()
    {
        Time.timeScale = 1;
        TransportPanel.SetActive(false);
        RacePanel.SetActive(false);
        ParkingPanel.SetActive(false);
        MissionPanel.SetActive(true);
    }

    public void LoadSceneAsync(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(sceneIndex);
    }
    public void OpenAbautMe()
    {
        AbautPanel .SetActive(true);    
    }
    public void CloseAbautMe()
    {
        AbautPanel.SetActive(false);
    }

    public void Mission() => LoadSceneAsync(1);
    public void TransportLvlÝntro() => LoadSceneAsync(5);
    public void TransPortLvl1() => LoadSceneAsync(6);
    public void TransPortLvl2() => LoadSceneAsync(7);
    public void TransPortLvl3() => LoadSceneAsync(8);

    public void RaceLvlIntro() => LoadSceneAsync(3);
    public void RaceLvl1() => LoadSceneAsync(4);
    public void RaceLvl2() => LoadSceneAsync(13);
    public void RaceLvl3() => LoadSceneAsync(14);

    public void FreeFlight() => LoadSceneAsync(1);

    public void ParkingIntro() => LoadSceneAsync(9);
    public void ParkingLvl1() => LoadSceneAsync(10);
    public void ParkingLvl2() => LoadSceneAsync(11);
    public void ParkingLvl3() => LoadSceneAsync(12); 
}