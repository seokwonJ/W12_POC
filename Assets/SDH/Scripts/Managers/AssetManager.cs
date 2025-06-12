using System;
using UnityEngine;

public class AssetManager // Resources에서 받아오는 각종 오브젝트 관리
{
    public StageSO[] StageTemplates; // 스테이지 구성 모음
    public GameObject Coin; // 전투 중 떨어지는 골드 오브젝트

    public void Init()
    {
        StageTemplates = Resources.LoadAll<StageSO>("StageTemplates");
        Array.Sort(StageTemplates, (a, b) => a.world == b.world ? a.stage.CompareTo(b.stage) : a.world.CompareTo(b.world)); // 스테이지와 월드 순서대로 정렬
        Coin = Resources.Load<GameObject>("Objects/Coin");
    }
}
