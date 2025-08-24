using System;
using System.Collections;
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

    //Time
    public float startingTime = 600;
    public float globalTime;

    private bool isChaseMusicPlaying = false;

    //Scoreboard
    private string scoreSavePath;
    private Scoreboard scoreboard = new Scoreboard();

    [Header("Game Over UI")]
    public GameOverManager gameOverManager;
    public InputField playerNameInput;
    public Button submitScoreButton;

    [Header("Highscore UI")]
    public Text highscoreText;
    public int maxScoresToShow = 6;

    private void Awake()
    {
        scoreSavePath = Path.Combine(Application.persistentDataPath, "highscores.json");
        LoadScores();
    }

    // Start is called before the first frame update
    void Start()
    {
        globalTime = startingTime;

        if (submitScoreButton != null)
        {
            submitScoreButton.onClick.AddListener(onSubmitScore);
        }

        UpdateHighscoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (globalTime > 0)
        {
            globalTime -= Time.deltaTime;
        }
        else
        {
            GlobalTimerFinished();
        }

        if (pursuitLevel > 0)
        {
            pursuitLevel -= Time.deltaTime * pursuitDecayMult;
        }

        timeSlider.value = globalTime;
        pursuitSlider.value = pursuitLevel;

        if (Input.GetKey(KeyCode.P))
        {
            pursuitLevel += 0.1f;
        }

        // Check if music should switch based on pursuit level
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
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void GlobalTimerFinished()
    {
        Debug.Log("Global Timer Finished");
        gameOverManager.TriggerGameOver(transform.position);
    }

    internal void IncreasePursuit(float adjustment)
    {
        if (pursuitLevel < 100)
        {
            if (pursuitLevel + adjustment <= 100)
            {
                pursuitLevel += adjustment;
            }
            else
            {
                pursuitLevel = 100f;
            }
        }

        Debug.Log(pursuitLevel);
    }

    internal void setPursuitDecay(float adjustment = 1f)
    {
        pursuitDecayMult = adjustment;
    }

    //Functions for scoreboard handling
    public void increaseScore(int adjustment)
    {
        score += adjustment;
    }

    
    void onSubmitScore()
    {
        string playerName = "Anonymous";
        if (playerNameInput != null && !string.IsNullOrWhiteSpace(playerNameInput.text))
        {
            playerName = playerNameInput.text;
        }

        AddScoreEntry(playerName, score);

        UpdateHighscoreUI();
    }
    public void AddScoreEntry(string playerName, int score)
    {
        scoreboard.scores.Add(new ScoreEntry(playerName, score));
        scoreboard.scores.Sort((a, b) => b.score.CompareTo(a.score));
        SaveScores();
    }

    public void UpdateHighscoreUI()
    {
        if (highscoreText == null) return;

        var scores = GetScores();

        highscoreText.text = "";

        for (int i = 0; i < Mathf.Min(maxScoresToShow, scores.Count); i++)
        {
            var entry = scores[i];
            highscoreText.text += $"{i + 1}. {entry.playerName} - {entry.score}\n";
        }
    }

    public List<ScoreEntry> GetScores()
    {
        Debug.Log("Scoreboard: " + scoreboard);
        return scoreboard.scores;
    }

    private void SaveScores()
    {
        string json = JsonUtility.ToJson(scoreboard, true);
        File.WriteAllText(scoreSavePath, json);
    }

    private void LoadScores()
    {
        if (File.Exists(scoreSavePath))
        {
            string json = File.ReadAllText(scoreSavePath);
            scoreboard = JsonUtility.FromJson<Scoreboard>(json);
        }
    }
}
