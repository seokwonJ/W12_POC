using System;
using System.Collections;
using UnityEngine;

public class StageManager // 씬 전환 관리 (전투-상점 등)
{
    public EnemySpawner enemySpawner;

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
                enemySpawner.DeleteField();
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

    public void StartGame() // 게임 시작. 현재는 스테이지 변수 초기화만 있으며 아무것도 안함 이거 수정하면서 위에 변수들 초기화 설정값 변경할 것 (필드를 시작할 때 값을 수정하므로 유의)
    {
        world = 1;
        stage = 0; // 시작할때마다 값을 1씩 더해주므로 0부터 시작할 것
        enemyTotalKill = 0;
        onField = true;
    }

    public void StartStage() // 현재 스테이지 시작하며 변수를 초기화하고 EnemySpawner에 현재 스테이지 정보 전달
    {
        Managers.Status.Hp = Managers.Status.MaxHp;
        enemyKill = 0;
        curEnemyCount = 0;
        nowStage = Array.Find(Managers.Asset.StageTemplates, stageSO => stageSO.world == world && stageSO.stage == stage);
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
