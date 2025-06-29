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
            DontDestroyOnLoad(gameObject); // Müzik kontrolünü sahneler arasý koru
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        int musicState = PlayerPrefs.GetInt("MusicState", 1); // Varsayýlan 1 (müzik açýk)

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

    public void OnMusic_Btn() // Müziði aç
    {
        if (!music.isPlaying) // Eðer çalmýyorsa baþlat
        {
            music.Play();
        }
        music.mute = false;
        btnmusic.color = Color.green;
        btnmusic2.color = Color.red;
        PlayerPrefs.SetInt("MusicState", 1); // Müzik açýk olarak kaydet
        PlayerPrefs.Save();
    }

    public void OffMusic_Btn() // Müziði kapat
    {
        music.mute = true; // Ses kapat
        btnmusic2.color = Color.green;
        btnmusic.color = Color.red;
        PlayerPrefs.SetInt("MusicState", 0); // Müzik kapalý olarak kaydet
        PlayerPrefs.Save();
    }
}
