using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Tooltip("�÷��̾� ����ҽ�")]
    [SerializeField] private AudioSource playerAudioSource; // �÷��̾� ���� �ҽ� (�뽬, �ȱ� ��)
    [Tooltip("���� ���� �ҽ�")]
    [SerializeField] private AudioSource bossAudioSource; // ���� ���� �ҽ� (���� ���� ��)

    [SerializeField] private AudioSource bgmAudioSource;

    [Tooltip("BGM")]
    [SerializeField] private AudioClip bgm;
    [Tooltip("BGM ����")]
    [SerializeField] private float bgmVolume;

    [Header("�÷��̾� ����")]
    [Tooltip("�뽬 ����")]
    [SerializeField] private AudioClip dashSound;
    [Tooltip("�뽬 ���� ����")]
    [SerializeField] private float dashSoundVolume;
    [Tooltip("�� �ֵθ��� ����")]
    [SerializeField] private AudioClip[] swordSound;
    [Tooltip("�� �ֵθ��� ���� ����")]
    [SerializeField] private float swordSoundVolume;
    [Tooltip("�ǰ� ����")]
    [SerializeField] private AudioClip playerHitSound;
    [Tooltip("�ǰ� ���� ����")]
    [SerializeField] private float playerHitSoundVolume;
    [Tooltip("���� �ǰ� ����")]
    [SerializeField] private AudioClip powerHitSound;
    [Tooltip("���� �ǰ� ���� ����")]
    [SerializeField] private float powerHitSoundVolume;
    [Tooltip("�ȱ� ����")]
    [SerializeField] private AudioClip walkSound;
    [Tooltip("�ȱ� ���� ����")]
    [SerializeField] private float walkSoundVolume;
    [Tooltip("���� ����")]
    [SerializeField] private AudioClip deathBellSound;
    [Tooltip("���� ���� ����")]
    [SerializeField] private float deathBellSoundVolume;

    [Header("���� ����")]
    [Tooltip("���� �ǰ� ����")]
    [SerializeField] private AudioClip bossHitSound;
    [Tooltip("���� �ǰ� ���� ����")]
    [SerializeField] private float bossHitSoundVolume;
    [Tooltip("���� Į�� ����")]
    [SerializeField] private AudioClip bossSwordSound;
    [Tooltip("���� Į�� ���� ����")]
    [SerializeField] private float bossSwordSoundVolume;
    [Tooltip("ǥâ ����")]
    [SerializeField] private AudioClip kunaiSound;
    [Tooltip("ǥâ ���� ����")]
    [SerializeField] private float kunaiSoundVolume;
    [Tooltip("����ȯ ����")]
    [SerializeField] private AudioClip rasenganSound;
    [Tooltip("����ȯ ���� ����")]
    [SerializeField] private float rasenganSoundVolume;
    [Tooltip("���� ����")]
    [SerializeField] private AudioClip shoutSound;
    [Tooltip("���� ���� ����")]
    [SerializeField] private float shoutSoundVolume;
    [Tooltip("����ź ����")]
    [SerializeField] private AudioClip smokeSound;
    [Tooltip("����ź ���� ����")]
    [SerializeField] private float smokeSoundVolume;
    [Tooltip("���� ����")]
    [SerializeField] private AudioClip koongSound;
    [Tooltip("���� ���� ����")]
    [SerializeField] private float koongSoundVolume;
    [Tooltip("�˱� ����")]
    [SerializeField] private AudioClip bossSwordForceSound;
    [Tooltip("�˱� ���� ����")]
    [SerializeField] private float bossSwordForceSoundVolume;
    [Tooltip("�ߵ� �غ� ����")]
    [SerializeField] private AudioClip dashAttackReadySound;
    [Tooltip("�ߵ� �غ� ���� ����")]
    [SerializeField] private float dashAttackReadySoundVolume;
    [Tooltip("�ߵ� ����")]
    [SerializeField] private AudioClip dashAttackSound;
    [Tooltip("�ߵ� ���� ����")]
    [SerializeField] private float dashAttackSoundVolume;
    [Tooltip("��� ����")]
    [SerializeField] private AudioClip crowSound;
    [Tooltip("��� ���� ����")]
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
        // TODO : stopsound �����Ŵ� �׼� �����ϴ°� �����ϴ�.
        // �׼� += StopSound();
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
    /// �÷��̾� ���� ���
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
    /// ���� ���� ���
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
    //    Debug.Log("�÷��̾� ���� ����");
    //    playerAudioSource.Stop();
    //}

    //private void StopBossSound()
    //{
    //    Debug.Log("���� ���� ����");
    //    bossAudioSource.Stop();
    //}
}
