using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour // 적 스폰을 컨트롤하는 코드
{
    // 맵 내/외부 스폰 영역
    [Header("Spawn Area Bounds")]
    public static Vector2 SPAWN_AREA_MIN = new Vector2(-20, -11);
    public static Vector2 SPAWN_AREA_MAX = new Vector2(20, 11);

    [Header("Spawn Indicator")]
    public GameObject onScreenSpawnIndicatorPrefab; // 화면 안 스폰 위치 표시를 위한 인디케이터
    public GameObject offScreenSpawnIndicatorPrefab; // 화면 밖 스폰 위치 표시를 위한 인디케이터
    private float onScreenindicatorDuration = 1.4f; // 인디케이터 표시 시간
    private float offScreenindicatorDuration = 1f; // 인디케이터 표시 시간

    private void Start()
    {
        Managers.Stage.enemySpawner = this;
        Managers.Stage.StartStage(); // nowStage 재설정
        StartCoroutine(CoSpawnEnemyRoutine(Managers.Stage.NowStage));
    }

    private IEnumerator CoSpawnEnemyRoutine(StageSO nowStage)
    {
        Debug.Log("ControlField->Start Spawning Waves");

        for (int waveIndex = 0; waveIndex < nowStage.enemyWave.Length; waveIndex++)
        {
            EnemyWaveSO wave = nowStage.enemyWave[waveIndex];

            // Wave 시작 전 지정된 시간 간격 전파
            yield return new WaitForSeconds(nowStage.wavePreparationTime[waveIndex]);

            // Wave 소환
            yield return StartCoroutine(SpawnWaveRoutine(wave, nowStage.waveCount[waveIndex]));

            // 보스 스테이지의 경우에는 모든 적이 제거될 때까지 대기하지 않고 다음 웨이브 소환
            if (nowStage.isBossStage)
            {
                continue;
            }

            // 모든 적 제거될 때까지 대기
            while (Managers.Stage.CurEnemyCount > 0)
            {
                // to do: 효율적인 방법 구현
                yield return null;
            }

            Debug.Log($"Wave {waveIndex + 1} Cleared");
        }

        yield return new WaitForSeconds(0.5f); // 마지막 웨이브 후 잠시 대기
        // 보스스테이지가 아닌 경우 모든 Wave 끝나면 Stage 종료
        if (!nowStage.isBossStage)
        {
            Managers.Stage.OnField = false;
        }
        Debug.Log("Stage Completed");
    }

    private IEnumerator SpawnWaveRoutine(EnemyWaveSO wave, int waveCount)
    {
        for (int i = 0; i < waveCount; i++)
        {
            for (int j = 0; j < wave.enemyPrefabs.Length; j++)
            {
                GameObject enemyPrefab = wave.enemyPrefabs[j];
                ESpawnPositionType type = wave.spawnPositionType[j];


                Vector3 spawnPos = CalculateSpawnPosition(type, enemyPrefab, nowBoss: wave.isBossWave);
                Vector3 IndicatorPos = spawnPos;

                if (type == ESpawnPositionType.OffScreenRandom)
                {
                    IndicatorPos.x = Mathf.Clamp(IndicatorPos.x, SPAWN_AREA_MIN.x, SPAWN_AREA_MAX.x);
                    IndicatorPos.y = Mathf.Clamp(IndicatorPos.y, SPAWN_AREA_MIN.y, SPAWN_AREA_MAX.y);
                }

                // 인디케이터 표시
                GameObject IndcatorPrefab = type == ESpawnPositionType.OnScreenRandom ? onScreenSpawnIndicatorPrefab : offScreenSpawnIndicatorPrefab;
                float indicatorDuration = type == ESpawnPositionType.OnScreenRandom ? onScreenindicatorDuration : offScreenindicatorDuration;
                GameObject indicator = Instantiate(IndcatorPrefab, IndicatorPos, Quaternion.identity);
                Destroy(indicator, indicatorDuration);

                // 인디케이터 후 실제 소환
                StartCoroutine(DelayedSpawn(enemyPrefab, spawnPos, 0.6f));
            
            }
            yield return new WaitForSeconds(wave.waveInterval);
        }
        yield break;
    }

    public void SpawnEnemys(EnemyWaveSO wave, int waveCount) // 외부에서 EnemySpawner를 통해 적을 소환할 때 사용되는 메서드
    {
        StartCoroutine(SpawnWaveRoutine(wave, waveCount));
    }


    private IEnumerator DelayedSpawn(GameObject prefab, Vector3 position, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(prefab, position, prefab.transform.rotation);
    }

    private Vector3 CalculateSpawnPosition(ESpawnPositionType type, GameObject prefab, bool nowBoss)
    {
        Vector3 pos = Vector3.zero;

        switch (type)
        {
            case ESpawnPositionType.OnScreenRandom:
                pos = GetOnScreenRandomPos();
                break;

            case ESpawnPositionType.OffScreenRandom:
                pos = GetOffScreenRandomPos();
                break;

            case ESpawnPositionType.RightSideCenter:
                pos = new Vector3(SPAWN_AREA_MAX.x + 6f, 0f, 0f);
                break;
        }

        return pos;
    }

    private Vector3 GetOnScreenRandomPos()
    {
        float x = Random.Range(SPAWN_AREA_MIN.x, SPAWN_AREA_MAX.x);
        float y = Random.Range(SPAWN_AREA_MIN.y, SPAWN_AREA_MAX.y);
        return new Vector3(x, y, 0f);
    }

    private Vector3 GetOffScreenRandomPos()
    {
        // 화면 밖 랜덤 위치 (화면 안쪽은 제외)
        // 화면 경계 바깥 4면 중 하나를 랜덤 선택하여 그 면에서만 소환
        int side = Random.Range(0, 6); // 0:좌, 1:우, 2,3:상, 4,5:하
        float xOff, yOff;
        Vector3 pos = Vector3.zero;
        switch (side)
        {
            case 0: // Left
                xOff = SPAWN_AREA_MIN.x - 5f;
                yOff = Random.Range(SPAWN_AREA_MIN.y, SPAWN_AREA_MAX.y);
                pos = new Vector3(xOff, yOff, 0f);
                break;
            case 1: // Right
                xOff = SPAWN_AREA_MAX.x + 5f;
                yOff = Random.Range(SPAWN_AREA_MIN.y, SPAWN_AREA_MAX.y);
                pos = new Vector3(xOff, yOff, 0f);
                break;
            case 2:
            case 3:// Top
                xOff = Random.Range(SPAWN_AREA_MIN.x, SPAWN_AREA_MAX.x);
                yOff = SPAWN_AREA_MAX.y + 5f;
                pos = new Vector3(xOff, yOff, 0f);
                break;
            case 4:
            case 5: // Bottom
                xOff = Random.Range(SPAWN_AREA_MIN.x, SPAWN_AREA_MAX.x);
                yOff = SPAWN_AREA_MIN.y - 5f;
                pos = new Vector3(xOff, yOff, 0f);
                break;
        }
        return pos;
    }



    public void DeleteField() // 스테이지 종료 시 모든 적과 적/아군 투사체 제거
    {
        StopAllCoroutines();
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = enemys.Length - 1; i >= 0; i--)
        {
            enemys[i].GetComponent<EnemyHP>().Die();
        }
        GameObject[] enemyProjectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        for (int i = enemyProjectiles.Length - 1; i >= 0; i--) Destroy(enemyProjectiles[i]);
        GameObject[] playerProjectiles = GameObject.FindGameObjectsWithTag("PlayerProjectile");
        for (int i = playerProjectiles.Length - 1; i >= 0; i--) Destroy(playerProjectiles[i]);
    }
}
