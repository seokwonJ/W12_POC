using System.Collections.Generic;
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
    private bool[] artifacts = new bool[(int)EArtifacts.Length];
    public bool IsFullArtifact => isFullArtifact;
    private bool isFullArtifact; // ��Ƽ��Ʈ�� ���� ä���ٸ�

    public void BuyArtifact(EArtifacts artifactCode)
    {
        if (artifacts[(int)artifactCode]) Debug.Log("���� �ߺ� ���ŵ�");
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

        isFullArtifact = true; // ��� ������ �����ߴٸ� isFullArtifact = true
    }
}
