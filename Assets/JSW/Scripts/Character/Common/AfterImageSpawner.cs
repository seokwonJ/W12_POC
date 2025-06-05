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

        // ���ϴ� ��Ȳ���� �ܻ� ����: ��) ��� ���� ��
        if (ShouldSpawnAfterImage() && timer >= spawnInterval)
        {
            SpawnAfterImage();
            timer = 0f;
        }
    }

    bool ShouldSpawnAfterImage()
    {
        // ���� ����: true�� �׻�, �Ǵ� ĳ���Ͱ� ��� ���� �� ��
        return true; // �ʿ信 ���� �ٲ���
    }

    void SpawnAfterImage()
    {
        GameObject clone = Instantiate(afterImagePrefab, transform.position, transform.rotation);
        SpriteRenderer cloneRenderer = clone.GetComponent<SpriteRenderer>();
        cloneRenderer.sprite = characterRenderer.sprite;
        cloneRenderer.flipX = characterRenderer.flipX;
    }
}
