using UnityEngine;

public class AfterImageSpawner : MonoBehaviour
{
    public Animator sourceAnimator;
    // 잔상
    public GameObject afterImagePrefab;
    public Material afterImageMaterial;
    public float spawnInterval = 0.05f;

    private float timer;
    private Rigidbody2D rb;

    // 오브젝트 풀
    public int poolSize = 20;
    private GameObject[] afterImagePool;
    private int poolIndex = 0;

    // 그림자
    public float shadowAlpha = 1f;

    private GameObject shadowInstance;
    private float shadowTimer = 0f;
    public float shadowBlinkInterval = 0.15f;
    public  Animator shadowAnimator;

    void Start()
    {
        sourceAnimator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Init();
        ShadowInit();
    }

    void Update()
    {
        timer += Time.deltaTime;
        shadowTimer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnAfterImage();
            timer = 0f;
        }

        if (shadowTimer >= shadowBlinkInterval)
        {
            ToggleShadow();
            shadowTimer = 0f;
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

    void ShadowInit()
    {
        // 그림자 초기화
        shadowInstance = Instantiate(afterImagePrefab, transform.position, Quaternion.identity, transform);

        // 스프라이트 복사
        SpriteRenderer[] originalRenderers = GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer[] cloneRenderers = shadowInstance.GetComponentsInChildren<SpriteRenderer>();

        foreach (var original in originalRenderers)
        {
            foreach (var cloneSR in cloneRenderers)
            {
                if (cloneSR.name == original.name)
                {
                    cloneSR.sprite = original.sprite;
                    cloneSR.flipX = original.flipX;
                    cloneSR.color = new Color(1f, 1f, 1f, 1f);
                    cloneSR.material = afterImageMaterial;
                }
            }
        }

        shadowAnimator = shadowInstance.GetComponent<AfterImageFadeOut>().animator;
        shadowAnimator.runtimeAnimatorController = sourceAnimator.runtimeAnimatorController;
        shadowInstance.SetActive(false);
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
                    cloneSR.color = new Color(1f, 1f, 1f, 0.2f);
                    cloneSR.material = afterImageMaterial;
                    clone.transform.localScale = transform.localScale;
                }
            }
        }

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

    void ToggleShadow()
    {
        if (shadowInstance == null)
        {
            ShadowInit();
            return;
        }

        if (!shadowInstance.activeSelf)
        {
            shadowInstance.SetActive(true);
            if (sourceAnimator != null && shadowAnimator != null)
            {
                shadowAnimator.SetBool("5_Fall", true);
                // 현재 상태 얻기
                AnimatorStateInfo state = sourceAnimator.GetCurrentAnimatorStateInfo(0);

                // 재생 (speed는 나중에 0으로)
                shadowAnimator.Play(state.fullPathHash, 0, state.normalizedTime);

                print("왜 안돼!!!!!!!!!!");
            }
        }
        else
        {
            shadowInstance.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (shadowInstance != null) shadowInstance.SetActive(false);
    }
}