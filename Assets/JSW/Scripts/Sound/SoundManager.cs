using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Tooltip("플레이어 사운드소스")]
    [SerializeField] private AudioSource playerAudioSource; // 플레이어 사운드 소스 (대쉬, 걷기 등)
    [Tooltip("보스 사운드 소스")]
    [SerializeField] private AudioSource bossAudioSource; // 보스 사운드 소스 (보스 공격 등)

    [SerializeField] private AudioSource bgmAudioSource;

    [Tooltip("BGM")]
    [SerializeField] private AudioClip bgm;
    [Tooltip("BGM 볼륨")]
    [SerializeField] private float bgmVolume;

    [Header("플레이어 사운드")]
    [Tooltip("대쉬 사운드")]
    [SerializeField] private AudioClip dashSound;
    [Tooltip("대쉬 사운드 볼륨")]
    [SerializeField] private float dashSoundVolume;
    [Tooltip("검 휘두르는 사운드")]
    [SerializeField] private AudioClip[] swordSound;
    [Tooltip("검 휘두르는 사운드 볼륨")]
    [SerializeField] private float swordSoundVolume;
    [Tooltip("피격 사운드")]
    [SerializeField] private AudioClip playerHitSound;
    [Tooltip("피격 사운드 볼륨")]
    [SerializeField] private float playerHitSoundVolume;
    [Tooltip("강한 피격 사운드")]
    [SerializeField] private AudioClip powerHitSound;
    [Tooltip("강한 피격 사운드 볼륨")]
    [SerializeField] private float powerHitSoundVolume;
    [Tooltip("걷기 사운드")]
    [SerializeField] private AudioClip walkSound;
    [Tooltip("걷기 사운드 볼륨")]
    [SerializeField] private float walkSoundVolume;
    [Tooltip("죽음 사운드")]
    [SerializeField] private AudioClip deathBellSound;
    [Tooltip("죽음 사운드 볼륨")]
    [SerializeField] private float deathBellSoundVolume;

    [Header("보스 사운드")]
    [Tooltip("보스 피격 사운드")]
    [SerializeField] private AudioClip bossHitSound;
    [Tooltip("보스 피격 사운드 볼륨")]
    [SerializeField] private float bossHitSoundVolume;
    [Tooltip("보스 칼질 사운드")]
    [SerializeField] private AudioClip bossSwordSound;
    [Tooltip("보스 칼질 사운드 볼륨")]
    [SerializeField] private float bossSwordSoundVolume;
    [Tooltip("표창 사운드")]
    [SerializeField] private AudioClip kunaiSound;
    [Tooltip("표창 사운드 볼륨")]
    [SerializeField] private float kunaiSoundVolume;
    [Tooltip("나선환 사운드")]
    [SerializeField] private AudioClip rasenganSound;
    [Tooltip("나선환 사운드 볼륨")]
    [SerializeField] private float rasenganSoundVolume;
    [Tooltip("기합 사운드")]
    [SerializeField] private AudioClip shoutSound;
    [Tooltip("기합 사운드 볼륨")]
    [SerializeField] private float shoutSoundVolume;
    [Tooltip("연막탄 사운드")]
    [SerializeField] private AudioClip smokeSound;
    [Tooltip("연막탄 사운드 볼륨")]
    [SerializeField] private float smokeSoundVolume;
    [Tooltip("착지 사운드")]
    [SerializeField] private AudioClip koongSound;
    [Tooltip("착지 사운드 볼륨")]
    [SerializeField] private float koongSoundVolume;
    [Tooltip("검기 사운드")]
    [SerializeField] private AudioClip bossSwordForceSound;
    [Tooltip("검기 사운드 볼륨")]
    [SerializeField] private float bossSwordForceSoundVolume;
    [Tooltip("발도 준비 사운드")]
    [SerializeField] private AudioClip dashAttackReadySound;
    [Tooltip("발도 준비 사운드 볼륨")]
    [SerializeField] private float dashAttackReadySoundVolume;
    [Tooltip("발도 사운드")]
    [SerializeField] private AudioClip dashAttackSound;
    [Tooltip("발도 사운드 볼륨")]
    [SerializeField] private float dashAttackSoundVolume;
    [Tooltip("까마귀 사운드")]
    [SerializeField] private AudioClip crowSound;
    [Tooltip("까마귀 사운드 볼륨")]
    [SerializeField] private float crowSoundVolume;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // TODO : stopsound 같은거는 액션 구독하는게 좋습니다.
        // 액션 += StopSound();
        PlayBGMSound(bgm, bgmVolume);
    }

    public void PlayBGMSound(AudioClip clip, float volume = 1f)
    {
        if (clip != null && bgmAudioSource != null)
        {
            bgmAudioSource.clip = clip;
            bgmAudioSource.volume = volume;
            bgmAudioSource.Play();
        }
    }

    /// <summary>
    /// 플레이어 사운드 재생
    /// </summary>

    public void PlayDashSound()
    {
        PlayPlayerSound(dashSound, dashSoundVolume);
    }
    public void PlaySwordSound(int num)
    {
        PlayPlayerSound(swordSound[num], swordSoundVolume);
    }

    public void PlayPlayerHitSound()
    {
        PlayPlayerSound(playerHitSound, playerHitSoundVolume);
    }

    public void PlayPowerHitSound()
    {
        PlayPlayerSound(powerHitSound, powerHitSoundVolume);
    }

    public void PlayWalkSound()
    {
        PlayPlayerSound(walkSound, walkSoundVolume);
    }

    public void PlayDeathBellSound()
    {
        PlayPlayerSound(deathBellSound, deathBellSoundVolume);
    }


    /// <summary>
    /// 보스 사운드 재생
    /// </summary>
    public void PlayBossHitSound()
    {
        PlayBossSound(bossHitSound, bossHitSoundVolume);
    }
    public void PlaySmokeSound()
    {
        PlayBossSound(smokeSound, smokeSoundVolume);
    }

    public void PlayBossSwordSound()
    {
        PlayBossSound(bossSwordSound, bossSwordSoundVolume);
    }

    public void PlayKoongSound()
    {
        PlayBossSound(koongSound, koongSoundVolume);
    }

    public void PlayKunaiSound()
    {
        PlayBossSound(kunaiSound, kunaiSoundVolume);
    }

    public void PlayRasenganSound()
    {
        PlayBossSound(rasenganSound, rasenganSoundVolume);
    }

    public void PlayShoutSound()
    {
        PlayBossSound(shoutSound, shoutSoundVolume);
    }
    public void PlayBossSwordForceSound()
    {
        PlayBossSound(bossSwordForceSound, bossSwordForceSoundVolume);
    }
    public void PlayDashAttackReadySound()
    {
        PlayBossSound(dashAttackReadySound, dashAttackReadySoundVolume);
    }
    public void PlayDashAttackSound()
    {
        PlayBossSound(dashAttackSound, dashAttackSoundVolume);
    }

    public void PlayCrowSound()
    {
        PlayBossSound(crowSound, crowSoundVolume);
    }


    private void PlayPlayerSound(AudioClip clip, float volume)
    {
        if (clip != null && playerAudioSource != null)
        {
            playerAudioSource.PlayOneShot(clip, volume);
        }
    }
    private void PlayBossSound(AudioClip clip, float volume)
    {
        if (clip != null && bossAudioSource != null)
        {
            bossAudioSource.PlayOneShot(clip, volume);
        }
    }

    //private void StopPlayerSound()
    //{
    //    Debug.Log("플레이어 사운드 정지");
    //    playerAudioSource.Stop();
    //}

    //private void StopBossSound()
    //{
    //    Debug.Log("보스 사운드 정지");
    //    bossAudioSource.Stop();
    //}
}
