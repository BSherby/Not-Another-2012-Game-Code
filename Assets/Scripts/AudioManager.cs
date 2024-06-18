using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip[] musicTracks;
    public AudioSource musicSource;
    public AudioSource explosionSource;
    public AudioSource hitBlockSource1;
    public AudioSource hitBlockSource2;

    public float explosionVolumeMultiplier = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        InitializeAudio();
        PlayRandomMusic();
    }

    public void PlayRandomMusic()
    {
        if (musicTracks.Length > 0 && musicSource != null)
        {
            int index = Random.Range(0, musicTracks.Length);
            musicSource.clip = musicTracks[index];
            musicSource.Play();
        }
        else
        {
            Debug.LogError("No music tracks assigned or MusicSource is missing.");
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void ResumeMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void PlayExplosionSound()
    {
        if (explosionSource != null)
        {
            explosionSource.Play();
        }
        else
        {
            Debug.LogError("ExplosionSource is missing.");
        }
    }

    public void InitializeAudio()
    {
        
        if (musicSource == null)
        {
            musicSource = GameObject.FindWithTag("MusicSource")?.GetComponent<AudioSource>();
        }
        if (explosionSource == null)
        {
            explosionSource = GameObject.FindWithTag("ExplosionSFX")?.GetComponent<AudioSource>();
        }
        if (hitBlockSource1 == null)
        {
            hitBlockSource1 = GameObject.FindWithTag("HitBlockSFX1")?.GetComponent<AudioSource>();
        }
        if (hitBlockSource2 == null)
        {
            hitBlockSource2 = GameObject.FindWithTag("HitBlockSFX2")?.GetComponent<AudioSource>();
        }

        if (musicSource == null)
        {
            Debug.LogError("MusicSource is not assigned or found in the scene.");
        }
        else
        {
            musicSource.loop = true;
        }

        if (explosionSource == null)
        {
            Debug.LogError("ExplosionSource is not assigned or found in the scene.");
        }

        if (hitBlockSource1 == null)
        {
            Debug.LogError("HitBlockSource1 is not assigned or found in the scene.");
        }

        if (hitBlockSource2 == null)
        {
            Debug.LogError("HitBlockSource2 is not assigned or found in the scene.");
        }

        Debug.Log("AudioManager initialized.");
    }
}
