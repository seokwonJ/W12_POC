using System;
using UnityEngine;

public class AssetManager // Resources���� �޾ƿ��� ���� ������Ʈ ����
{
    public StageSO[] StageTemplates; // �������� ���� ����
    public GameObject Coin; // ���� �� �������� ��� ������Ʈ

    public void Init()
    {
        StageTemplates = Resources.LoadAll<StageSO>("StageTemplates");
        Array.Sort(StageTemplates, (a, b) => a.world == b.world ? a.stage.CompareTo(b.stage) : a.world.CompareTo(b.world)); // ���������� ���� ������� ����
        Coin = Resources.Load<GameObject>("Objects/Coin");
    }
}
