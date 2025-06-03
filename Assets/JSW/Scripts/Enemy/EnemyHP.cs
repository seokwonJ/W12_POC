using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int enemyHP;
    public bool isDead = false;

    private Renderer renderer;
    private Collider2D collider;
    private Animator animator;
    private Rigidbody2D rb;
    private WaitForSeconds flashDuration = new WaitForSeconds(0.1f);
    // 사망후 애니메이션 기다리는 시간
    private WaitForSeconds dieEffectWaitTime = new WaitForSeconds(0.1f);
    private float dieDelay = 0.5f;


    private void Awake()
    {
        //dieEffectDuration = new WaitForSeconds(dieDelay);
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int hp)
    {
        if (isDead) return;

        enemyHP -= hp;
        StartCoroutine(CoDamagedEffect());

        if (enemyHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        if (collider != null)
        {
            collider.enabled = false; // 사망시 콜라이더 비활성화
        }
        gameObject.tag = "Untagged"; // 사망시 태그 제거
        rb.linearVelocity = Vector2.zero; // 죽을 때 속도 초기화
        StartCoroutine(CoDieEffect());
    }
    IEnumerator CoDamagedEffect()
    {
        renderer.material.EnableKeyword("_ISFLASHED");
        yield return flashDuration;
        renderer.material.DisableKeyword("_ISFLASHED");
    }

    IEnumerator CoDieEffect()
    {
        // 애니메이션 정지
        if (animator != null)
        {
            animator.enabled = false;
        }
        yield return dieEffectWaitTime;

        // _DieEffectValue가  dieDelay 시간에 걸쳐 1에서 0으로 감소
        float elapsedTime = 0f;
        while (elapsedTime < dieDelay)
        {
            // sin에 의한 부드러운 감소 효과
            float dieEffectValue = Mathf.Sin((1f - (elapsedTime / dieDelay)) * Mathf.PI * 0.5f);
            renderer.material.SetFloat("_DieEffectValue", dieEffectValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
