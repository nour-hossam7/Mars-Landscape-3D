using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip collectEnergySound;

    private void Awake()
    {
        // منع وجود أكثر من AudioManager عند الانتقال بين الـ Scenes
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (musicSource == null || gameplayMusic == null)
        {
            return;
        }

        if (musicSource.clip != gameplayMusic)
        {
            musicSource.clip = gameplayMusic;
        }

        musicSource.loop = true;

        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void PlayButtonClick()
    {
        PlaySFX(buttonClickSound);
    }

    public void PlayCollectEnergy()
    {
        PlaySFX(collectEnergySound);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null)
        {
            return;
        }

        sfxSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Clamp01(volume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = Mathf.Clamp01(volume);
        }
    }

    public void SetMusicMuted(bool isMuted)
    {
        if (musicSource != null)
        {
            musicSource.mute = isMuted;
        }
    }

    public void SetSFXMuted(bool isMuted)
    {
        if (sfxSource != null)
        {
            sfxSource.mute = isMuted;
        }
    }
}