﻿using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Clips")]
    public AudioClip engineSound;
    public AudioClip driftSound;
    public AudioClip explosionSound;

    [Header("Audio Sources")]
    public AudioSource engineAudioSource;
    public AudioSource driftAudioSource;
    public AudioSource explosionAudioSource;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Set up engine audio source
        if (engineAudioSource == null)
        {
            engineAudioSource = gameObject.AddComponent<AudioSource>();
            engineAudioSource.loop = true;
            engineAudioSource.clip = engineSound;
            engineAudioSource.playOnAwake = false;
        }

        // Set up drift audio source
        if (driftAudioSource == null)
        {
            driftAudioSource = gameObject.AddComponent<AudioSource>();
            driftAudioSource.loop = true;
            driftAudioSource.clip = driftSound;
            driftAudioSource.playOnAwake = false;
        }

        // Set up explosion audio source
        if (explosionAudioSource == null)
        {
            explosionAudioSource = gameObject.AddComponent<AudioSource>();
            explosionAudioSource.loop = false;
            explosionAudioSource.clip = explosionSound;
            explosionAudioSource.playOnAwake = false;
        }

        if (engineSound != null)
            engineAudioSource.Play();
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
}
