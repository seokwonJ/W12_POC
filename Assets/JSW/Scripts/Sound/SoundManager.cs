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

    public void PlaySFX(string clipName)
    {
        AudioClip clip = GetClip(clipName);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    private AudioClip GetClip(string name)
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
}
