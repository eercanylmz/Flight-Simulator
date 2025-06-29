using System.Collections;  // IEnumerator kullanmak i�in gerekli olan k�t�phane
using UnityEngine;         // Unity'nin temel fonksiyonlar�n� kullanmak i�in gerekli k�t�phane
using UnityEngine.SceneManagement;  // Sahne y�netimi i�in gerekli olan k�t�phane

public class Drone1 : MonoBehaviour
{
    
    private DroneController dronecontroller;  // Drone'un kontrol� i�in kullan�lacak DroneController referans�
    public Transform[] pathPoints;  // Drone'un izleyece�i yolu belirleyen nokta referanslar�
    public float speed;  // Drone'un h�z�n� belirleyen de�i�ken
    [HideInInspector]
    public int currentPoint = 0;  // Drone'un �u anda hangi noktaya y�neldi�ini g�steren indeks
    private bool hasFinished = false;  // Drone'un yolu tamamlay�p tamamlamad���n� g�steren flag

    void Start()
    {
        dronecontroller = GetComponent<DroneController>();  // DroneController bile�enini bul ve ata
        speed = Random.Range(5f, 7f);  // Drone'un h�z�n� 2 ile 7 aras�nda rastgele bir de�er olarak ayarla
    }

    void Update()
    {
        // E�er drone biti�e ula�mad�ysa ve hala yol �zerindeki noktalar� ge�iyorsa
        if (!hasFinished && currentPoint < pathPoints.Length)
        {
            // Hedef noktan�n y�n�n� hesapla
            Vector3 targetDirection = pathPoints[currentPoint].position - transform.position;

            // Yava��a hedef y�ne d�n, yeni y�n� hesapla
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed * Time.deltaTime, 0.0f);

            // Dronun y�n�n� yeni y�ne d�nd�r
            transform.rotation = Quaternion.LookRotation(newDirection);

            // Hedef noktaya do�ru belirli bir h�zda ilerle
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pathPoints[currentPoint].position, step);

            // E�er drone hedef noktaya �ok yakla�t�ysa (mesafe 0.1'den k���kse)
            if (Vector3.Distance(transform.position, pathPoints[currentPoint].position) < 0.1f)
            {
                currentPoint++;  // Sonraki hedef noktaya ge�
            }
        }
    }
}
