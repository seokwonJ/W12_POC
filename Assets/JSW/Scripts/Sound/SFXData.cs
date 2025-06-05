using UnityEngine;

[System.Serializable]
public class SFXData
{
    public string clipName;      // 효과음 이름
    public AudioClip clip;       // 오디오 파일
    [Range(0f, 1f)]
    public float volume = 1f;    // 볼륨(0~1)
}
