using UnityEngine;

public class MysteryBoxCollect : MonoBehaviour
{
    public GameManagerUI ui;
    public GameObject pickupVFX; // optional

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (pickupVFX != null)
            Instantiate(pickupVFX, transform.position, Quaternion.identity);

        if (ui != null)
            ui.SetMysteryCollected();

        Destroy(gameObject);
    }
}
