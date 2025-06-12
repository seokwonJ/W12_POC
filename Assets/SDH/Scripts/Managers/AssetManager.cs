using System;
using UnityEngine;

public class AssetManager // Resources에서 받아오는 각종 오브젝트 관리
{
    public StageSO[] StageTemplates => stageTemplates;
    private StageSO[] stageTemplates; // 스테이지 구성 모음
    public GameObject[] Vehicles => vehicles;
    private GameObject[] vehicles; // 비행체 모음
    public GameObject[] Characters => characters;
    private GameObject[] characters; // 동료 모음
    public GameObject OptionTemplate => optionTemplate;
    private GameObject optionTemplate; // 설정창에 사용할 옵션 배경
    public GameObject Coin => coin;
    private GameObject coin; // 전투 중 떨어지는 골드 오브젝트

    public void Init()
    {
        stageTemplates = Resources.LoadAll<StageSO>("StageTemplates");
        Array.Sort(StageTemplates, (a, b) => a.world == b.world ? a.stage.CompareTo(b.stage) : a.world.CompareTo(b.world)); // 스테이지와 월드 순서대로 정렬

        vehicles = Resources.LoadAll<GameObject>("Vehicles");

        characters = Resources.LoadAll<GameObject>("Characters");

        optionTemplate = Resources.Load<GameObject>("Objects/OptionTemplate");

        coin = Resources.Load<GameObject>("Objects/Coin");
    }
}
