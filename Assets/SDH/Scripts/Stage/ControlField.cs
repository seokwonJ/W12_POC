using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ControlField : MonoBehaviour // 적 스폰을 컨트롤하는 코드이며 편의상 타이머 기능도 겸함 (EnemySpawnerJH.cs에서 가져옴)
{
    // 맵 내/외부 스폰 영역
    [Header("Spawn Area Bounds")]
    [SerializeField] private Vector2 spawnAreaMin;
    [SerializeField] private Vector2 spawnAreaMax;

    [Header("Spawn Indicator")]
    [SerializeField] private GameObject spawnIndicatorPrefab; // 스폰 위치 표시를 위한 인디케이터
    [SerializeField] private float indicatorDuration = 2f; // 인디케이터 표시 시간

    private void Start()
    {
        Managers.Stage.controlField = this;
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
            yield return new WaitForSeconds(wave.waveInterval);

            // Wave 소환
            yield return StartCoroutine(SpawnWaveRoutine(wave, nowStage.waveCount[waveIndex]));

            // 모든 적 제거될 때까지 대기
            while (Managers.Stage.CurEnemyCount > 0)
            {
                // to do: 효율적인 방법 구현
                yield return null;
            }

            Debug.Log($"Wave {waveIndex + 1} Cleared");
        }

        yield return new WaitForSeconds(0.5f); // 마지막 웨이브 후 잠시 대기
        // 모든 Wave 끝나면 Stage 종료
        Managers.Stage.OnField = false;
        Debug.Log("Stage Completed");
    }

    // to do 코드 고치기
    private IEnumerator SpawnWaveRoutine(EnemyWaveSO wave, int waveCount)
    {
        for (int i = 0; i < waveCount; i++)
        {
            for (int j = 0; j < wave.enemyPrefabs.Length; j++)
            {
                GameObject enemyPrefab = wave.enemyPrefabs[j];
                ESpawnPositionType type = wave.spawnPositionType[j];


                Vector3 spawnPos = CalculateSpawnPosition(type, enemyPrefab, nowBoss: wave.isBossWave);

                // 인디케이터 표시
                GameObject indicator = Instantiate(spawnIndicatorPrefab, spawnPos, Quaternion.identity);
                Destroy(indicator, indicatorDuration);

                // 인디케이터 후 실제 소환
                StartCoroutine(DelayedSpawn(enemyPrefab, spawnPos, indicatorDuration));
            
            }
            yield return new WaitForSeconds(wave.waveInterval);
        }
        yield break;
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
                float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
                float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
                pos = new Vector3(x, y, 0f);
                break;
        }

        return pos;
    }


    private Vector3 GetBossFixedPosition(GameObject bossPrefab)
    {
        // to do
        //// BossWaveSO에 위치 리스트가 있다고 가정
        //// 예시로 Stage 구조체에 bossPositions가 있다면
        //var positions = Managers.Stage.NowStage.bossPositions;
        //if (positions != null && positions.Length > 0)
        //    return positions[0];

        // 기본 중앙 위치 반환
        return Vector3.zero;
    }


    public void DeleteField() // 스테이지 종료 시 모든 적과 적/아군 투사체 제거
    {
        StopAllCoroutines();
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = enemys.Length - 1; i >= 0; i--) Destroy(enemys[i]);
        GameObject[] enemyProjectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        for (int i = enemyProjectiles.Length - 1; i >= 0; i--) Destroy(enemyProjectiles[i]);
        GameObject[] playerProjectiles = GameObject.FindGameObjectsWithTag("PlayerProjectile");
        for (int i = playerProjectiles.Length - 1; i >= 0; i--) Destroy(playerProjectiles[i]);
    }
}
