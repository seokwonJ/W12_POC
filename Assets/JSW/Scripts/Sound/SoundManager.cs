using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource sfxSource;
    public AudioSource bgmSource;

    private Dictionary<string, AudioClip> cache = new Dictionary<string, AudioClip>();

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
        }
    }

    private void Start()
    {
        PlayBGM("Bgm1", true);
    }

    public void PlaySFX(string clipName)
    {
        AudioClip clip = GetSFXClip(clipName);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayBGM(string clipName, bool loop = true)
    {
        AudioClip clip = GetBGMClip(clipName);
        if (clip != null)
        {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        }
    }

    private AudioClip GetSFXClip(string name)
    {
        if (cache.TryGetValue(name, out var cachedClip))
        {
            return cachedClip;
        }

        AudioClip clip = Resources.Load<AudioClip>($"SFX/{name}");
        if (clip != null)
        {
            cache[name] = clip;
        }
        else
        {
            Debug.LogWarning($"[SoundManager] Clip not found: {name}");
        }

        return clip;
    }

    private AudioClip GetBGMClip(string name)
    {
        if (cache.TryGetValue(name, out var cachedClip))
        {
            return cachedClip;
        }

        AudioClip clip = Resources.Load<AudioClip>($"BGM/{name}");
        if (clip != null)
        {
            cache[name] = clip;
        }
        else
        {
            Debug.LogWarning($"[SoundManager] Clip not found: {name}");
        }

        return clip;
    }
}
