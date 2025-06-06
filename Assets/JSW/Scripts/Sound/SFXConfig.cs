using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXConfig", menuName = "Audio/SFXConfig")]
public class SFXConfig : ScriptableObject
{
    public List<SFXData> sfxList; // 효과음 목록
}