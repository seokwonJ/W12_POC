﻿using System.Collections;
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

    private float dieDelay = 0.4f;
    private Coroutine flashCoroutine;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int hp)
    {
        if (isDead) return;

        enemyHP -= hp;
        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(CoDamagedEffect());
        SoundManager.Instance.PlaySFX("HitSound");

        if (enemyHP <= 0)
        {

            Boss boss = GetComponent<Boss>(); // 보스라면 StageManager의 OnField를 False로 설정
            if (boss != null)
            {
                Managers.Stage.OnField = false; // 보스가 죽으면 필드 종료
            }

            Managers.Stage.PlusEnemyKill(transform.position);
            Die();
        }
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }

        Managers.Stage.CurEnemyCount--; // 현재 스테이지에서 남아있는 적 수 감소
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
