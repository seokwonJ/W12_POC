using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Random은 UnityEngine.Random으로 자동으로 사용하게
using Random = UnityEngine.Random; // UnityEngine.Random을 사용하기 위해 명시적으로 지정

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
    private int world = 1; // 월드 번호, 1-2 일 경우 1
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
    private int stage = 1; // 스테이지 번호, 1-2 일 경우 2

    public void Init()
    {
        stageTemplates = Resources.LoadAll<StageSO>("StageTemplates");
    }

    public void StartGame() // 게임 시작. 현재는 스테이지 변수 초기화만 있으며 아무것도 안함 이거 수정하면서 위에 변수들 초기화 설정값 변경할 것
    {
        world = 1;
        stage = 1;
    }

    public StageSO GetNowStageTemplate() // 현재 스테이지 템플릿 가져오기
    {
        return Array.Find(stageTemplates, stageSO => stageSO.world == world && stageSO.stage == stage);
    }
}
