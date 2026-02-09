using UnityEngine;

public class FinishZone : MonoBehaviour
{
    public GameObject endScreenUI;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            endScreenUI.SetActive(true);
            Time.timeScale = 0f; //this will make sure the game is paused when player reaches the end zone
        }
    }
}
