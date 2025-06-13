using System.Collections.Generic;
using UnityEngine;

public enum EArtifacts // 아티팩트 Enum
{
    Health10,
    Health11,
    Health12,
    Health13,
    Health14,
    Health15,
    Length // 이건 유물이 아니라 길이용으로 추가한 값
}

public class ArtifactManager // 인게임 유물 관리
{
    public bool[] Artifacts => artifacts;
    private bool[] artifacts = new bool[(int)EArtifacts.Length];
    public bool IsFullArtifact => isFullArtifact;
    private bool isFullArtifact; // 아티팩트를 전부 채웠다면

    public void BuyArtifact(EArtifacts artifactCode)
    {
        if (artifacts[(int)artifactCode]) Debug.Log("유물 중복 구매됨");
        artifacts[(int)artifactCode] = true;

        switch (artifactCode)
        {
            case EArtifacts.Health10:
                Managers.Status.MaxHp += 10;
                break;
            case EArtifacts.Health11:
                Managers.Status.MaxHp += 11;
                break;
            case EArtifacts.Health12:
                Managers.Status.MaxHp += 12;
                break;
            case EArtifacts.Health13:
                Managers.Status.MaxHp += 13;
                break;
            case EArtifacts.Health14:
                Managers.Status.MaxHp += 14;
                break;
            case EArtifacts.Health15:
                Managers.Status.MaxHp += 15;
                break;
        }

        foreach(bool artifactCheck in artifacts)
        {
            if (!artifactCheck) return;
        }

        isFullArtifact = true; // 모든 유물을 구매했다면 isFullArtifact = true
    }
}
