using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public static Scoreboard Instance;

    [Header("UI")]
    public Text collectedText; // e.g. "Collected: 0 / 5"

    [Header("Win UI")]
    public WinManager winManager; // Assign your WinManager here

    private int totalBags = 0;
    private int collectedBags = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Called by MoneyBagItem on Start
    public void RegisterBag()
    {
        totalBags++;
        UpdateUI();
    }

    // Called by MoneyBagItem when collected
    public void CollectBag()
    {
        collectedBags++;
        UpdateUI();

        // Check if all bags are collected
        if (collectedBags >= totalBags)
        {
            winManager?.ShowWinScreen();
        }
    }

    private void UpdateUI()
    {
        if (collectedText != null)
            collectedText.text = $"Collected: {collectedBags} / {totalBags}";
    }

    // Optional: reset for new game
    public void ResetScore()
    {
        totalBags = 0;
        collectedBags = 0;
        UpdateUI();
    }
}
