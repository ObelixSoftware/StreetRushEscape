using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;

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
            SoundManager.Instance.PlayMenuMusic();
        }
    }

    public void PlayGame()
    {
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
