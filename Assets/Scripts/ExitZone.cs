using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitZone : MonoBehaviour
{
    [Header("UI")]
    public GameObject exitUI;      // The "Score Display" UI
    public Text scoreText;        // Displays collected score

    private bool triggeredGameOver = false;

    private void Start()
    {
        if (exitUI != null)
            exitUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Car") && !triggeredGameOver)
        {
            triggeredGameOver = true;
            ShowScore();
            StartCoroutine(ShowScoreThenGameOver());
        }
    }

    private void ShowScore()
    {
        if (exitUI != null)
            exitUI.SetActive(true);

        if (scoreText != null && Scoreboard.Instance != null)
        {
            int totalBags = Scoreboard.Instance.scores.Count;
            int totalAmount = 0;

            foreach (var entry in Scoreboard.Instance.scores)
            {
                totalAmount += entry.score;
            }

            scoreText.text = $"You Collected: ${totalAmount:N0}\nBags: {totalBags}";
        }

        Time.timeScale = 0f; // Pause game during score display
    }

    private IEnumerator ShowScoreThenGameOver()
    {
        yield return new WaitForSecondsRealtime(10f);

        if (exitUI != null)
            exitUI.SetActive(false);

        if (GameOverManager.Instance != null)
        {
            GameOverManager.Instance.TriggerGameOver(transform.position);
            Time.timeScale = 0f; // Ensure game stays paused
        }
    }
}
