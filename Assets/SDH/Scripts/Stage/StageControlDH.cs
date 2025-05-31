using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageControlDH : MonoBehaviour // 적 스폰과 스테이지 종료를 컨트롤하는 스크립트
{
    private const int MIN_RIGHT_SIDE_INDEX = 5; // 오른쪽 면에 있는 위치 인덱스의 최소값
    private const int MAX_RIGHT_SIDE_INDEX = 12; // 오른쪽 면에 있는 위치 인덱스의 최대값

    public Transform[] spawnPoints; // 임시 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public TextMeshProUGUI currentTimeDisplay;

    private StageSO nowStage;
    private float currentTime;

    private void Start()
    {
        Managers.Rider.Init(); // RiderManager 수정하면서 여기도 수정 필요 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        nowStage = Managers.Stage.GetNowStageTemplate();

        StartCoroutine(CoSpawnEnemyRoutine(nowStage.enemyWave, nowStage.WaveCount, nowStage.WaveInterval, nowStage.stagePlayTime));
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        currentTimeDisplay.text = ((int)currentTime).ToString();

        if (currentTime <= 0)
        {
            Debug.Log("현재 스테이지 끝");
            Managers.Stage.Stage++;

            SceneManager.LoadScene("StoreEx_SDH");
        }
    }

    private IEnumerator CoSpawnEnemyRoutine(EnemyWaveSO[] enemyWave, int[] WaveCount, float[] WaveInterval, float stageplayTime)
    {
        Debug.Log("EnemySpawnerJH->StageControlDH Start Coroutine");
        currentTime = stageplayTime;
        for (int waveIndex = 0; waveIndex < enemyWave.Length; waveIndex++) // 웨이브1, 2, 3, 4... 를 반복
        {
            EnemyWaveSO enemyWaveSO = enemyWave[waveIndex];
            for (int i = 0; i < WaveCount[waveIndex]; i++) // 각 웨이브를 소환하는 횟수만큼 반복
            {
                SpawnEnemyWave(enemyWaveSO);
                yield return new WaitForSeconds(WaveInterval[waveIndex]);
            }
        }
    }

    private void SpawnEnemyWave(EnemyWaveSO enemyWaveSO)
    {
        Debug.Log("Spawning enemy wave: " + enemyWaveSO.name);
        Dictionary<int, GameObject> spawnDictonary = new Dictionary<int, GameObject>();

        for (int i = 0; i < enemyWaveSO.enemyPrefabs.Length; i++) // 각 웨이브의 적을 소환
        {
            GameObject enemyPrefab = enemyWaveSO.enemyPrefabs[i];
            int enemyCount = enemyWaveSO.enemyCount[i];
            ESpawnPositionType spawnPositionType = enemyWaveSO.spawnPositionType[i];

            for (int j = 0; j < enemyCount; j++)
            {
                // ESpawnPositionType가 Random인 경우 spawnPoints 중에서 랜덤한 인덱스, RightSideRandom인 경우 MIN_RIGHT_SIDE_INDEX이상 MAX_RIGHT_SIDE_INDEX 이하의 인덱스에서 랜덤하게
                // 이미   spawnDictonary에 있는 인덱스는 제외하고 랜덤하게 뽑는다.
                int spawnIndex = -1;
                if (spawnPositionType == ESpawnPositionType.Random)
                {
                    do
                    {
                        spawnIndex = Random.Range(0, spawnPoints.Length);
                    } while (spawnDictonary.ContainsKey(spawnIndex));
                }
                else if (spawnPositionType == ESpawnPositionType.RightSideRandom)
                {
                    do
                    {
                        spawnIndex = Random.Range(MIN_RIGHT_SIDE_INDEX, MAX_RIGHT_SIDE_INDEX + 1);
                    } while (spawnDictonary.ContainsKey(spawnIndex));
                }
                else if (spawnPositionType == ESpawnPositionType.EvenlySpaced)
                {
                    // 균등한 간격으로 배치하기. 1개면 중앙에 배치하고 2개면 위부터 1/3, 2/3 위치에 배치
                    // 가장 위는 MIN_RIGHT_SIDE_INDEX, 가장 아래는 MAX_RIGHT_SIDE_INDEX로 간주하고,

                    int totalCount = enemyCount;
                    int positionIndex = j % totalCount; // 현재 몇 번째 적인지
                    float spacing = 1f / totalCount; // 간격 계산
                    float positionY = (positionIndex + 0.5f) * spacing; // 중앙에 배치하기 위해 0.5를 더함
                    // MIN_RIGHT_SIDE_INDEX ~ MAX_RIGHT_SIDE_INDEX 범위 내에서 균등하게 인덱스 계산
                    spawnIndex = Mathf.RoundToInt(positionY * (MAX_RIGHT_SIDE_INDEX - MIN_RIGHT_SIDE_INDEX) + MIN_RIGHT_SIDE_INDEX);
                }

                spawnDictonary.Add(spawnIndex, enemyPrefab);

            }
        }

        Debug.Log(spawnDictonary.Count + " enemies to spawn in this wave.");

        // spawnDictonary에 있는 인덱스에 해당하는 spawnPoints에서 enemyPrefab을 Instantiate
        foreach (var kvp in spawnDictonary)
        {
            int spawnIndex = kvp.Key;
            GameObject enemyPrefab = kvp.Value;
            Transform spawnPosition = spawnPoints[spawnIndex];
            Instantiate(enemyPrefab, spawnPosition.position, enemyPrefab.transform.rotation);
        }
    }
}
