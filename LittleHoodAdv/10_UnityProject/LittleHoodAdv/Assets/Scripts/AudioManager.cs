using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM")]
    [SerializeField] private AudioSource _bgm = null;
    [SerializeField] private AudioClip _bgmClip = null;
    [SerializeField] private AudioClip _gameOverClip = null;
    [Header("SE")]
    [SerializeField] private AudioSource _se = null;
    [SerializeField] private AudioClip _jumpClip = null;

    float lastSeTime;
    [SerializeField] float seInterval = 0.3f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // ===== BGM =====
    public void PlayBgmMain()
    {
        PlayBgm(_bgmClip);
    }

    public void PlayBgmGameOver()
    {
        PlayBgm(_gameOverClip);
    }

    void PlayBgm(AudioClip clip)
    {
        if (_bgm.clip == clip) return;

        _bgm.clip = clip;
        _bgm.Play();
    }

    public void StopBgm()
    {
        _bgm.Stop();
    }

    // ===== SE =====
    public void PlaySeClick()
    {
        //if (_se.isPlaying) return;
        if (Time.time - lastSeTime < seInterval) return;
        lastSeTime = Time.time;
        _se.PlayOneShot(_jumpClip);
    }
}
