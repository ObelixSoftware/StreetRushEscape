using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [Header("UI")]
    public GameObject gameOverPanel;
    public Button yesButton;
    public Button noButton;

    [Header("Audio")]
    public AudioClip gameOverSound;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (yesButton != null)
            yesButton.onClick.AddListener(OnRestartPressed);
        if (noButton != null)
            noButton.onClick.AddListener(OnMenuPressed);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void TriggerGameOver(Vector3 explosionPosition)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StopMusic();
        }

        StartCoroutine(GameOverRoutine(explosionPosition));
    }

    private IEnumerator GameOverRoutine(Vector3 explosionPosition)
    {
        yield return new WaitForSeconds(2f);

        if (gameOverSound != null)
            audioSource.PlayOneShot(gameOverSound);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    private void OnRestartPressed()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBackgroundMusic();
        }
    }

    private void OnMenuPressed()
    {
        SceneManager.LoadScene("Menu");
    }
}
