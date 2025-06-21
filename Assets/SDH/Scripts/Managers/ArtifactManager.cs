using System;
using System.Collections.Generic;

public enum EArtifacts // ��Ƽ��Ʈ Enum
{
    Health10,
    Health11,
    Health12,
    Health13,
    Health14,
    Health15,
    Length // �̰� ������ �ƴ϶� ���̿����� �߰��� ��
}

public class ArtifactManager // �ΰ��� ���� ����
{
    public bool[] Artifacts => artifacts;
    private bool[] artifacts; // ��Ƽ��Ʈ �ߺ� üũ
    public bool IsFullArtifact => isFullArtifact;
    private bool isFullArtifact; // ��Ƽ��Ʈ�� ���� ä���ٸ�
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
    private int artifactCounts; // ������ ��Ƽ��Ʈ ��
    public List<ArtifactBaseTemplate> ArtifactLists => artifactLists;
    private List<ArtifactBaseTemplate> artifactLists; // ��Ƽ��Ʈ ������ ȿ�� ����Ʈ, ����� ������ ����ȯ �ʿ�... �̰� �� �ּ���

    public void StartGame() // ���� ����. ��Ʈ �Լ��� Stage��
    {
        artifactLists = new();

        foreach (Type artifact in typeof(ArtifactsList).GetNestedTypes(System.Reflection.BindingFlags.Public)) // ArtifactsList���� Public�ۿ� ������ NonPublic�� �� �ʿ� X
        {
            artifactLists.Add((ArtifactBaseTemplate)Activator.CreateInstance(artifact));
        }
    }

    public void BuyArtifact(int artifactIdx) // ��Ƽ��Ʈ���� ����ȯ�ϰ� ���� �ʿ�
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

    public bool IsArtifactMax() // ��Ƽ��ƮǮ�� ��Ƽ��Ʈ�� �� �̻� ������ üũ
    {
        return Managers.Artifact.ArtifactCounts == Managers.Artifact.ArtifactLists.Count;
    }
}