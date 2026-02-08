// MysteryBoxCollect.cs
using UnityEngine;

public class MysteryBoxCollect : MonoBehaviour
{
    public GameObject pickupVFX; // optional

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player p = other.GetComponent<Player>();
            if (p != null)
                p.UnlockDash();

            if (pickupVFX != null)
                Instantiate(pickupVFX, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
