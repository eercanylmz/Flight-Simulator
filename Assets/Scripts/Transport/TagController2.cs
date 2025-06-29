






using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TagController2 : MonoBehaviour
{
    public GameObject image2;
    public GameObject cargo2; // Kargo objesi 
    public GameObject cargo3; // Kargo objesi      
    public GameObject btn2;
    public GameObject btn3;
    public GameObject wastedText;
    public bool active2 = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Container2"))
        {
            image2.SetActive(true);
            cargo3.SetActive(true);
        }
        else
        {
            StartCoroutine(Wasted());
        }
    }
    public void DropCargo()
    {

        cargo2 = GameObject.Find("container2");
        active2 = true;
        cargo2.transform.SetParent(null);
        Rigidbody rb = cargo2.AddComponent<Rigidbody>(); // Kargo objesine Rigidbody ekler (düþmesini saðlar) 
        rb.useGravity = true; // Kargoya yer çekimi etkisi ekler  
        btn2.SetActive(false);
        btn3.SetActive(true);
    }
    IEnumerator Wasted()
    {
        wastedText.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}









