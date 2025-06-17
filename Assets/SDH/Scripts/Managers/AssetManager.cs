using System;
using UnityEngine;

public class AssetManager // Resources���� �޾ƿ��� ���� ������Ʈ ����
{
    public StageSO[] StageTemplates => stageTemplates;
    private StageSO[] stageTemplates; // �������� ���� ����
    public GameObject[] Vehicles => vehicles;
    private GameObject[] vehicles; // ����ü ����
    public GameObject[] VehicleIcons => vehicleIcons;
    private GameObject[] vehicleIcons; // ����ü ������ ����
    public GameObject[] Characters => characters;
    private GameObject[] characters; // ���� ����
    public GameObject[] CharacterIcons => characterIcons;
    private GameObject[] characterIcons; // ���� ������ ����
    public GameObject OptionTemplate => optionTemplate;
    private GameObject optionTemplate; // ����â�� ����� �ɼ� ���
    public GameObject Coin => coin;
    private GameObject coin; // ���� �� �������� ��� ������Ʈ

    public void Init()
    {
        stageTemplates = Resources.LoadAll<StageSO>("StageTemplates");
        Array.Sort(StageTemplates, (a, b) => a.world == b.world ? a.stage.CompareTo(b.stage) : a.world.CompareTo(b.world)); // ���������� ���� ������� ����

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

        coin = Resources.Load<GameObject>("Objects/Coin");
    }

    public int FindIcon(GameObject character) // ���� ���ӿ�����Ʈ�� ���� ���¸Ŵ��� �� �ε����� ã�� �Լ� (�ε����� ���� Icon�� �ҷ����� ����)
    {
        for(int i = 0; i < characters.Length; i++)
        {
            if (character.Equals(characters[i])) return i;
        }

        return -1;
    }
}
