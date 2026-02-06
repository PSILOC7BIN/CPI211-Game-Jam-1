using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("KillPlane triggered by: " + other.name);

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
