using System.Collections;  // IEnumerator kullanmak için gerekli olan kütüphane
using UnityEngine;         // Unity'nin temel fonksiyonlarýný kullanmak için gerekli kütüphane
using UnityEngine.SceneManagement;  // Sahne yönetimi için gerekli olan kütüphane

public class Drone1 : MonoBehaviour
{
    
    private DroneController dronecontroller;  // Drone'un kontrolü için kullanýlacak DroneController referansý
    public Transform[] pathPoints;  // Drone'un izleyeceði yolu belirleyen nokta referanslarý
    public float speed;  // Drone'un hýzýný belirleyen deðiþken
    [HideInInspector]
    public int currentPoint = 0;  // Drone'un þu anda hangi noktaya yöneldiðini gösteren indeks
    private bool hasFinished = false;  // Drone'un yolu tamamlayýp tamamlamadýðýný gösteren flag

    void Start()
    {
        dronecontroller = GetComponent<DroneController>();  // DroneController bileþenini bul ve ata
        speed = Random.Range(5f, 7f);  // Drone'un hýzýný 2 ile 7 arasýnda rastgele bir deðer olarak ayarla
    }

    void Update()
    {
        // Eðer drone bitiþe ulaþmadýysa ve hala yol üzerindeki noktalarý geçiyorsa
        if (!hasFinished && currentPoint < pathPoints.Length)
        {
            // Hedef noktanýn yönünü hesapla
            Vector3 targetDirection = pathPoints[currentPoint].position - transform.position;

            // Yavaþça hedef yöne dön, yeni yönü hesapla
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed * Time.deltaTime, 0.0f);

            // Dronun yönünü yeni yöne döndür
            transform.rotation = Quaternion.LookRotation(newDirection);

            // Hedef noktaya doðru belirli bir hýzda ilerle
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pathPoints[currentPoint].position, step);

            // Eðer drone hedef noktaya çok yaklaþtýysa (mesafe 0.1'den küçükse)
            if (Vector3.Distance(transform.position, pathPoints[currentPoint].position) < 0.1f)
            {
                currentPoint++;  // Sonraki hedef noktaya geç
            }
        }
    }
}
