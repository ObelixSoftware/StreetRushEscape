using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;

    [Header("UI Elements")]
    public GameObject winPanel;          // Panel that displays "You Won"
    public Text titleText;               // Main title, e.g., "You Won!"
    public Text subtitleText;            // Subtext, e.g., "To be continued..."
    public Button continueButton;        // Button to go back to Menu

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (winPanel != null)
            winPanel.SetActive(false); // Hide at start
    }

    public void ShowWinScreen()
    {
        if (winPanel != null)
            winPanel.SetActive(true);

        // Stop game in the background
        Time.timeScale = 0f;

        // Optionally stop sounds
        SoundManager.Instance?.StopMusic();
        SoundManager.Instance?.StopEngine();
        SoundManager.Instance?.StopDrift();
    }

    private void Start()
    {
        if (continueButton != null)
            continueButton.onClick.AddListener(OnContinueClicked);
    }

    private void OnContinueClicked()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene("Menu"); // Load main menu
    }
}
