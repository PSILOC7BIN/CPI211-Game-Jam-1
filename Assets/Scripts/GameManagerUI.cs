using UnityEngine;
using TMPro;

public class GameManagerUI : MonoBehaviour
{
    public TMP_Text mysteryStatusText;

    void Start()
    {
        
    }

    public void SetMysteryCollected()
    {

        mysteryStatusText.text = "Collected";
        mysteryStatusText.color = Color.green;
    }
}
