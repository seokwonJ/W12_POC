using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
// Random은 UnityEngine.Random으로 자동으로 사용하게
using Random = UnityEngine.Random; // UnityEngine.Random을 사용하기 위해 명시적으로 지정

public class EnemySpawnerJH : MonoBehaviour
{
    public Transform[] spawnPoints;

    public StageSO[] stageSOArray;
    private StageSO stageSO;

    private int world = 1; // 월드 번호, 1-2 일 경우 1
    private int stage = 1; // 스테이지 번호, 1-2 일 경우 2
    private EnemyWaveSO[] enemyWave; // 각 웨이브의 적 구성
    private int[] WaveCount; // 각 웨이브를 몇 번씩 소환할지 횟수
    private float[] WaveInterval; // 각 웨이브 사이의 시간 간격    

    private float stageplayTime;
    private float currentPlayTime;
    public Text playTime_Text;

    private const int MIN_RIGHT_SIDE_INDEX = 5; // 오른쪽 면에 있는 위치 인덱스의 최소값
    private const int MAX_RIGHT_SIDE_INDEX = 12; // 오른쪽 면에 있는 위치 인덱스의 최대값
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        stageSO = Array.Find(stageSOArray, stageSO => stageSO.world == world && stageSO.stage == stage);
        if (stageSO == null)
        {
            Debug.LogError("StageSO not found for world: " + world + ", stage: " + stage);
            return;
        }
        enemyWave = stageSO.enemyWave;
        WaveCount = stageSO.WaveCount;
        WaveInterval = stageSO.WaveInterval;
        stageplayTime = stageSO.stagePlayTime;
        currentPlayTime = stageplayTime;
        StartCoroutine(CoSpawnEnemyRoutine());
    }

    private void Update()
    {
        currentPlayTime -= Time.deltaTime;
        playTime_Text.text = ((int)currentPlayTime).ToString();
    }

    IEnumerator CoSpawnEnemyRoutine()
    {
        Debug.Log("EnemySpawnerJH Start Coroutine");
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
