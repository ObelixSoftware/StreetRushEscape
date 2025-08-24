using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public static Scoreboard Instance;

    [Header("UI")]
    public Text collectedText; // Example: "Collected: $1,000,000 / $4,000,000"

    [Header("Win UI")]
    public WinManager winManager; // Assign your WinManager here

    [Header("Score Data")]
    public List<ScoreEntry> scores = new List<ScoreEntry>();

    private int totalBags = 0;
    private int collectedBags = 0;
    private const int totalAmount = 4000000; // Total money available

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterBag()
    {
        totalBags++;
        UpdateUI();
    }

    public void CollectBag()
    {
        collectedBags++;
        UpdateUI();

        // Add dynamic score for this collection
        scores.Add(new ScoreEntry("Player", collectedBags * (totalAmount / totalBags)));

        // Check if all bags collected
        if (collectedBags >= totalBags && totalBags > 0)
        {
            winManager?.ShowWinScreen();
        }
    }

    private void UpdateUI()
    {
        if (collectedText != null && totalBags > 0)
        {
            int moneyPerBag = totalAmount / totalBags;
            int moneyCollected = collectedBags * moneyPerBag;
            collectedText.text = $"Collected: ${moneyCollected:N0} / ${totalAmount:N0}";
        }
        else if (collectedText != null)
        {
            collectedText.text = $"Collected: $0 / ${totalAmount:N0}";
        }
    }

    public void ResetScore()
    {
        totalBags = 0;
        collectedBags = 0;
        scores.Clear();
        UpdateUI();
    }
}
