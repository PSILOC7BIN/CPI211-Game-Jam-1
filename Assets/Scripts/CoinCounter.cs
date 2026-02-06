using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter Instance { get; private set; }

    [SerializeField] private TMP_Text countText;

    private int count;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        count = 0;
        Refresh();
    }

    public void Add(int amount)
    {
        count += amount;
        Refresh();
    }

    private void Refresh()
    {
        if (countText != null)
            countText.text = count.ToString();
    }
}
