using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip engineSound;
    public AudioClip driftSound;
    public AudioClip explosionSound;
    public AudioClip backgroundMusic;
    public AudioClip chaseMusic;
    public AudioClip pedestrianHitSound;
    public AudioClip menuMusic;

    public AudioSource engineAudioSource;
    public AudioSource driftAudioSource;
    public AudioSource explosionAudioSource;
    public AudioSource backgroundMusicSource;
    public AudioSource chaseMusicSource;
    public AudioSource pedestrianHitSource;
    public AudioSource menuMusicSource;
    private bool audioContextUnlocked = false;
    private bool musicStarted = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // WebGL specific: Don't auto-play audio until user interaction
        #if UNITY_WEBGL && !UNITY_EDITOR
        audioContextUnlocked = false;
        #else
        audioContextUnlocked = true;
        #endif
        
        // Start audio setup after a frame to ensure clips are loaded
        StartCoroutine(SetupAudioAfterLoad());
    }
    
    private IEnumerator SetupAudioAfterLoad()
    {
        // Wait for audio clips to be fully loaded
        yield return null;
        
        // Check if audio clips are loaded before setting them up
        if (AreAudioClipsLoaded())
        {
            SetupMusicAudioSources();
            SetupPedestrianHitAudio();
            SetupEngineAudio();
            SetupDriftAudio();
            SetupExplosionAudio();
        }
        else
        {
            // Debug.LogWarning("Audio clips not fully loaded yet. Retrying...");
            // Retry after a short delay
            yield return new WaitForSeconds(0.1f);
            if (AreAudioClipsLoaded())
            {
                SetupMusicAudioSources();
                SetupPedestrianHitAudio();
                SetupEngineAudio();
                SetupDriftAudio();
                SetupExplosionAudio();
            }
            else
            {
                // Debug.LogError("Failed to load audio clips after retry");
            }
        }
    }
    
    private bool AreAudioClipsLoaded()
    {
        // Check if all required audio clips are loaded and have valid data
        bool clipsLoaded = true;
        
        if (engineSound != null && engineSound.loadState != AudioDataLoadState.Loaded)
            clipsLoaded = false;
        if (driftSound != null && driftSound.loadState != AudioDataLoadState.Loaded)
            clipsLoaded = false;
        if (explosionSound != null && explosionSound.loadState != AudioDataLoadState.Loaded)
            clipsLoaded = false;
        if (backgroundMusic != null && backgroundMusic.loadState != AudioDataLoadState.Loaded)
            clipsLoaded = false;
        if (chaseMusic != null && chaseMusic.loadState != AudioDataLoadState.Loaded)
            clipsLoaded = false;
        if (pedestrianHitSound != null && pedestrianHitSound.loadState != AudioDataLoadState.Loaded)
            clipsLoaded = false;
        if (menuMusic != null && menuMusic.loadState != AudioDataLoadState.Loaded)
            clipsLoaded = false;
            
        return clipsLoaded;
    }

    // Call this method after user interaction (like clicking a button)
    public void UnlockAudioContext()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        if (!audioContextUnlocked)
        {
            // Create a silent audio clip and play it to unlock the audio context
            AudioClip silence = AudioClip.Create("silence", 1, 1, 44100, false);
            AudioSource tempSource = gameObject.AddComponent<AudioSource>();
            tempSource.PlayOneShot(silence);
            Destroy(tempSource, 0.1f);
            
            audioContextUnlocked = true;
            // Debug.Log("WebGL Audio Context Unlocked");
            
            // Try to set up audio clips again in case they weren't loaded initially
            StartCoroutine(RetryAudioSetup());
            
            // Auto-start menu music once audio context is unlocked
            StartCoroutine(StartMenuMusicAfterUnlock());
        }
        #endif
    }
    
    private IEnumerator RetryAudioSetup()
    {
        // Wait a bit for audio clips to potentially load
        yield return new WaitForSeconds(0.2f);
        
        // Check if we can set up audio clips now
        if (AreAudioClipsLoaded())
        {
            SetupMusicAudioSources();
            SetupPedestrianHitAudio();
            SetupEngineAudio();
            SetupDriftAudio();
            SetupExplosionAudio();
            // Debug.Log("Audio clips successfully set up after retry");
        }
        else
        {
            // Debug.LogWarning("Audio clips still not loaded after retry");
        }
    }
    
    private IEnumerator StartMenuMusicAfterUnlock()
    {
        // Wait a frame to ensure audio context is fully unlocked
        yield return null;
        
        // Start playing menu music
        PlayMenuMusic();
    }
    
    void SetupEngineAudio()
    {
        if (engineAudioSource == null)
        {
            engineAudioSource = gameObject.AddComponent<AudioSource>();
            engineAudioSource.loop = true;
            engineAudioSource.playOnAwake = false;
            engineAudioSource.volume = 0.1f;
            
            // Only assign clip if it's loaded
            if (engineSound != null && engineSound.loadState == AudioDataLoadState.Loaded)
            {
                engineAudioSource.clip = engineSound;
            }
        }
    }

    void SetupDriftAudio()
    {
        if (driftAudioSource == null)
        {
            driftAudioSource = gameObject.AddComponent<AudioSource>();
            driftAudioSource.loop = true;
            driftAudioSource.playOnAwake = false;
            
            // Only assign clip if it's loaded
            if (driftSound != null && driftSound.loadState == AudioDataLoadState.Loaded)
            {
                driftAudioSource.clip = driftSound;
            }
        }
    }

    void SetupExplosionAudio()
    {
        if (explosionAudioSource == null)
        {
            explosionAudioSource = gameObject.AddComponent<AudioSource>();
            explosionAudioSource.loop = false;
            explosionAudioSource.playOnAwake = false;
            
            // Only assign clip if it's loaded
            if (explosionSound != null && explosionSound.loadState == AudioDataLoadState.Loaded)
            {
                explosionAudioSource.clip = explosionSound;
            }
        }
    }

    void SetupMusicAudioSources()
    {
        if (backgroundMusicSource == null)
        {
            backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            backgroundMusicSource.loop = true;
            backgroundMusicSource.playOnAwake = false;
            backgroundMusicSource.volume = 0.5f;
            
            // Only assign clip if it's loaded
            if (backgroundMusic != null && backgroundMusic.loadState == AudioDataLoadState.Loaded)
            {
                backgroundMusicSource.clip = backgroundMusic;
            }
            
            #if UNITY_WEBGL && !UNITY_EDITOR
            // WebGL specific: Set audio settings for better compatibility
            backgroundMusicSource.ignoreListenerPause = true;
            #endif
        }

        if (chaseMusicSource == null)
        {
            chaseMusicSource = gameObject.AddComponent<AudioSource>();
            chaseMusicSource.loop = true;
            chaseMusicSource.playOnAwake = false;
            chaseMusicSource.volume = 0.5f;
            
            // Only assign clip if it's loaded
            if (chaseMusic != null && chaseMusic.loadState == AudioDataLoadState.Loaded)
            {
                chaseMusicSource.clip = chaseMusic;
            }
            
            #if UNITY_WEBGL && !UNITY_EDITOR
            // WebGL specific: Set audio settings for better compatibility
            chaseMusicSource.ignoreListenerPause = true;
            #endif
        }

        if (menuMusicSource == null)
        {
            menuMusicSource = gameObject.AddComponent<AudioSource>();
            menuMusicSource.loop = true;
            menuMusicSource.playOnAwake = false;
            menuMusicSource.volume = 0.5f;
            
            // Only assign clip if it's loaded
            if (menuMusic != null && menuMusic.loadState == AudioDataLoadState.Loaded)
            {
                menuMusicSource.clip = menuMusic;
            }
            
            #if UNITY_WEBGL && !UNITY_EDITOR
            // WebGL specific: Set audio settings for better compatibility
            menuMusicSource.ignoreListenerPause = true;
            #endif
        }
    }

    void SetupPedestrianHitAudio()
    {
        if (pedestrianHitSource == null)
        {
            pedestrianHitSource = gameObject.AddComponent<AudioSource>();
            pedestrianHitSource.loop = false;
            pedestrianHitSource.playOnAwake = false;
            
            // Only assign clip if it's loaded
            if (pedestrianHitSound != null && pedestrianHitSound.loadState == AudioDataLoadState.Loaded)
            {
                pedestrianHitSource.clip = pedestrianHitSound;
            }
        }
    }

    // --- Engine, Drift, Explosion ---
    public void UpdateEngineSound(float speedPercent)
    {       
        if (engineAudioSource != null)
        {
            engineAudioSource.pitch = Mathf.Lerp(0.7f, 1.5f, speedPercent);
            if (!engineAudioSource.isPlaying)
                engineAudioSource.Play();
        }
    }

    public void StopEngine()
    {
        if (engineAudioSource != null && engineAudioSource.isPlaying)
            engineAudioSource.Stop();
    }

    public void PlayDrift()
    {
        if (driftAudioSource != null && !driftAudioSource.isPlaying)
            driftAudioSource.Play();
    }

    public void StopDrift()
    {
        if (driftAudioSource != null && driftAudioSource.isPlaying)
            driftAudioSource.Stop();
    }

    public void PlayExplosion()
    {
        if (explosionAudioSource != null && explosionSound != null && explosionSound.loadState == AudioDataLoadState.Loaded)
        {
            explosionAudioSource.PlayOneShot(explosionSound);
        }
        else
        {
            // Debug.LogWarning("Explosion sound not loaded or audio source missing");
        }
    }

    public void PlayPedestrianHitSound()
    {
        if (pedestrianHitSource != null && pedestrianHitSound != null && pedestrianHitSound.loadState == AudioDataLoadState.Loaded)
        {
            pedestrianHitSource.PlayOneShot(pedestrianHitSound);
        }
        else
        {
            // Debug.LogWarning("Pedestrian hit sound not loaded or audio source missing");
        }
    }

    // --- Music Controls ---
    public void PlayBackgroundMusic()
    {
        Debug.Log("PlayBackgroundMusic");
        
        #if UNITY_WEBGL && !UNITY_EDITOR
        if (!audioContextUnlocked)
        {
            Debug.Log("WebGL: Audio context not unlocked yet. Call UnlockAudioContext() after user interaction.");
            return;
        }
        #endif
        
        StopMusic();
        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying) 
             backgroundMusicSource.Play();
    }

    public void PlayChaseMusic()
    {
        Debug.Log("PlayChaseMusic");
        
        #if UNITY_WEBGL && !UNITY_EDITOR
        if (!audioContextUnlocked)
        {
            Debug.Log("WebGL: Audio context not unlocked yet. Call UnlockAudioContext() after user interaction.");
            return;
        }
        #endif
        
        StopMusic();
        if (chaseMusicSource != null && !chaseMusicSource.isPlaying)
            chaseMusicSource.Play();
    }

    public void PlayMenuMusic()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        if (!audioContextUnlocked)
        {
            // Store the intent to play menu music
            StartCoroutine(WaitForAudioContextAndPlay());
            return;
        }
        #endif
        
        // Check if menuMusicSource exists
        if (menuMusicSource == null)
        {
            return;
        }
        
        // Check if menuMusic clip is assigned
        if (menuMusicSource.clip == null)
        {
            // Try to assign the clip if it's loaded
            if (menuMusic != null && menuMusic.loadState == AudioDataLoadState.Loaded)
            {
                menuMusicSource.clip = menuMusic;
            }
            else
            {
                return;
            }
        }
        
        // Check if clip is loaded
        if (menuMusicSource.clip != null && menuMusicSource.clip.loadState != AudioDataLoadState.Loaded)
        {
            return;
        }
        
        StopMusic();
        
        if (!menuMusicSource.isPlaying) {
            menuMusicSource.Play();
        }
    }
    
    private IEnumerator WaitForAudioContextAndPlay()
    {
        // Wait for audio context to be unlocked
        while (!audioContextUnlocked)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        // Now play the menu music
        PlayMenuMusic();
    }
    


    public IEnumerator FadeMusicToGame(float duration)
    {
        if (menuMusicSource == null || backgroundMusicSource == null) yield break;

        float startMenuVol = menuMusicSource.volume;
        float startGameVol = backgroundMusicSource.volume;
        float time = 0f;

        backgroundMusicSource.volume = 0f;
        if (!backgroundMusicSource.isPlaying)
            backgroundMusicSource.Play();

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            menuMusicSource.volume = Mathf.Lerp(startMenuVol, 0f, t);
            backgroundMusicSource.volume = Mathf.Lerp(0f, startGameVol, t);

            yield return null;
        }

        menuMusicSource.Stop();
        backgroundMusicSource.volume = startGameVol;
    }

    public void StopMusic()
    {
        backgroundMusicSource?.Stop();
        chaseMusicSource?.Stop();
        menuMusicSource?.Stop();
    }
    
    public void StopAllNonMusicAudio()
    {
        StopEngine();
        StopDrift();
        if (explosionAudioSource != null && explosionAudioSource.isPlaying)
            explosionAudioSource.Stop();
        if (pedestrianHitSource != null && pedestrianHitSource.isPlaying)
            pedestrianHitSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        if (backgroundMusicSource != null)
            backgroundMusicSource.volume = volume;

        if (chaseMusicSource != null)
            chaseMusicSource.volume = volume;

        if (menuMusicSource != null)
            menuMusicSource.volume = volume;
    }

    public void StartMusicAfterUnlock()
    {
        if (!musicStarted)
        {
            PlayBackgroundMusic();
            musicStarted = true;
        }
    }

    // --- Cutscene Helpers ---
    public void MuteAllMusic()
    {
        if (backgroundMusicSource != null) backgroundMusicSource.Pause();
        if (chaseMusicSource != null) chaseMusicSource.Pause();
        if (menuMusicSource != null) menuMusicSource.Pause();
        PauseEngineSound();
        StopDrift();
    }

    public void ResumeAllMusic()
    {
        if (backgroundMusicSource != null) backgroundMusicSource.UnPause();
        if (chaseMusicSource != null) chaseMusicSource.UnPause();
        if (menuMusicSource != null) menuMusicSource.UnPause();
    }

    public void PauseEngineSound()
    {
        if (engineAudioSource != null && engineAudioSource.isPlaying)
            engineAudioSource.Pause();
    }

    public void ResumeEngineSound()
    {
        if (engineAudioSource != null)
            engineAudioSource.UnPause();
    }
}
