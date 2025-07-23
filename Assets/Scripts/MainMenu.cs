using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
