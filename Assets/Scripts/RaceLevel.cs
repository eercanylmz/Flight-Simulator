using TMPro;  // TextMeshPro kullanýmý için gerekli kütüphane
using UnityEngine;  // Unity'nin temel fonksiyonlarýný kullanmak için gerekli kütüphane
using UnityEngine.UI;   // Unity'nin UI bileþenlerini kullanmak için gerekli kütüphane (Button vb.)

public class RaceLevel : MonoBehaviour
{
    [SerializeField]
    
    int unlockLevelsNumber;  // Kaç seviyenin kilidinin açýldýðýný tutan deðiþken
    public GameObject Panel;  // Seviye 1 için gösterilecek panel
    public Button lvl2;
    public Button lvl3;
    public GameObject Panel2;  // Seviye 2 için gösterilecek panel
    public TextMeshProUGUI LevelText;  // Geçerli seviyeyi gösterecek olan TextMeshPro UI elementi

    private void Start()
    {
        lvl2.enabled = false;
        lvl3.enabled = false;
        // Eðer "Parking" adýnda bir anahtar yoksa, onu 1 olarak ayarla
        if (!PlayerPrefs.HasKey("LevelsUnlocked"))
        {
            PlayerPrefs.SetInt("LevelsUnlocked", 1);  // Ýlk seviyeyi açýk olarak ayarla
        }

        // Kaç seviyenin kilidinin açýldýðýný oku ve LevelText'i güncelle
        unlockLevelsNumber = PlayerPrefs.GetInt("LevelsUnlocked");
        LevelText.text = PlayerPrefs.GetInt("LevelsUnlocked", 0).ToString(); 

        // Panellerin aktif olup olmayacaðýný ayarla
        SetPanelsActiveState();
    }

    private void Update()
    {
        // Her frame'de kilidi açýlmýþ seviyeleri güncelle
        unlockLevelsNumber = PlayerPrefs.GetInt("LevelsUnlocked"); 

        // Panellerin durumunu tekrar kontrol et
        SetPanelsActiveState();
    }

    private void SetPanelsActiveState()
    {
        // Eðer 1. seviyeden fazla seviye açýlmýþsa, Panel'i gizle
        if (unlockLevelsNumber > 1)
        {
            Panel.SetActive(false);  // Seviye 1 için paneli kapat
            lvl2.enabled = true;
            PlayerPrefs.SetInt("LevelsUnlocked", 2);  // Geçerli seviyeyi 1 olarak ayarla
        }
        else
        {
            lvl2.enabled = false;
            Panel.SetActive(true);  // Eðer sadece 1. seviye açýksa paneli göster
        }

        // Eðer 2. seviyeden fazla seviye açýlmýþsa, Panel2'yi gizle
        if (unlockLevelsNumber > 2)
        {
            Panel2.SetActive(false);  // Seviye 2 için paneli kapat
            lvl3.enabled = true;
            PlayerPrefs.SetInt("LevelsUnlocked", 3);  // Geçerli seviyeyi 2 olarak ayarla
        }
        else
        {
            lvl3.enabled = false;
            Panel2.SetActive(true);  // Eðer sadece 2. seviye açýksa paneli göster

        }

        // Eðer 3. seviyeden fazla seviye açýlmýþsa, geçerli seviyeyi 3 olarak ayarla
        if (unlockLevelsNumber > 3)
        {

            PlayerPrefs.SetInt("LevelsUnlocked", 3);  // Geçerli seviyeyi 3 olarak güncelle
        }

        // Geçerli seviyeyi LevelText üzerinde göster
        LevelText.text = PlayerPrefs.GetInt("LevelsUnlocked", 0).ToString();
    }

    // PlayerPrefs verilerini temizleyen ve panelleri sýfýrlayan fonksiyon
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();  // Tüm PlayerPrefs verilerini sil
        Panel.SetActive(true);  // Seviye 1 için paneli tekrar aç
        Panel2.SetActive(true);  // Seviye 2 için paneli tekrar aç
        LevelText.text = "0";  // LevelText'i sýfýrla
    }
}
