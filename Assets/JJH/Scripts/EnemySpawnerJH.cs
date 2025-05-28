using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EnemySpawnerJH : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public Transform[] spawnPoints;
    
    public float Enemy1SpawnDelay = 5;
    float Enemy1SpawnCurrentDelay;
    public float Enemy2SpawnDelay = 3;
    float Enemy2SpawnCurrentDelay;

    public float playTime;
    float currentPlayTime = 0;
    public Text playTime_Text;

    private void Start()
    {
        Enemy1SpawnCurrentDelay = Enemy1SpawnDelay;
        Enemy2SpawnCurrentDelay = Enemy2SpawnDelay;
        currentPlayTime = playTime;
    }

    private void Update()
    {
        Enemy1SpawnCurrentDelay -= Time.deltaTime;
        Enemy2SpawnCurrentDelay -= Time.deltaTime;
        currentPlayTime -= Time.deltaTime;
        playTime_Text.text = ((int)currentPlayTime).ToString();

        if (currentPlayTime <= 0)
        {
            GameSceneManager.Instance.GameClearUI();
        }

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
    }
}
