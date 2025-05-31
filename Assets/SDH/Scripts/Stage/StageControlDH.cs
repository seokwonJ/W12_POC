using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageControlDH : MonoBehaviour // �� ������ �������� ���Ḧ ��Ʈ���ϴ� ��ũ��Ʈ
{
    private const int MIN_RIGHT_SIDE_INDEX = 5; // ������ �鿡 �ִ� ��ġ �ε����� �ּҰ�
    private const int MAX_RIGHT_SIDE_INDEX = 12; // ������ �鿡 �ִ� ��ġ �ε����� �ִ밪

    public Transform[] spawnPoints; // �ӽ� !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    public TextMeshProUGUI currentTimeDisplay;

    private StageSO nowStage;
    private float currentTime;

    private void Start()
    {
        Managers.Rider.Init(); // RiderManager �����ϸ鼭 ���⵵ ���� �ʿ� @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        nowStage = Managers.Stage.GetNowStageTemplate();

        StartCoroutine(CoSpawnEnemyRoutine(nowStage.enemyWave, nowStage.WaveCount, nowStage.WaveInterval, nowStage.stagePlayTime));
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
        currentTimeDisplay.text = ((int)currentTime).ToString();

        if (currentTime <= 0)
        {
            Debug.Log("���� �������� ��");
            Managers.Stage.Stage++;

            SceneManager.LoadScene("StoreEx_SDH");
        }
    }

    private IEnumerator CoSpawnEnemyRoutine(EnemyWaveSO[] enemyWave, int[] WaveCount, float[] WaveInterval, float stageplayTime)
    {
        Debug.Log("EnemySpawnerJH->StageControlDH Start Coroutine");
        currentTime = stageplayTime;
        for (int waveIndex = 0; waveIndex < enemyWave.Length; waveIndex++) // ���̺�1, 2, 3, 4... �� �ݺ�
        {
            EnemyWaveSO enemyWaveSO = enemyWave[waveIndex];
            for (int i = 0; i < WaveCount[waveIndex]; i++) // �� ���̺긦 ��ȯ�ϴ� Ƚ����ŭ �ݺ�
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

        for (int i = 0; i < enemyWaveSO.enemyPrefabs.Length; i++) // �� ���̺��� ���� ��ȯ
        {
            GameObject enemyPrefab = enemyWaveSO.enemyPrefabs[i];
            int enemyCount = enemyWaveSO.enemyCount[i];
            ESpawnPositionType spawnPositionType = enemyWaveSO.spawnPositionType[i];

            for (int j = 0; j < enemyCount; j++)
            {
                // ESpawnPositionType�� Random�� ��� spawnPoints �߿��� ������ �ε���, RightSideRandom�� ��� MIN_RIGHT_SIDE_INDEX�̻� MAX_RIGHT_SIDE_INDEX ������ �ε������� �����ϰ�
                // �̹�   spawnDictonary�� �ִ� �ε����� �����ϰ� �����ϰ� �̴´�.
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
                    // �յ��� �������� ��ġ�ϱ�. 1���� �߾ӿ� ��ġ�ϰ� 2���� ������ 1/3, 2/3 ��ġ�� ��ġ
                    // ���� ���� MIN_RIGHT_SIDE_INDEX, ���� �Ʒ��� MAX_RIGHT_SIDE_INDEX�� �����ϰ�,

                    int totalCount = enemyCount;
                    int positionIndex = j % totalCount; // ���� �� ��° ������
                    float spacing = 1f / totalCount; // ���� ���
                    float positionY = (positionIndex + 0.5f) * spacing; // �߾ӿ� ��ġ�ϱ� ���� 0.5�� ����
                    // MIN_RIGHT_SIDE_INDEX ~ MAX_RIGHT_SIDE_INDEX ���� ������ �յ��ϰ� �ε��� ���
                    spawnIndex = Mathf.RoundToInt(positionY * (MAX_RIGHT_SIDE_INDEX - MIN_RIGHT_SIDE_INDEX) + MIN_RIGHT_SIDE_INDEX);
                }

                spawnDictonary.Add(spawnIndex, enemyPrefab);

            }
        }

        Debug.Log(spawnDictonary.Count + " enemies to spawn in this wave.");

        // spawnDictonary�� �ִ� �ε����� �ش��ϴ� spawnPoints���� enemyPrefab�� Instantiate
        foreach (var kvp in spawnDictonary)
        {
            int spawnIndex = kvp.Key;
            GameObject enemyPrefab = kvp.Value;
            Transform spawnPosition = spawnPoints[spawnIndex];
            Instantiate(enemyPrefab, spawnPosition.position, enemyPrefab.transform.rotation);
        }
    }
}
