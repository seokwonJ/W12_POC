using System;
using UnityEngine;
using static UnityEngine.InputManagerEntry;
// Random은 UnityEngine.Random으로 자동으로 사용하게

public class StageManager // 씬 전환 관리 (전투-상점 등)
{
    public StageSO[] StageTemplates => stageTemplates;
    private StageSO[] stageTemplates; // 스테이지 구성 모음
    public int World
    {
        get
        {
            return world;
        }
        set
        {
            world = value;
        }
    }
    private int world; // 월드 번호, 1-2 일 경우 1
    public int Stage
    {
        get
        {
            return stage;
        }
        set
        {
            stage = value;
        }
    }
    private int stage; // 스테이지 번호, 1-2 일 경우 2
    public bool OnField
    {
        get
        {
            return onField;
        }
        set
        {
            onField = value;
        }
    }
    private bool onField = true; //true면 필드, false면 상점 -> 이후 수정 필요
    public int EnemyKill
    {
        get
        {
            return enemyKill;
        }
        set
        {
            enemyKill = value;
        }
    }
    private int enemyKill; // 잡은 적 수
    public void Init()
    {
        stageTemplates = Resources.LoadAll<StageSO>("StageTemplates");
        Array.Sort(stageTemplates, (a, b) => a.world == b.world ? a.stage.CompareTo(b.stage) : a.world.CompareTo(b.world)); // stageSO를 순서대로 정렬
    }

    public void StartGame() // 게임 시작. 현재는 스테이지 변수 초기화만 있으며 아무것도 안함 이거 수정하면서 위에 변수들 초기화 설정값 변경할 것 (필드를 시작할 때 값을 수정하므로 유의)
    {
        world = 1;
        stage = 0;
    }

    public void StartStage() // 현재 스테이지 시작
    {

    }

    public StageSO GetNowStageTemplate() // 현재 스테이지 템플릿 가져오기
    {
        return Array.Find(stageTemplates, stageSO => stageSO.world == world && stageSO.stage == stage); // stageTemplates를 정렬해뒀기 때문에 나중에 이부분 수정 가능 @@@@@@@@@@@@@@@@@@@@@@@@@@
    }
}
