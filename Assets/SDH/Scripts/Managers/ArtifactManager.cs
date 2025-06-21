using System;
using System.Collections.Generic;

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
    public int ArtifactCounts
    {
        get
        {
            return artifactCounts;
        }
        set
        {
            artifactCounts = value;
        }
    }
    private int artifactCounts; // 구매한 아티팩트 수
    public List<ArtifactBaseTemplate> ArtifactLists => artifactLists;
    private List<ArtifactBaseTemplate> artifactLists; // 아티팩트 정보와 효과 리스트, 사용할 때마다 형변환 필요... 이게 내 최선임

    public void StartGame() // 게임 시작. 루트 함수는 Stage임
    {
        artifactLists = new();

        foreach (Type artifact in typeof(ArtifactsList).GetNestedTypes(System.Reflection.BindingFlags.Public)) // ArtifactsList에는 Public밖에 없으니 NonPublic는 볼 필요 X
        {
            artifactLists.Add((ArtifactBaseTemplate)Activator.CreateInstance(artifact));
        }
    }

    public void BuyArtifact(int artifactIdx) // 아티팩트마다 형변환하고 구독 필요
    {
        var t = typeof(ArtifactTemplate);

        switch (artifactIdx)
        {
            case 0:
                ((ArtifactTemplate)Managers.Artifact.ArtifactLists[0]).Subscribe();
                break;
            case 1:
                ((ArtifactTemplate<PlayerMove>)Managers.Artifact.ArtifactLists[1]).Subscribe();
                break;
            case 2:
                ((ArtifactTemplate<PlayerMove>)Managers.Artifact.ArtifactLists[2]).Subscribe();
                break;
            case 3:
                ((ArtifactTemplate<Character>)Managers.Artifact.ArtifactLists[3]).Subscribe();
                break;
        }
    }

    public bool IsArtifactMax() // 아티팩트풀에 아티팩트가 더 이상 없는지 체크
    {
        return Managers.Artifact.ArtifactCounts == Managers.Artifact.ArtifactLists.Count;
    }
}