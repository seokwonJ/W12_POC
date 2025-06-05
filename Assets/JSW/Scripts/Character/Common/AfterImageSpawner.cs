using UnityEngine;

public class AfterImageSpawner : MonoBehaviour
{
    public GameObject afterImagePrefab;
    public float spawnInterval = 0.05f;
    private float timer;

    private SpriteRenderer characterRenderer;

    void Start()
    {
        characterRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 원하는 상황에만 잔상 생성: 예) 대시 중일 때
        if (ShouldSpawnAfterImage() && timer >= spawnInterval)
        {
            SpawnAfterImage();
            timer = 0f;
        }
    }

    bool ShouldSpawnAfterImage()
    {
        // 조건 예시: true면 항상, 또는 캐릭터가 대시 중일 때 등
        return true; // 필요에 따라 바꿔줘
    }

    void SpawnAfterImage()
    {
        GameObject clone = Instantiate(afterImagePrefab, transform.position, transform.rotation);
        SpriteRenderer cloneRenderer = clone.GetComponent<SpriteRenderer>();
        cloneRenderer.sprite = characterRenderer.sprite;
        cloneRenderer.flipX = characterRenderer.flipX;
    }
}
