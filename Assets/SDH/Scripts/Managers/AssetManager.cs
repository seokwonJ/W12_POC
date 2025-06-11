using System;
using UnityEngine;

public class AssetManager // Resources���� �޾ƿ��� ���� ������Ʈ ����
{
    public StageSO[] StageTemplates => stageTemplates;
    private StageSO[] stageTemplates; // �������� ���� ����
    public GameObject[] Vehicles => vehicles;
    private GameObject[] vehicles; // ����ü ����
    public GameObject[] Characters => characters;
    private GameObject[] characters; // ���� ����
    public GameObject OptionTemplate => optionTemplate;
    private GameObject optionTemplate; // ����â�� ����� �ɼ� ���
    public GameObject Coin => coin;
    private GameObject coin; // ���� �� �������� ��� ������Ʈ

    public void Init()
    {
        stageTemplates = Resources.LoadAll<StageSO>("StageTemplates");
        Array.Sort(StageTemplates, (a, b) => a.world == b.world ? a.stage.CompareTo(b.stage) : a.world.CompareTo(b.world)); // ���������� ���� ������� ����

        vehicles = Resources.LoadAll<GameObject>("Vehicles");

        characters = Resources.LoadAll<GameObject>("Characters");

        optionTemplate = Resources.Load<GameObject>("Objects/OptionTemplate");

        coin = Resources.Load<GameObject>("Objects/Coin");
    }
}
