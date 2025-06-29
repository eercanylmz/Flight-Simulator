 using TMPro;  // TextMeshPro kullan�m� i�in gerekli k�t�phane
using UnityEngine;  // Unity'nin temel fonksiyonlar�n� kullanmak i�in gerekli k�t�phane
using UnityEngine.UI;   // Unity'nin UI bile�enlerini kullanmak i�in gerekli k�t�phane (Button vb.)

public class ParkingLevel : MonoBehaviour
{
    [SerializeField]

    int unlockLevelsNumber;  // Ka� seviyenin kilidinin a��ld���n� tutan de�i�ken
    public GameObject Panel;  // Seviye 1 i�in g�sterilecek panel
    public Button lvl2;
    public Button lvl3;
    public GameObject Panel2;  // Seviye 2 i�in g�sterilecek panel
    public TextMeshProUGUI LevelText;  // Ge�erli seviyeyi g�sterecek olan TextMeshPro UI elementi

    private void Start()
    {
        lvl2.enabled = false;
        lvl3.enabled = false;
        // E�er "Parking" ad�nda bir anahtar yoksa, onu 1 olarak ayarla
        if (!PlayerPrefs.HasKey("Parking"))
        {
            PlayerPrefs.SetInt("Parking", 1);  // �lk seviyeyi a��k olarak ayarla
        }

        // Ka� seviyenin kilidinin a��ld���n� oku ve LevelText'i g�ncelle
        unlockLevelsNumber = PlayerPrefs.GetInt("Parking");
        LevelText.text = PlayerPrefs.GetInt("Parking", 0).ToString();

        // Panellerin aktif olup olmayaca��n� ayarla
        SetPanelsActiveState();
    }

    private void Update()
    {
        // Her frame'de kilidi a��lm�� seviyeleri g�ncelle
        unlockLevelsNumber = PlayerPrefs.GetInt("Parking");

        // Panellerin durumunu tekrar kontrol et
        SetPanelsActiveState();
    }

    private void SetPanelsActiveState()
    {
        // E�er 1. seviyeden fazla seviye a��lm��sa, Panel'i gizle
        if (unlockLevelsNumber > 1)
        {
            Panel.SetActive(false);  // Seviye 1 i�in paneli kapat
            lvl2.enabled = true;
            PlayerPrefs.SetInt("Parking", 2);  // Ge�erli seviyeyi 1 olarak ayarla
        }
        else
        {
            lvl2.enabled = false;
            Panel.SetActive(true);  // E�er sadece 1. seviye a��ksa paneli g�ster
        }

        // E�er 2. seviyeden fazla seviye a��lm��sa, Panel2'yi gizle
        if (unlockLevelsNumber > 2)
        {
            Panel2.SetActive(false);  // Seviye 2 i�in paneli kapat
            lvl3.enabled = true;
            PlayerPrefs.SetInt("Parking", 3);  // Ge�erli seviyeyi 2 olarak ayarla
        }
        else
        {
            lvl3.enabled = false;
            Panel2.SetActive(true);  // E�er sadece 2. seviye a��ksa paneli g�ster

        }

        // E�er 3. seviyeden fazla seviye a��lm��sa, ge�erli seviyeyi 3 olarak ayarla
        if (unlockLevelsNumber > 3)
        {

            PlayerPrefs.SetInt("Parking", 3);  // Ge�erli seviyeyi 3 olarak g�ncelle
        }

        // Ge�erli seviyeyi LevelText �zerinde g�ster
        LevelText.text = PlayerPrefs.GetInt("Parking", 0).ToString();
    }

    // PlayerPrefs verilerini temizleyen ve panelleri s�f�rlayan fonksiyon
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();  // T�m PlayerPrefs verilerini sil
        Panel.SetActive(true);  // Seviye 1 i�in paneli tekrar a�
        Panel2.SetActive(true);  // Seviye 2 i�in paneli tekrar a�
        LevelText.text = "0";  // LevelText'i s�f�rla
    }
}
