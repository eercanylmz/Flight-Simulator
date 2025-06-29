using System.Collections; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitParking : MonoBehaviour
{
    [SerializeField] GameObject YouWinText;
    public int levelToUnlock;
    int NumberOfUnlockedLevels;
    public bool isActive; 
    private void Start()
    { 
        isActive = false;
        YouWinText.SetActive(false);
    }
    private void Update()
    {
        if (isActive == true)
        {
            NumberOfUnlockedLevels = PlayerPrefs.GetInt("Parking");
            if (NumberOfUnlockedLevels <= levelToUnlock)
            {
                PlayerPrefs.SetInt("Parking", NumberOfUnlockedLevels + 1);
            } 
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Drone"))
        {
             
            isActive = true;
            StartCoroutine(YouWÝn());
        } 
    }
    IEnumerator YouWÝn()
    {
        YouWinText.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }
}
