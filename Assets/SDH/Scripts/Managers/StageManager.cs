using System;
using System.Collections;
using UnityEngine;
// Random은 UnityEngine.Random으로 자동으로 사용하게

public class StageManager // 씬 전환 관리 (전투-상점 등)
{
    private StageSO[] stageTemplates; // 스테이지 구성 모음
    public GameObject Coin => coin;
    private GameObject coin; // 전투 도중 떨어지는 골드 오브젝트
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
            Managers.PlayerControl.NowPlayer.GetComponent<TmpPlayerControl>().StageEnd();
        }
    }
    private bool onField = true; // true면 필드, false면 상점 이 변수가 호출되었다는 것은 스테이지나 상점이 끝났다는 의미
    public int EnemyTotalKill
    {
        get
        {
            return enemyTotalKill;
        }
        set
        {
            enemyTotalKill = value;
        }
    }
    private int enemyTotalKill; // 이번 게임에서 잡은 적 수
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
    private int enemyKill; // 이번 스테이지에서 잡은 적 수

    public int CurEnemyCount
    {
        get
        {
            return curEnemyCount;
        }
        set
        {
            curEnemyCount = value;
            Debug.Log($"현재 스테이지 남아있는 적 수: {curEnemyCount}");
        }
    }
    private int curEnemyCount; // 현재 스테이지에서 남아있는 적 수

    public void Init()
    {
        stageTemplates = Resources.LoadAll<StageSO>("StageTemplates");
        Array.Sort(stageTemplates, (a, b) => a.world == b.world ? a.stage.CompareTo(b.stage) : a.world.CompareTo(b.world)); // stageSO를 순서대로 정렬
        coin = Resources.Load<GameObject>("Objects/Coin");
    }

    public void StartGame() // 게임 시작. 현재는 스테이지 변수 초기화만 있으며 아무것도 안함 이거 수정하면서 위에 변수들 초기화 설정값 변경할 것 (필드를 시작할 때 값을 수정하므로 유의)
    {
        world = 1;
        stage = 0;
        enemyTotalKill = 0;
        onField = true;
    }

    public StageSO StartStage() // 현재 스테이지 시작하며 변수를 초기화하고 EnemySpawner에 현재 스테이지 정보 전달
    {
        Managers.Status.Hp = Managers.Status.MaxHp;
        enemyKill = 0;
        curEnemyCount = 0;
        return Array.Find(stageTemplates, stageSO => stageSO.world == world && stageSO.stage == stage);
    }

    public IEnumerator StageTimer(float stageTime)
    {
        while (stageTime >= 0)
        {
            stageTime -= Time.deltaTime;
            yield return null;
        }
    }

    public StageSO GetNowStageTemplate() // 현재 스테이지 템플릿 가져오기
    {
        return Array.Find(stageTemplates, stageSO => stageSO.world == world && stageSO.stage == stage); // stageTemplates를 정렬해뒀기 때문에 나중에 이부분 수정 가능 @@@@@@@@@@@@@@@@@@@@@@@@@@
    }

    public void PlusEnemyKill(Vector3 position) // 적 처치 수 증가
    {
        EnemyKill++;
        EnemyTotalKill++;
        if (EnemyKill % 10 == 0) // 10마리마다 코인 생성
        {
            SpawnCoin(position);
        }
    }

    public void SpawnCoin(Vector3 position, int coinCount = 1) // 코인 생성
    {
        for (int i = 0; i < coinCount; i++)
        {
            GameObject coinObj = GameObject.Instantiate(coin, position, Quaternion.identity);
            coinObj.GetComponent<Coin>().SetCoinValue(UnityEngine.Random.Range(8, 13)); // 코인 값은 8~12 사이의 랜덤값
        }
    }
}
