using System.Collections;  
using UnityEngine;
using UnityEngine.SceneManagement;

public class TagController1 : MonoBehaviour
{ 
    public GameObject image3; 
    public GameObject cargo3; // Kargo objesi 
    public GameObject button3; 
    public GameObject win ; 
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
        // Kargo objesinin pozisyonunu ve baðlýlýðýný günceller
        cargo3.transform.SetParent(null); // Kargonun drone'a baðlýlýðýný kaldýrýr 
        Rigidbody rb = cargo3.AddComponent<Rigidbody>(); // Kargo objesine Rigidbody ekler (düþmesini saðlar) 
        rb.useGravity = true; // Kargoya yer çekimi etkisi ekler   
        StartCoroutine(Win());
    }
    IEnumerator Win()
    {
        win.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2); 
    }
}
