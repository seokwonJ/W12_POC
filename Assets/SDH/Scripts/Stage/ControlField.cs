using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlField : MonoBehaviour // �� ������ ��Ʈ���ϴ� �ڵ��̸� ���ǻ� Ÿ�̸� ��ɵ� ���� (EnemySpawnerJH.cs���� ������)
{
    private const int MIN_RIGHT_SIDE_INDEX = 5; // ������ �鿡 �ִ� ��ġ �ε����� �ּҰ�
    private const int MAX_RIGHT_SIDE_INDEX = 12; // ������ �鿡 �ִ� ��ġ �ε����� �ִ밪

    public Transform[] spawnPoints; // �ӽ� !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    [SerializeField] private TextMeshProUGUI currentTimeTxt;

    private StageSO nowStage;

    private void Start()
    {
        nowStage = Managers.Stage.StartStage();

        StartCoroutine(StageTimer(nowStage.stagePlayTime));
        StartCoroutine(CoSpawnEnemyRoutine(nowStage));
    }

    private IEnumerator StageTimer(float stagePlayTime)
    {
        float currentTime = stagePlayTime;

        while (currentTime >= 0)
        {
            currentTimeTxt.text = Mathf.Ceil(currentTime).ToString();

            currentTime -= Time.deltaTime;
            yield return null;
        }

        currentTimeTxt.text = "0";
        Debug.Log("���� �������� ��");

        DeleteEnemy();
        Managers.Stage.OnField = false;
    }

    private IEnumerator CoSpawnEnemyRoutine(StageSO nowStage)
    {
        Debug.Log("EnemySpawnerJH->StageControlDH Start Coroutine");
        for (int waveIndex = 0; waveIndex < nowStage.enemyWave.Length; waveIndex++) // ���̺�1, 2, 3, 4... �� �ݺ�
        {
            EnemyWaveSO enemyWaveSO = nowStage.enemyWave[waveIndex];
            for (int i = 0; i < nowStage.WaveCount[waveIndex]; i++) // �� ���̺긦 ��ȯ�ϴ� Ƚ����ŭ �ݺ�
            {
                SpawnEnemyWave(enemyWaveSO);
                yield return new WaitForSeconds(nowStage.WaveInterval[waveIndex]);
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

    public void DeleteEnemy() // �������� ���� �� ��� ���� ��/�Ʊ� ����ü ����
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
