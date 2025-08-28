using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    
    private bool audioUnlocked = false;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(delegate {
                OnMusicVolumeChanged(volumeSlider.value);
            });
        }

        ApplyMusicVolume(savedVolume);

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StopAllNonMusicAudio();
        }

        #if !UNITY_WEBGL || UNITY_EDITOR
        SoundManager.Instance.PlayMenuMusic();
        #else
        // Add click handler to the entire canvas for WebGL audio unlocking
        StartCoroutine(AddCanvasClickHandler());
        #endif
    }
    
    void Update()
    {
        // Handle keyboard input for WebGL audio unlocking
        #if UNITY_WEBGL && !UNITY_EDITOR
        if (Input.anyKeyDown && SoundManager.Instance != null)
        {
            if (!audioUnlocked)
            {
                Debug.Log("Keyboard input detected - unlocking audio context and starting menu music");
                SoundManager.Instance.UnlockAudioContext();
                StartCoroutine(StartMenuMusicAfterUnlock());
                audioUnlocked = true;
            }
        }
        #endif
    }
    
    private IEnumerator AddCanvasClickHandler()
    {
        // Wait a frame to ensure everything is set up
        yield return null;
        
        // Find the Canvas component
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            // Add a click handler to the canvas
            AddClickHandlerToCanvas(canvas);
        }
        else
        {
            Debug.LogWarning("No Canvas found for click handler");
        }
    }
    
    private void AddClickHandlerToCanvas(Canvas canvas)
    {
        // Get or add a GraphicRaycaster to the canvas
        GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
        if (raycaster == null)
        {
            raycaster = canvas.gameObject.AddComponent<GraphicRaycaster>();
        }
        
        // Add a click handler to the canvas GameObject
        Button canvasButton = canvas.gameObject.GetComponent<Button>();
        if (canvasButton == null)
        {
            canvasButton = canvas.gameObject.AddComponent<Button>();
        }
        
        // Set up the click handler
        canvasButton.onClick.AddListener(OnCanvasClicked);
        
        // Make the button transparent and cover the entire canvas
        Image buttonImage = canvasButton.GetComponent<Image>();
        if (buttonImage == null)
        {
            buttonImage = canvasButton.gameObject.AddComponent<Image>();
        }
        buttonImage.color = new Color(0, 0, 0, 0); // Transparent
        buttonImage.raycastTarget = true;
        
        // Set the button to stretch to fill the canvas
        RectTransform buttonRect = buttonImage.GetComponent<RectTransform>();
        buttonRect.anchorMin = Vector2.zero;
        buttonRect.anchorMax = Vector2.one;
        buttonRect.offsetMin = Vector2.zero;
        buttonRect.offsetMax = Vector2.zero;
        
        Debug.Log("Canvas click handler added for WebGL audio unlocking");
    }
    
    private void OnCanvasClicked()
    {
        // Only handle this once for audio unlocking
        if (!audioUnlocked && SoundManager.Instance != null)
        {
            Debug.Log("Canvas clicked - unlocking audio context and starting menu music");
            SoundManager.Instance.UnlockAudioContext();
            
            // Start menu music after a short delay
            StartCoroutine(StartMenuMusicAfterUnlock());
            
            audioUnlocked = true;
        }
    }

    public void PlayGame()
    {
        // Unlock audio context on first user interaction
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.UnlockAudioContext();
        }
        
        StartCoroutine(UnlockAudioAndLoad());
    }

    private IEnumerator UnlockAudioAndLoad()
    {
        if (SoundManager.Instance != null)
        {
            AudioSource audioSource = SoundManager.Instance.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                AudioClip silence = AudioClip.Create("silence", 1, 1, 44100, false);
                audioSource.PlayOneShot(silence);
            }
        }

        yield return null;

        if (SoundManager.Instance != null)
        {
            yield return StartCoroutine(SoundManager.Instance.FadeMusicToGame(1.5f));
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnMusicVolumeChanged(float volume)
    {
        // Unlock audio context on first user interaction
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.UnlockAudioContext();
            
            // Start playing menu music after audio context is unlocked
            #if UNITY_WEBGL && !UNITY_EDITOR
            StartCoroutine(StartMenuMusicAfterUnlock());
            #endif
        }
        
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
        ApplyMusicVolume(volume);
    }
    
    private IEnumerator StartMenuMusicAfterUnlock()
    {
        // Wait a bit for the audio context to be fully unlocked
        yield return new WaitForSeconds(0.1f);
        
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMenuMusic();
        }
    }

    void ApplyMusicVolume(float volume)
    {
        if (SoundManager.Instance == null) return;
        SoundManager.Instance.SetMusicVolume(volume);
    }
    
    // Call this after audio context is unlocked to start playing music
    public void StartMenuMusic()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMenuMusic();
        }
    }
    
    // Manual method to unlock audio and start music (useful for testing)
    public void UnlockAudioAndStartMusic()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.UnlockAudioContext();
            StartCoroutine(StartMenuMusicAfterUnlock());
        }
    }
}
