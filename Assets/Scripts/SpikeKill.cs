using UnityEngine;

public class SpikeKill : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (!col.collider.CompareTag("Player")) return;

        Player p = col.collider.GetComponent<Player>();
        if (p != null)
            p.Respawn();
    }
}
