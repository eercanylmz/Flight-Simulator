using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isActive = false; // Checkpoint aktif mi?
    public Checkpoint nextCheckpoint; // Bir sonraki checkpoint
    public GameObject finishObject; // Finish objesi (Sadece son checkpoint için atanmalý)

    private Renderer[] renderers;

    void Start()
    {
        // Bu GameObject'in tüm çocuklarýndaki Renderer bileþenlerini al
        renderers = GetComponentsInChildren<Renderer>();
        UpdateColor();

        // Baþlangýçta finish objesini kapat
        if (finishObject != null)
        {
            finishObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Drone") && isActive)
        {
            ActivateNextCheckpoint();
        }
    }

    void UpdateColor()
    {
        // Eðer aktifse yeþil, deðilse kýrmýzý yapar.
        Color newColor = isActive ? Color.green : Color.red;
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = newColor;
        }
    }

    public void ActivateNextCheckpoint()
    {
        UpdateColor();

        if (nextCheckpoint != null)
        {
            nextCheckpoint.isActive = true;
            nextCheckpoint.UpdateColor();
        }
        else
        {
            // Eðer bu son checkpoint ise finish'i aç
            if (finishObject != null)
            {
                finishObject.SetActive(true);
            }
        }
    }
}
