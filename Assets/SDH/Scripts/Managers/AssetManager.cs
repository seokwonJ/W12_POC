using System;
using UnityEngine;

public class AssetManager // Resources에서 받아오는 각종 오브젝트 관리
{
    public StageSO[] StageTemplates => stageTemplates;
    private StageSO[] stageTemplates; // 스테이지 구성 모음
    public GameObject[] Vehicles => vehicles;
    private GameObject[] vehicles; // 비행체 모음
    public GameObject[] VehicleIcons => vehicleIcons;
    private GameObject[] vehicleIcons; // 비행체 아이콘 모음
    public GameObject[] Characters => characters;
    private GameObject[] characters; // 동료 모음
    public GameObject[] CharacterIcons => characterIcons;
    private GameObject[] characterIcons; // 동료 아이콘 모음
    public GameObject OptionTemplate => optionTemplate;
    private GameObject optionTemplate; // 설정창에 사용할 옵션 배경
    public GameObject Coin => coin;
    private GameObject coin; // 전투 중 떨어지는 골드 오브젝트

    public void Init()
    {
        stageTemplates = Resources.LoadAll<StageSO>("StageTemplates");
        Array.Sort(StageTemplates, (a, b) => a.world == b.world ? a.stage.CompareTo(b.stage) : a.world.CompareTo(b.world)); // 스테이지와 월드 순서대로 정렬

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

    public int FindIcon(GameObject character) // 동료 게임오브젝트를 보고 에셋매니저 내 인덱스를 찾는 함수 (인덱스가 같은 Icon을 불러오기 위해)
    {
        for(int i = 0; i < characters.Length; i++)
        {
            if (character.Equals(characters[i])) return i;
        }

        return -1;
    }
}
