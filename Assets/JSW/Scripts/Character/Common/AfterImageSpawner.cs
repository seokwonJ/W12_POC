using UnityEngine;

public class AfterImageSpawner : MonoBehaviour
{
    public GameObject afterImagePrefab;
    public Material afterImageMaterial;
    public float spawnInterval = 0.05f;

    private float timer;
    private Rigidbody2D rb;

    // 오브젝트 풀
    public int poolSize = 20;
    private GameObject[] afterImagePool;
    private int poolIndex = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Init();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnAfterImage();
            timer = 0f;
        }
    }
    void Init()
    {
        // 풀 초기화
        afterImagePool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            GameObject clone = Instantiate(afterImagePrefab);
            clone.SetActive(false);
            afterImagePool[i] = clone;
        }
    }

    void SpawnAfterImage()
    {
        GameObject clone = afterImagePool[poolIndex];
        if (clone == null)
        {
            Init();
            return;
        }

        else
        {
            clone.transform.position = transform.position;
            clone.transform.rotation = transform.rotation;
            clone.SetActive(true);
        }

        poolIndex = (poolIndex + 1) % poolSize;

        // 스프라이트 복사
        SpriteRenderer[] originalRenderers = GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer[] cloneRenderers = clone.GetComponentsInChildren<SpriteRenderer>();

        foreach (var original in originalRenderers)
        {
            foreach (var cloneSR in cloneRenderers)
            {
                if (cloneSR.name == original.name)
                {
                    cloneSR.sprite = original.sprite;
                    cloneSR.flipX = original.flipX;
                    cloneSR.color = new Color(1f, 1f, 1f, 0.7f);
                    cloneSR.material = afterImageMaterial;
                }
            }
        }

        // Animator 동기화
        Animator sourceAnimator = GetComponent<Animator>();
        Animator cloneAnimator = clone.GetComponent<AfterImageFadeOut>().animator;

        if (sourceAnimator != null && cloneAnimator != null)
        {
            AnimatorStateInfo state = sourceAnimator.GetCurrentAnimatorStateInfo(0);
            cloneAnimator.Play(state.fullPathHash, 0, state.normalizedTime);
            cloneAnimator.speed = 0f;
        }

        // 페이드 시작
        clone.GetComponent<AfterImageFadeOut>().StartFade();
    }
}