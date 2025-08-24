using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Slider pursuitSlider;
    public Slider timeSlider;

    public float pursuitLevel = 0f;
    public float pursuitDecayMult = 1f;

    public Text scoreText;
    public int score = 1000;

    public float startingTime = 600;
    public float globalTime;

    private bool isChaseMusicPlaying = false;

    [Header("Scoreboard")]
    public Scoreboard scoreboard;

    [Header("Game Over UI")]
    public GameOverManager gameOverManager;
    public InputField playerNameInput;
    public Button submitScoreButton;

    [Header("Highscore UI")]
    public Text highscoreText;
    public int maxScoresToShow = 6;

    private string scoreSavePath;

    private void Awake()
    {
        scoreSavePath = Path.Combine(Application.persistentDataPath, "highscores.json");
        LoadScores();
    }

    private void Start()
    {
        globalTime = startingTime;

        if (submitScoreButton != null)
            submitScoreButton.onClick.AddListener(OnSubmitScore);

        UpdateHighscoreUI();
    }

    private void Update()
    {
        if (globalTime > 0)
            globalTime -= Time.deltaTime;
        else
            GlobalTimerFinished();

        if (pursuitLevel > 0)
            pursuitLevel -= Time.deltaTime * pursuitDecayMult;

        timeSlider.value = globalTime;
        pursuitSlider.value = pursuitLevel;

        if (Input.GetKey(KeyCode.P))
            pursuitLevel += 0.1f;

        if (pursuitLevel >= 30f && !isChaseMusicPlaying && SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayChaseMusic();
            isChaseMusicPlaying = true;
        }
        else if (pursuitLevel < 30f && isChaseMusicPlaying && SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBackgroundMusic();
            isChaseMusicPlaying = false;
        }

        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    private void GlobalTimerFinished()
    {
        gameOverManager.TriggerGameOver(transform.position);
    }

    public void IncreasePursuit(float adjustment)
    {
        pursuitLevel = Mathf.Clamp(pursuitLevel + adjustment, 0f, 100f);
        Debug.Log("Pursuit: " + pursuitLevel);
    }

    public void SetPursuitDecay(float adjustment = 1f)
    {
        pursuitDecayMult = adjustment;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    private void OnSubmitScore()
    {
        string playerName = "Anonymous";
        if (playerNameInput != null && !string.IsNullOrWhiteSpace(playerNameInput.text))
            playerName = playerNameInput.text;

        AddScoreEntry(playerName, score);
        UpdateHighscoreUI();
    }

    public void AddScoreEntry(string playerName, int score)
    {
        if (scoreboard != null)
        {
            scoreboard.scores.Add(new ScoreEntry(playerName, score));
            scoreboard.scores.Sort((a, b) => b.score.CompareTo(a.score));
            SaveScores();
        }
    }

    public void UpdateHighscoreUI()
    {
        if (highscoreText == null || scoreboard == null) return;

        highscoreText.text = "";
        var scores = scoreboard.scores;

        for (int i = 0; i < Mathf.Min(maxScoresToShow, scores.Count); i++)
        {
            var entry = scores[i];
            highscoreText.text += $"{i + 1}. {entry.playerName} - {entry.score}\n";
        }
    }

    private void SaveScores()
    {
        if (scoreboard == null) return;

        string json = JsonUtility.ToJson(scoreboard, true);
        File.WriteAllText(scoreSavePath, json);
    }

    private void LoadScores()
    {
        if (File.Exists(scoreSavePath))
        {
            string json = File.ReadAllText(scoreSavePath);
            if (scoreboard != null)
                JsonUtility.FromJsonOverwrite(json, scoreboard);
        }
    }
}
