using UnityEngine;

public class FallingAfterImageSpawner : MonoBehaviour
{
    public GameObject afterImagePrefab;
    public float spawnInterval = 0.02f;

    private float timer;
    private Rigidbody2D rb;

    public Material afterImageMaterial; // <- Inspector에 할당

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (rb.linearVelocity.y < -0.1f && timer >= spawnInterval)
        {
            SpawnAfterImage();
            timer = 0f;
        }
    }

    void SpawnAfterImage()
    {
        GameObject clone = Instantiate(afterImagePrefab, transform.position, transform.rotation);

        // 각 부위 sprite 복사
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
                    cloneSR.color = new Color(1f, 1f, 1f, 0.7f); // 검은 실루엣
                    cloneSR.material = afterImageMaterial;

                }
            }
        }

        // 3. Animator 동기화
        Animator sourceAnimator = GetComponent<Animator>();
        Animator cloneAnimator = clone.GetComponent<AfterImageFadeOut>().animator;

        if (sourceAnimator != null && cloneAnimator != null)
        {
            AnimatorStateInfo state = sourceAnimator.GetCurrentAnimatorStateInfo(0);
            cloneAnimator.Play(state.fullPathHash, 0, state.normalizedTime);
            cloneAnimator.speed = 0f;
        }
    }
}