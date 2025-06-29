
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TagController : MonoBehaviour
{
    public GameObject image1;
    public GameObject cargo; // Kargo objesi 
    public GameObject cargo2; // Kargo objesi      
    public GameObject btn1;
    public GameObject btn2;
    public GameObject wastedText;
    public bool active1 = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Container"))
        {
            image1.SetActive(true);
            cargo2.SetActive(true);
        }
        else
        {
            image1.SetActive(false);
            StartCoroutine(Wasted());

        }
    }
    public void DropCargo()
    {

        cargo = GameObject.Find("container");
        active1 = true;
        cargo.transform.SetParent(null);
        Rigidbody rb = cargo.AddComponent<Rigidbody>(); // Kargo objesine Rigidbody ekler (düþmesini saðlar) 
        rb.useGravity = true; // Kargoya yer çekimi etkisi ekler  
        btn1.SetActive(false);
        btn2.SetActive(true);

    }
    IEnumerator Wasted()
    {
        wastedText.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
