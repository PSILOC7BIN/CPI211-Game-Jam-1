using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("respawning at checkpoint");
            player.Respawn();
        }
    }
}

}
