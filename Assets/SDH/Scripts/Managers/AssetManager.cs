using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AssetManager // Resources���� �޾ƿ��� ���� ������Ʈ ����
{
    public StageSO[] StageTemplates => stageTemplates;
    private StageSO[] stageTemplates; // �������� ���� ����
    public int[] StageCounts => stageCounts;
    private int[] stageCounts; // ���� �� �������� �� ����
    public GameObject[] Vehicles => vehicles;
    private GameObject[] vehicles; // ����ü ����
    public GameObject[] VehicleIcons => vehicleIcons;
    private GameObject[] vehicleIcons; // ����ü ������ ����
    public GameObject[] Characters => characters;
    private GameObject[] characters; // ���� ����
    public GameObject[] CharacterIcons => characterIcons;
    private GameObject[] characterIcons; // ���� ������ ����
    public GameObject OptionTemplate => optionTemplate;
    private GameObject optionTemplate; // ����â � ����� �ɼ� ���
    public GameObject BossIcon => bossIcon;
    private GameObject bossIcon; // ������ �����Ű�� ����� ������
    public GameObject Coin => coin;
    private GameObject coin; // ���� �� �������� ��� ������Ʈ

    public void Init()
    {
        stageTemplates = Resources.LoadAll<StageSO>("StageTemplates");
        Array.Sort(StageTemplates, (a, b) => a.world == b.world ? a.stage.CompareTo(b.stage) : a.world.CompareTo(b.world)); // ���������� ���� ������� ����
        SetStageCounts();

        vehicleIcons = Resources.LoadAll<GameObject>("VehicleIcons");
        Array.Sort(vehicleIcons, (a, b) => a.GetComponent<ObjectOrder>().Order.CompareTo(b.GetComponent<ObjectOrder>().Order));

        vehicles = new GameObject[VehicleIcons.Length];
        foreach(GameObject vehicle in Resources.LoadAll<GameObject>("Vehicles"))
        {
            for(int i = 0; i < vehicleIcons.Length; i++)
            {
                if(vehicle.name == vehicleIcons[i].name)
                {
                    vehicles[i] = vehicle;
                }
            }
        }

        characterIcons = Resources.LoadAll<GameObject>("CharacterIcons");
        Array.Sort(characterIcons, (a, b) => a.GetComponent<ObjectOrder>().Order.CompareTo(b.GetComponent<ObjectOrder>().Order));

        characters = new GameObject[characterIcons.Length];
        foreach (GameObject character in Resources.LoadAll<GameObject>("Characters"))
        {
            for (int i = 0; i < characterIcons.Length; i++)
            {
                if (character.name == characterIcons[i].name)
                {
                    characters[i] = character;
                }
            }
        }

        optionTemplate = Resources.Load<GameObject>("Objects/OptionTemplate");

        bossIcon = Resources.Load<GameObject>("Objects/BossIcon");

        coin = Resources.Load<GameObject>("Objects/Coin");
    }

    private void SetStageCounts() // stageCounts �� �����ֱ� (stageTemplates�� �޾ƿ� �� �����ؾ� ��)
    {
        stageCounts = new int[stageTemplates[^1].world + 1]; // ���Ǹ� ���� �ε��� 1���� ���� (0�� �������)
        foreach(StageSO stage in stageTemplates)
        {
            if (!stage.isBossStage) continue; // ���� ���������� �׻� ���� �������̹Ƿ� �̰����� ���帶�� �������� ���� �Ǵ�

            stageCounts[stage.world] = stage.stage;
        }
    }
}
