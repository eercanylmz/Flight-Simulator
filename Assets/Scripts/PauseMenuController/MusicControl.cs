using UnityEngine;
using TMPro;

public class MusicControl : MonoBehaviour
{
    public AudioSource music;
    public TextMeshProUGUI btnmusic;
    public TextMeshProUGUI btnmusic2;
    private static MusicControl instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // M�zik kontrol�n� sahneler aras� koru
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        int musicState = PlayerPrefs.GetInt("MusicState", 1); // Varsay�lan 1 (m�zik a��k)

        if (musicState == 1)
        {
            music.Play();
            music.mute = false;
            btnmusic.color = Color.green;
            btnmusic2.color = Color.red;
        }
        else
        {
            music.mute = true;
            btnmusic2.color = Color.green;
            btnmusic.color = Color.red;
        }
    }

    public void OnMusic_Btn() // M�zi�i a�
    {
        if (!music.isPlaying) // E�er �alm�yorsa ba�lat
        {
            music.Play();
        }
        music.mute = false;
        btnmusic.color = Color.green;
        btnmusic2.color = Color.red;
        PlayerPrefs.SetInt("MusicState", 1); // M�zik a��k olarak kaydet
        PlayerPrefs.Save();
    }

    public void OffMusic_Btn() // M�zi�i kapat
    {
        music.mute = true; // Ses kapat
        btnmusic2.color = Color.green;
        btnmusic.color = Color.red;
        PlayerPrefs.SetInt("MusicState", 0); // M�zik kapal� olarak kaydet
        PlayerPrefs.Save();
    }
}
