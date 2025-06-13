using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EnemySpawner_Prev : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public Transform[] spawnPoints;
    
    float Enemy1SpawnDelay = 3;
    float Enemy1SpawnCurrentDelay;
    float Enemy2SpawnDelay = 4;
    float Enemy2SpawnCurrentDelay;
    float Enemy3SpawnDelay = 7;
    float Enemy3SpawnCurrentDelay;

    public float playTime;
    float currentPlayTime = 0;
    public Text playTime_Text;

    private void Start()
    {
        Enemy1SpawnCurrentDelay = Enemy1SpawnDelay;
        Enemy2SpawnCurrentDelay = Enemy2SpawnDelay;
        Enemy3SpawnCurrentDelay = Enemy3SpawnDelay;
        currentPlayTime = playTime;
    }

    private void Update()
    {
        Enemy1SpawnCurrentDelay -= Time.deltaTime;
        Enemy2SpawnCurrentDelay -= Time.deltaTime;
        Enemy3SpawnCurrentDelay -= Time.deltaTime;
        currentPlayTime -= Time.deltaTime;
        playTime_Text.text = ((int)currentPlayTime).ToString();

        if (Enemy1SpawnCurrentDelay < 0)
        {
            Enemy1SpawnCurrentDelay = Enemy1SpawnDelay;
            Transform spawnPosition = spawnPoints[Random.Range(0,spawnPoints.Length)];
            Instantiate(enemy1,spawnPosition.position, Quaternion.identity);
        }
        if (Enemy2SpawnCurrentDelay < 0)
        {
            Enemy2SpawnCurrentDelay = Enemy2SpawnDelay;
            Transform spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemy2, spawnPosition.position, Quaternion.identity);
        }
        if (Enemy3SpawnCurrentDelay < 0)
        {
            Enemy3SpawnCurrentDelay = Enemy3SpawnDelay;
            // 5이상 12 이하의 숫자 인덱스에서 중복되지 않은 2개를 뽑는다. 5이상 12이하 인덱스가 오른쪽 면에 있는 위치임
            // spawnPoints에서 해당 인덱스에서 enemy3를 각각 Instantiate
            int index1 = Random.Range(5, 13);
            int index2;
            do
            {
                index2 = Random.Range(5, 13);
            } while (index1 == index2);
            Transform spawnPosition1 = spawnPoints[index1];
            Transform spawnPosition2 = spawnPoints[index2];
            Instantiate(enemy3, spawnPosition1.position, enemy3.transform.rotation);
            Instantiate(enemy3, spawnPosition2.position, enemy3.transform.rotation);
        }
    }
}
