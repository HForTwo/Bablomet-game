using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Аудио источники")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Фоновая музыка")]
    [SerializeField] private AudioClip _backgroundMusic;

    [Header("Звуковые эффекты (SFX)")]
    [SerializeField] private AudioClip _moneyDrawSound;
    [SerializeField] private AudioClip _goldMoneyDrawSound;
    [SerializeField] private AudioClip _buyItemSound;

    public void PlayMoneyDraw() => PlaySFX(_moneyDrawSound);
    public void PlayGoldMoneyDraw() => PlaySFX(_goldMoneyDrawSound);
    public void PlayBuyItem() => PlaySFX(_buyItemSound);

    public void PlayBackgroundMusic()
    {
        if (_backgroundMusic != null && _musicSource != null)
        {
            _musicSource.clip = _backgroundMusic;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (_musicSource != null)
            _musicSource.Stop();
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip != null && _sfxSource != null)
            _sfxSource.PlayOneShot(clip);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }
}