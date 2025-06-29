using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransportLvl2 : MonoBehaviour
{
    public GameObject image3;
    public GameObject cargo3; // Kargo objesi 
    public GameObject button3;
    public GameObject win;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Container3"))
        {
            button3.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        button3.SetActive(false);
    }
    public void DropCargo3()
    {
        button3.SetActive(false);
        image3.SetActive(true);
        // Kargo objesinin pozisyonunu ve ba�l�l���n� g�nceller
        cargo3.transform.SetParent(null); // Kargonun drone'a ba�l�l���n� kald�r�r 
        Rigidbody rb = cargo3.AddComponent<Rigidbody>(); // Kargo objesine Rigidbody ekler (d��mesini sa�lar) 
        rb.useGravity = true; // Kargoya yer �ekimi etkisi ekler  
        win.SetActive(true);
        StartCoroutine(Win());
    }
    IEnumerator Win()
    {
        win.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(10);
    }
}
