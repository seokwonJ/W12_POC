using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource sfxSource;
    public AudioSource bgmSource;

    // public SFXConfig sfxConfig; // 삭제됨: 단일 SFXConfig에서 여러 개로 변경
    public List<SFXConfig> sfxConfigs; // 추가됨: 여러 SFXConfig를 Inspector에서 등록
    private Dictionary<string, SFXData> sfxDataDict; // clipName으로 SFXData를 찾기 위한 Dictionary

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSFXDictionary(); // 여러 SFXConfig를 합쳐서 Dictionary로 등록
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 여러 SFXConfig의 sfxList를 모두 합쳐서 Dictionary로 등록
    private void InitializeSFXDictionary()
    {
        sfxDataDict = new Dictionary<string, SFXData>();
        if (sfxConfigs != null)
        {
            foreach (var config in sfxConfigs) // 추가됨
            {
                if (config == null || config.sfxList == null) continue;
                foreach (var sfx in config.sfxList)
                {
                    if (!string.IsNullOrEmpty(sfx.clipName) && sfx.clip != null)
                        sfxDataDict[sfx.clipName] = sfx;
                }
            }
        }
    }

    private void Start()
    {
        PlayBGM("Bgm1", true);
    }

    public void PlaySFX(string clipName)
    {
        if (sfxDataDict != null && sfxDataDict.TryGetValue(clipName, out var sfxData) && sfxData.clip != null)
        {
            sfxSource.PlayOneShot(sfxData.clip, sfxData.volume);
        }
        else
        {
            Debug.LogWarning($"[SoundManager] SFX not found or clip missing: {clipName}");
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

    // BGM은 기존 방식 유지 (Resources에서 로드)
    private Dictionary<string, AudioClip> bgmCache = new Dictionary<string, AudioClip>();
    private AudioClip GetBGMClip(string name)
    {
        if (bgmCache.TryGetValue(name, out var cachedClip))
        {
            return cachedClip;
        }

        AudioClip clip = Resources.Load<AudioClip>($"BGM/{name}");
        if (clip != null)
        {
            bgmCache[name] = clip;
        }
        else
        {
            Debug.LogWarning($"[SoundManager] BGM Clip not found: {name}");
        }

        return clip;
    }
}
