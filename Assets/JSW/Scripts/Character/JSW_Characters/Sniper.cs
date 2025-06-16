using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSize = 1f;
    public float skillDamage = 1f;
    public float skillProjectileSpeed = 15;
    private bool _isSkillReady;
    public float skillSlowDelay = 0.3f;
    private Vector3 _beforeSkillVelocity = Vector3.zero;

    [Header("저격수 공격")]
    public int AttackMaxCount = 3;
    private int _AttackCurrentCount;
    public float realoadTime;

    [Header("강화")]
    public float nomalAttackSize;

    public int upgradeNum;

    public GameObject player;

    [Header("이펙트")]
    public SpriteRenderer dim;
    private float dimOriginAlpha;
    private Coroutine dimDarking;
    public GameObject sniperSkillActive1;
    public GameObject sniperSkillActive2;
    public GameObject sniperActiveShot;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMove>().gameObject;
        skillTargetList = new List<Transform>();
    }

    protected override void FixedUpdate()
    {
        if (_isSkillReady) rb.linearVelocity = _beforeSkillVelocity / 5;
        else
        {
            if (_beforeSkillVelocity != Vector3.zero)
            {
                rb.linearVelocity = _beforeSkillVelocity;
                _beforeSkillVelocity = Vector3.zero;
            }
        }
        if (rb.linearVelocity.y < -maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxFallSpeed);
        }
    }


    protected override IEnumerator NormalAttackRoutine()
    {
        _AttackCurrentCount = AttackMaxCount;

        while (true)
        {
            yield return new WaitForSeconds(normalFireInterval);

            if (_AttackCurrentCount <= 0)
            {
                animator.SetBool("isEndTReload", false);
                animator.Play("Reload", -1, 0.1f);

                Debug.Log("스나이퍼 장전 중");
                SoundManager.Instance.PlaySFX("SniperReloadStart");

                yield return new WaitForSeconds(realoadTime - 1);

                SoundManager.Instance.PlaySFX("SniperReloadEnd");

                yield return new WaitForSeconds(1);

                _AttackCurrentCount = AttackMaxCount;
                animator.SetBool("isEndTReload", true);
            }

            if (!isGround) continue;

            Transform target = FindMuchHPEnemy();
            if (target != null)
            {
                animator.Play("ATTACK", -1, 0f);
                _AttackCurrentCount -= 1;
                FireNormalProjectile(target.position);
            }
        }
    }

    // 일반 공격: 가까운 적에게 관통 공격
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        FireSniper();
    }

    List<Transform> skillTargetList;

    // 스킬: 느리고 커다란 관통 공격 3발 발사
    protected override IEnumerator FireSkill()
    {
        animator.Play("IDLE", -1, 0.1f);
        skillTargetList = new List<Transform>();

        if (dim == null)
        {
            dim = FindAnyObjectByType<Dim>().GetComponent<SpriteRenderer>();
            dimOriginAlpha = dim.color.a;
        }
        yield return new WaitForSeconds(skillInterval);
        SoundManager.Instance.PlaySFX("SniperSkillActive");
        animator.Play("SKILL", -1, 0f);
        Instantiate(sniperSkillActive2, transform.position, Quaternion.identity,transform);

        _isSkillReady = true;
        _beforeSkillVelocity = rb.linearVelocity;
        dimDarking = StartCoroutine(FadeAlphaTo(dim,0.8f,0.3f));
    
        yield return new WaitForSeconds(skillSlowDelay);
        Instantiate(sniperSkillActive1, transform.position, Quaternion.identity, transform);

        for (int i = 0; i < skillCount; i++)
        {
            animator.Play("ATTACK", -1, 0f);
            FireSkillProjectiles();
            //Instantiate(skillActiveEffect, transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(skillFireDelay);
        }

        StopCoroutine(dimDarking);
        StartCoroutine(FadeAlphaTo(dim, dimOriginAlpha, 0.5f));
        _AttackCurrentCount = AttackMaxCount;
        _isSkillReady = false;
    }

    public IEnumerator FadeAlphaTo(SpriteRenderer sr, float targetAlpha, float duration)
    {
        Color color = sr.color;
        float startAlpha = color.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            sr.color = new Color(color.r, color.g, color.b, newAlpha);
            yield return null;
        }

        // 마지막 값 보정
        sr.color = new Color(color.r, color.g, color.b, targetAlpha);

    }

    // 스킬 발사 구현
    protected override void FireSkillProjectiles()
    {
        FireSniper();
    }

    void FireSniper()
    {
        Transform newTargetPos = FindMuchHPEnemy();

        if (isSkillActive) skillTargetList.Add(newTargetPos);

        if (newTargetPos == null) return;
        
        Vector2 direction = (newTargetPos.position - firePoint.position).normalized;

        // 시각적으로 보일 투사체
        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        Instantiate(sniperActiveShot, transform.position, Quaternion.identity);

        if (_isSkillReady)
        {
            proj.GetComponent<SniperAttack>().SetInit(direction, skillProjectileSpeed, skillSize);

            SoundManager.Instance.PlaySFX("SniperSkillAttack");
        }
        else
        {
            proj.GetComponent<SniperAttack>().SetInit(direction, projectileSpeed, nomalAttackSize);

            SoundManager.Instance.PlaySFX("SniperAttack");
        }

        // 캐릭터 스프라이트 방향 반전
        if (direction.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        // Raycast로 적 관통 처리
        float maxDistance = 100f; // 원하는 사거리 설정
        RaycastHit2D[] hits = Physics2D.RaycastAll(firePoint.position, direction, maxDistance, LayerMask.GetMask("Enemy")); // "Enemy" 레이어는 적에게만 설정되어야 함

        float totalAttackDamage = TotalAttackDamage();
        float totalSkillDamage = TotalSkillDamage();

        foreach (var hit in hits)
        {
            EnemyHP enemy = hit.collider.GetComponent<EnemyHP>();
            if (enemy != null)
            {
                if (_isSkillReady)
                {
                    enemy.TakeDamage((int)totalSkillDamage, ECharacterType.Sniper);
                }
                else
                {
                    enemy.TakeDamage((int)totalAttackDamage, ECharacterType.Sniper);
                }
                totalAttackDamage *= 0.9f; // 10% 감소

                // 넉백
                Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;

                Enemy enemyComponenet = enemy.GetComponent<Enemy>();
                if (enemyComponenet != null)
                {
                    enemyComponenet.ApplyKnockback(knockbackDirection, knockbackPower * (knockbackPowerUpNum / 100));
                }

                print("현재 데미지 : " + totalAttackDamage);
            }
        }


    }


    protected Transform FindMuchHPEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, enemyDetectRadius, LayerMask.GetMask("Enemy"));

        EnemyHP highestHPNotInList = null;
        EnemyHP highestHPInList = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyHP enemy = hit.GetComponent<EnemyHP>();
                if (enemy == null) continue;

                // 카메라 안에 있는지 체크
                Vector3 viewportPos = Camera.main.WorldToViewportPoint(enemy.transform.position);
                bool isInCamera = viewportPos.z > 0 && viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1;
                if (!isInCamera) continue;

                if (!skillTargetList.Contains(enemy.transform))
                {
                    if (highestHPNotInList == null || enemy.enemyHP > highestHPNotInList.enemyHP)
                    {
                        highestHPNotInList = enemy;
                    }
                }
                else
                {
                    if (highestHPInList == null || enemy.enemyHP > highestHPInList.enemyHP)
                    {
                        highestHPInList = enemy;
                    }
                }
            }
        }

        // 제외 대상에 없는 애가 있으면 그 중 최고 체력 반환
        if (highestHPNotInList != null) return highestHPNotInList.transform;

        // 다 제외 대상이면 그 중 최고 체력 반환
        return highestHPInList != null ? highestHPInList.transform : null;
    }

    public override void EndFieldAct() // 필드전투가 종료될 때 실행
    {
        base.EndFieldAct();

        if (_isSkillReady) _isSkillReady = false;

        if (dim != null)
        {
            dim.color = new Color(dim.color.r, dim.color.g, dim.color.b, dimOriginAlpha);
            dim = null;
        }
    }
}