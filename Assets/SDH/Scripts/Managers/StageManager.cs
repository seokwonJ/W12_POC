using System;
using System.Collections;
using UnityEngine;

public class StageManager // 씬 전환 관리 (전투-상점 등)
{
    public EnemySpawner EnemySpawner;

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
    public StageSO NowStage => nowStage;
    private StageSO nowStage; // 현재 스테이지 정보
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
            if (onField) // 상점 끝나고 스테이지로
            {
                Managers.Record.ClearStageDamageRecord(); // 스테이지 끝나고 기록 초기화
            }
            else // 스테이지 끝나고 상점으로
            {
                Debug.Log("현재 스테이지 끝");
                Managers.PlayerControl.NowPlayer.GetComponentInChildren<PlayerHP>().isEndFieldNoDamage = true;
                Managers.Record.AddTotalDamageRecord(); // 스테이지 끝나고 총 데미지 레코드에 기록을 더하기
                Managers.Record.PrintAllDamageRecord(isStage:true); // 스테이지에서 가한 피해량 출력
                Managers.Record.PrintAllDamageRecord(isStage:false); // 총 가한 피해량 출력
            }
        }
    }
    private bool onField; // true면 필드, false면 상점 이 변수가 호출되었다는 것은 스테이지나 상점이 끝났다는 의미
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
        }
    }
    private int curEnemyCount; // 현재 스테이지에서 남아있는 적 수

    public void StartGame() // 게임 시작. 다른 매니저의 게임 시작도 이곳에서
    {
        nowStage = null;
        world = 1;
        stage = 1; // 상점에서 값을 1씩 추가하므로 1부터 시작
        enemyTotalKill = 0;
        onField = true;

        Managers.Status.StartGame();
        Managers.Artifact.StartGame();
    }

    public void SetStage() // 현재 스테이지 시작하며 기본 설정
    {
        Managers.Status.Hp = Managers.Status.MaxHp;
        enemyKill = 0;
        curEnemyCount = 0;
        nowStage = Array.Find(Managers.Asset.StageTemplates, stageSO => stageSO.world == world && stageSO.stage == stage);
    }

    public void StartStage() // 현재 스테이지 시작. 위쪽 SetStage 다음에 실행되어야 함
    {
        EnemySpawner.StartSpawnEnemy();
    }

    public void GoNextStage()
    {
        if (Managers.Stage.NowStage == null || !Managers.Stage.NowStage.isBossStage) // 이 코드는 상점이 끝날 때 실행되니 1-1전투 1-1상점 1-2전투... 식으로 진행됨
        {
            Managers.Stage.Stage++;
        }
        else
        {
            Managers.Stage.World++;
            Managers.Stage.Stage = 1;
        }
    }

    public void PlusEnemyKill(Vector3 position) // 적 처치 수 증가
    {
        enemyKill++;
        enemyTotalKill++;
        if (!nowStage.isBossStage && enemyKill % 10 == 0) // 10마리마다 코인 생성
        {
            SpawnCoin(position);
        }
    }

    public void SpawnCoin(Vector3 position, int coinCount = 1) // 코인 생성
    {
        for (int i = 0; i < coinCount; i++)
        {
            GameObject coinObj = UnityEngine.Object.Instantiate(Managers.Asset.Coin, position, Quaternion.identity);
            coinObj.GetComponent<Coin>().SetCoinValue(UnityEngine.Random.Range(8, 13)); // 코인 값은 8~12 사이의 랜덤값
        }
    }
}
