using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Clips")]
    public AudioClip engineSound;
    public AudioClip driftSound;
    public AudioClip explosionSound;
    public AudioClip backgroundMusic;         // General background music
    public AudioClip chaseMusic;              // Police chase music
    public AudioClip pedestrianHitSound;      // Pedestrian hit sound

    [Header("Audio Sources")]
    public AudioSource engineAudioSource;
    public AudioSource driftAudioSource;
    public AudioSource explosionAudioSource;
    public AudioSource backgroundMusicSource;
    public AudioSource chaseMusicSource;

    private AudioSource pedestrianHitSource; // For hit sound

    private bool isChaseMusicPlaying = false;

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

        SetupMusicAudioSources();
        SetupPedestrianHitAudio();
    }

    void Start()
    {
        SetupEngineAudio();
        SetupDriftAudio();
        SetupExplosionAudio();

        PlayBackgroundMusic();  // Start with general music

        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SetMusicVolume(savedVolume);
    }

    void SetupEngineAudio()
    {
        if (engineAudioSource == null)
        {
            engineAudioSource = gameObject.AddComponent<AudioSource>();
            engineAudioSource.loop = true;
            engineAudioSource.clip = engineSound;
            engineAudioSource.playOnAwake = false;
        }

        if (engineSound != null)
            engineAudioSource.Play();
    }

    void SetupDriftAudio()
    {
        if (driftAudioSource == null)
        {
            driftAudioSource = gameObject.AddComponent<AudioSource>();
            driftAudioSource.loop = true;
            driftAudioSource.clip = driftSound;
            driftAudioSource.playOnAwake = false;
        }
    }

    void SetupExplosionAudio()
    {
        if (explosionAudioSource == null)
        {
            explosionAudioSource = gameObject.AddComponent<AudioSource>();
            explosionAudioSource.loop = false;
            explosionAudioSource.clip = explosionSound;
            explosionAudioSource.playOnAwake = false;
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
            backgroundMusicSource.clip = backgroundMusic;
        }

        if (chaseMusicSource == null)
        {
            chaseMusicSource = gameObject.AddComponent<AudioSource>();
            chaseMusicSource.loop = true;
            chaseMusicSource.playOnAwake = false;
            chaseMusicSource.volume = 0.5f;
            chaseMusicSource.clip = chaseMusic;
        }
    }

    void SetupPedestrianHitAudio()
    {
        if (pedestrianHitSource == null)
        {
            pedestrianHitSource = gameObject.AddComponent<AudioSource>();
            pedestrianHitSource.loop = false;
            pedestrianHitSource.playOnAwake = false;
            pedestrianHitSource.clip = pedestrianHitSound;
        }
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }

        if (chaseMusicSource != null && chaseMusicSource.isPlaying)
        {
            chaseMusicSource.Stop();
            isChaseMusicPlaying = false;
        }
    }

    public void PlayChaseMusic()
    {
        if (chaseMusicSource != null && !chaseMusicSource.isPlaying)
        {
            chaseMusicSource.Play();
            isChaseMusicPlaying = true;
        }

        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
    }

    public void StopMusic()
    {
        if (backgroundMusicSource != null && backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
        if (chaseMusicSource != null && chaseMusicSource.isPlaying)
        {
            chaseMusicSource.Stop();
        }
    }

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
        if (explosionAudioSource != null)
            explosionAudioSource.PlayOneShot(explosionSound);
    }

    public void PlayPedestrianHitSound()
    {
        if (pedestrianHitSource != null && pedestrianHitSound != null)
        {
            pedestrianHitSource.PlayOneShot(pedestrianHitSound);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (backgroundMusicSource != null)
            backgroundMusicSource.volume = volume;

        if (chaseMusicSource != null)
            chaseMusicSource.volume = volume;
    }
}
