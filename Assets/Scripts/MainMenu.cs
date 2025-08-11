using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // Volume Slider setup (only for music)
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(delegate {
                OnMusicVolumeChanged(volumeSlider.value);
            });
        }
        ApplyMusicVolume(savedVolume);
    }

    public void PlayGame()
    {
        StartCoroutine(UnlockAudioAndLoad());
    }

    private IEnumerator UnlockAudioAndLoad()
    {
        // Unlock audio for WebGL by playing silent clip once
        if (SoundManager.Instance != null)
        {
            AudioSource audioSource = SoundManager.Instance.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                AudioClip silence = AudioClip.Create("silence", 1, 1, 44100, false);
                audioSource.PlayOneShot(silence);
            }
        }

        yield return null; // Wait a frame

        // Tell SoundManager to start music after unlock
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StartMusicAfterUnlock();
        }

        // Load next scene (game)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void OnMusicVolumeChanged(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
        ApplyMusicVolume(volume);
    }

    void ApplyMusicVolume(float volume)
    {
        if (SoundManager.Instance == null) return;

        SoundManager.Instance.SetMusicVolume(volume);
    }
}
