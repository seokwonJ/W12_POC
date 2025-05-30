using System.Collections.Generic;
using UnityEngine;

public class ArtifactManager // 인게임 유물 관리
{
    public Dictionary<string, bool> Artifats => artifacts;
    private Dictionary<string, bool> artifacts = new(); // 유물 리스트
}
