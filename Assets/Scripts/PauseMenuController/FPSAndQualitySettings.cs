using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class FPSAndQualitySettings : MonoBehaviour
{
    // FPS Text components
    public TextMeshProUGUI fps30Text;
    public TextMeshProUGUI fps60Text;
    public TextMeshProUGUI fps90Text;

    // Quality Text components
    public TextMeshProUGUI lowQualityText;
    public TextMeshProUGUI mediumQualityText;
    public TextMeshProUGUI highQualityText;

    void Start()
    {
        // Baþlangýçta kalite renklerini ayarla
        lowQualityText.color = Color.green;
        mediumQualityText.color = Color.red;
        highQualityText.color = Color.red;
        fps30Text.color = Color.green;
        fps60Text.color = Color.red;
        fps90Text .color = Color.red;

        // VSync'i kapatýn
        QualitySettings.vSyncCount = 0;

        // FPS Text bileþenlerine týklanabilirlik ekleyin
        AddEventTrigger(fps30Text.gameObject, () => SetFPS(30, fps30Text));
        AddEventTrigger(fps60Text.gameObject, () => SetFPS(60, fps60Text));
        AddEventTrigger(fps90Text.gameObject, () => SetFPS(90, fps90Text));

        // Quality Text bileþenlerine týklanabilirlik ekleyin
        AddEventTrigger(lowQualityText.gameObject, () => SetQuality("Low"));
        AddEventTrigger(mediumQualityText.gameObject, () => SetQuality("Medium"));
        AddEventTrigger(highQualityText.gameObject, () => SetQuality("High"));
    }

    void SetFPS(int targetFPS, TextMeshProUGUI selectedText)
    {
        // Hedef kare hýzýný ayarlayýn
        Application.targetFrameRate = targetFPS;

        // FPS Text renklerini ayarlayýn
        fps30Text.color = (selectedText == fps30Text) ? Color.green : Color.red;
        fps60Text.color = (selectedText == fps60Text) ? Color.green : Color.red;
        fps90Text.color = (selectedText == fps90Text) ? Color.green : Color.red;

        Debug.Log("FPS set to: " + targetFPS);
    }

    void SetQuality(string qualityLevel)
    {
        // Kalite seviyesini ayarla
        switch (qualityLevel)
        {
            case "Low":
                QualitySettings.SetQualityLevel(1); // 1, düþük kalite seviyesi
                lowQualityText.color = Color.green;
                mediumQualityText.color = Color.red;
                highQualityText.color = Color.red;
                break;
            case "Medium":
                QualitySettings.SetQualityLevel(2); // 2, orta kalite seviyesi
                lowQualityText.color = Color.red;
                mediumQualityText.color = Color.green;
                highQualityText.color = Color.red;
                break;
            case "High":
                QualitySettings.SetQualityLevel(5); // 5, yüksek kalite seviyesi
                lowQualityText.color = Color.red;
                mediumQualityText.color = Color.red;
                highQualityText.color = Color.green;
                break;
        }
        Debug.Log("Quality set to: " + qualityLevel);
    }

    void AddEventTrigger(GameObject obj, System.Action action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = obj.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { action(); });
        trigger.triggers.Add(entry);
    }
}
