using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioClip bgmMenuClip;
    [SerializeField] private AudioClip bgmGameplayClip;
    [SerializeField] private AudioClip sfxTapClip;
    [SerializeField] private AudioClip sfxComboClip;
    [SerializeField] private AudioClip sfxLevelCompleteClip;
    [SerializeField] private AudioClip sfxGameOverClip;

    private bool isMusicEnabled = true;
    private bool isSoundEnabled = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadAudioSettings();
    }

    private void LoadAudioSettings()
    {
        isMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        bgmSource.mute = !isMusicEnabled;
        sfxSource.mute = !isSoundEnabled;
    }

    public void PlayMenuMusic()
    {
        if (!isMusicEnabled) return;
        bgmSource.clip = bgmMenuClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlayGameplayMusic()
    {
        if (!isMusicEnabled) return;
        bgmSource.clip = bgmGameplayClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlayTapSound()
    {
        if (!isSoundEnabled) return;
        sfxSource.PlayOneShot(sfxTapClip);
    }

    public void PlayComboSound()
    {
        if (!isSoundEnabled) return;
        sfxSource.PlayOneShot(sfxComboClip);
    }

    public void PlayLevelCompleteSound()
    {
        if (!isSoundEnabled) return;
        sfxSource.PlayOneShot(sfxLevelCompleteClip);
    }

    public void PlayGameOverSound()
    {
        if (!isSoundEnabled) return;
        sfxSource.PlayOneShot(sfxGameOverClip);
    }

    public void ToggleMusic()
    {
        isMusicEnabled = !isMusicEnabled;
        bgmSource.mute = !isMusicEnabled;
        PlayerPrefs.SetInt("MusicEnabled", isMusicEnabled ? 1 : 0);
    }

    public void ToggleSound()
    {
        isSoundEnabled = !isSoundEnabled;
        sfxSource.mute = !isSoundEnabled;
        PlayerPrefs.SetInt("SoundEnabled", isSoundEnabled ? 1 : 0);
    }

    public bool IsMusicEnabled() => isMusicEnabled;
    public bool IsSoundEnabled() => isSoundEnabled;
}
