using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public float cloudSpawnTime;
    public GameObject cloud;
    private float currentCloudSpawnTime;

    private void Start()
    {
        currentCloudSpawnTime = cloudSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentCloudSpawnTime -= Time.deltaTime;
        if(currentCloudSpawnTime < 0)
        {
            currentCloudSpawnTime = cloudSpawnTime;
            float randomPos = Random.Range(-9.0f, 12.0f);
            Instantiate(cloud, new Vector3(transform.position.x, randomPos, 0),Quaternion.identity);
        }
    }
}
