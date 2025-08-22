using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MissionCutsceneController : MonoBehaviour
{
    [Header("Video Setup")]
    public VideoPlayer videoPlayer;       // VideoPlayer component
    public RawImage cutsceneDisplay;      // RawImage that shows the Render Texture

    [Header("Mission Objects")]
    public GameObject missionObjects;     // Things to activate after cutscene (money bags, UI, etc.)

    private void Start()
    {
        // Hide RawImage and mission objects at start
        if (cutsceneDisplay != null)
            cutsceneDisplay.gameObject.SetActive(false);

        if (missionObjects != null)
            missionObjects.SetActive(false);
    }

    public void StartCutscene()
    {
        if (videoPlayer == null || cutsceneDisplay == null) return;

        // Mute all sounds including engine
        SoundManager.Instance?.MuteAllMusic();

        cutsceneDisplay.gameObject.SetActive(true);
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnCutsceneEnd;
    }

    private void OnCutsceneEnd(VideoPlayer vp)
    {
        cutsceneDisplay.gameObject.SetActive(false);

        // Activate mission objects
        if (missionObjects != null)
            missionObjects.SetActive(true);

        // Resume all sounds including engine
        SoundManager.Instance?.ResumeAllMusic();
        SoundManager.Instance?.ResumeEngineSound();

        videoPlayer.loopPointReached -= OnCutsceneEnd;
    }
}
