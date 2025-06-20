using System;
using UnityEngine;

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
    private ArtifactsList artifactsList = new();
    public Action<PlayerMove> playerAction; // ����ü�� ���õ� �׼�

    public void StartGame() // ���� ����. ��Ʈ �Լ��� Stage��
    {
        artifacts = new bool[(int)EArtifacts.Length];

        //playerAction += artifactsList.OnAbility;
    }
}

public class ArtifactsList // ��Ƽ��Ʈ�� ȿ����
{
    public void OnAbility(PlayerMove playerMove) // �ƹ��� �� Ÿ�� ���� �� ����ü �ӷ� 2��
    {
        if (Managers.Status.RiderCount == 0) playerMove.moveSpeed *= 2f;
    }
}