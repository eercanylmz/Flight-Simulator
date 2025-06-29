using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isActive = false; // Checkpoint aktif mi?
    public Checkpoint nextCheckpoint; // Bir sonraki checkpoint
    public GameObject finishObject; // Finish objesi (Sadece son checkpoint i�in atanmal�)

    private Renderer[] renderers;

    void Start()
    {
        // Bu GameObject'in t�m �ocuklar�ndaki Renderer bile�enlerini al
        renderers = GetComponentsInChildren<Renderer>();
        UpdateColor();

        // Ba�lang��ta finish objesini kapat
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
        // E�er aktifse ye�il, de�ilse k�rm�z� yapar.
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
            // E�er bu son checkpoint ise finish'i a�
            if (finishObject != null)
            {
                finishObject.SetActive(true);
            }
        }
    }
}
