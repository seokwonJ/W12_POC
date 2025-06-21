using System;
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
    private bool[] artifacts; // 아티팩트 중복 체크
    public bool IsFullArtifact => isFullArtifact;
    private bool isFullArtifact; // 아티팩트를 전부 채웠다면
    private ArtifactsList artifactsList = new();
    public Action<PlayerMove> playerAction; // 비행체와 관련된 액션

    public void StartGame() // 게임 시작. 루트 함수는 Stage임
    {
        artifacts = new bool[(int)EArtifacts.Length];

        //playerAction += artifactsList.OnAbility;
    }
}

public class ArtifactsList // 아티팩트의 효과들
{
    public void OnAbility(PlayerMove playerMove) // 아무도 안 타고 있을 시 비행체 속력 2배
    {
        if (Managers.Status.RiderCount == 0) playerMove.moveSpeed *= 2f;
    }
}