using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class AssetManager // Resources에서 받아오는 각종 오브젝트 관리
{
    public StageSO[] StageTemplates => stageTemplates;
    private StageSO[] stageTemplates; // 스테이지 구성 모음
    public int[] StageCounts => stageCounts;
    private int[] stageCounts; // 월드 별 스테이지 수 모음
    public GameObject[] Vehicles => vehicles;
    private GameObject[] vehicles; // 비행체 모음
    public GameObject[] VehicleIcons => vehicleIcons;
    private GameObject[] vehicleIcons; // 비행체 아이콘 모음
    public GameObject[] Characters => characters;
    private GameObject[] characters; // 동료 모음
    public GameObject[] CharacterIcons => characterIcons;
    private GameObject[] characterIcons; // 동료 아이콘 모음
    public GameObject OptionTemplate => optionTemplate;
    private GameObject optionTemplate; // 설정창 등에 사용할 옵션 배경
    public GameObject BossIcon => bossIcon;
    private GameObject bossIcon; // 보스를 연상시키는 모양의 아이콘
    public GameObject Coin => coin;
    private GameObject coin; // 전투 중 떨어지는 골드 오브젝트

    public void Init()
    {
        stageTemplates = Resources.LoadAll<StageSO>("StageTemplates");
        Array.Sort(StageTemplates, (a, b) => a.world == b.world ? a.stage.CompareTo(b.stage) : a.world.CompareTo(b.world)); // 스테이지와 월드 순서대로 정렬
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

    private void SetStageCounts() // stageCounts 값 정해주기 (stageTemplates를 받아온 뒤 실행해야 함)
    {
        stageCounts = new int[stageTemplates[^1].world + 1]; // 편의를 위해 인덱스 1부터 시작 (0은 비어있음)
        foreach(StageSO stage in stageTemplates)
        {
            if (!stage.isBossStage) continue; // 보스 스테이지는 항상 월드 마지막이므로 이것으로 월드마다 스테이지 수를 판단

            stageCounts[stage.world] = stage.stage;
        }
    }
}
