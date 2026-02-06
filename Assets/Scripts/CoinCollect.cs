using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coin trigger hit by: " + other.name);

        if (!other.CompareTag("Player"))
            return;

        Debug.Log("Collected coin!");

        if (CoinCounter.Instance != null)
            CoinCounter.Instance.Add(1);
        else
            Debug.LogError("CoinCounter.Instance is NULL (no CoinCounter in scene).");

        Destroy(gameObject);
    }
}
