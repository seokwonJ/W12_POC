using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levi : Character
{
    [Header("리바이 공격")]
    public int attackNum = 2;
    public float dobleAttackCoolTime = 0.05f;
    public float NormalAttackProjectileDuration = 2f;
    public float attackPerDamageMinus = 5;

    [Header("스킬")]
    public int skillTargetCount;
    private int _nowskillTargetCount;
    public float skillInterval = 0.3f;
    public float skillDashSpeed;
    public GameObject trail;

    [Header("강화")]
    public bool isSkillEndPlusSkillCountUp;
    public bool isAttackWhileSkillUpgrade;
    public int maximumSkillEndPlusSkillCountUp = 10;
    public int upgradeNum;

    [Header("이펙트")]
    public GameObject skillDashStartEffect;
    public GameObject skillActiveEffect;


    // 일반 공격: 연속 두번 관통형 공격 발사
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        // 방향에 따라 캐릭터 스프라이트 좌우 반전
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float totalAttackDamage = TotalAttackDamage();
        bool isCritical = IsCriticalHit();
        if (isCritical) totalAttackDamage *= ((criticalDamage * criticalDamageUpNum / 100) / 100);

        proj.GetComponent<LeviAttack>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), isCritical, NormalAttackProjectileDuration, attackPerDamageMinus);

        SoundManager.Instance.PlaySFX("LeviAttack");
    }

    protected override IEnumerator NormalAttackRoutine()
    {

        _nowskillTargetCount = skillTargetCount;
        while (true)
        {
            yield return new WaitForSeconds(normalFireInterval);
            if (!isGround) continue;

            Transform target = FindNearestEnemy();
            if (target != null)
            {
                Vector3 targetpos = target.position;
                animator.Play("ATTACK", -1, 0f);

                for (int i = 0; i < attackNum; i++)
                {
                    FireNormalProjectile(targetpos);
                    yield return new WaitForSeconds(dobleAttackCoolTime);
                }
            }
        }
    }

    // 스킬 : 캐릭터 기준 가장 멀리있는 적에게 돌진하여 데미지를 줌
    protected override IEnumerator FireSkill()
    {
        trail.GetComponent<TrailRenderer>().enabled = false;
        trail.transform.SetParent(this.transform);
        trail.transform.position = this.transform.position;
        //trail.SetActive(true);
        trail.GetComponent<TrailRenderer>().enabled = true;

        animator.Play("SKILL", -1, 0f);
        SoundManager.Instance.PlaySFX("LeviDashStart");

        // 바라보는 방향에 따라 이펙트 좌우 반전
        if (transform.localScale.x < 0) // 왼쪽
        {
            GameObject DashEffect = Instantiate(skillDashStartEffect, transform.position + Vector3.right * 2 + Vector3.up * 0.3f, Quaternion.identity, playerTransform);
            Vector3 scale = DashEffect.transform.localScale;
            scale.x *= -1;
            DashEffect.transform.localScale = scale;
        }
        else
        {
            GameObject DashEffect = Instantiate(skillDashStartEffect, transform.position - Vector3.right * 2 + Vector3.up * 0.3f, Quaternion.identity, playerTransform);
        }


        List<Transform> hitEnemies = new List<Transform>();

        gameObject.layer = LayerMask.NameToLayer("DoSkill");

        Transform target;

        for (int i = 0; i < _nowskillTargetCount; i++)
        {
            target = FindFarestEnemyExcluding(hitEnemies);
            if (target == null) break;

            hitEnemies.Add(target);

            // 돌진
            yield return StartCoroutine(DashToTarget(target));

            // 중간에 죽었을 경우
            if (target == null) continue;

            // 데미지 주기
            EnemyHP enemyHP = target.GetComponent<EnemyHP>();
            SoundManager.Instance.PlaySFX("LeviSkillAttack");

            float totalSkillDamage = TotalSkillDamage();

            if (enemyHP != null)
            {
                enemyHP.TakeDamage((int)(totalSkillDamage), ECharacterType.Levi);

                Instantiate(skillActiveEffect, enemyHP.transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(skillInterval); // 약간의 딜레이
        }

        transform.up = Vector3.up;

        rb.linearVelocity = new Vector2(-10, jumpForce);
        //trail.SetActive(false);
        trail.transform.SetParent(null);


        animator.Play("SKILLEND", -1, 0f);

        yield return new WaitForSeconds(0.5f);

        //animator.SetBool("5_FALL", true);
        gameObject.layer = LayerMask.NameToLayer("Character");
    }

    private Transform FindFarestEnemyExcluding(List<Transform> excluded)
    {
        Transform farest = null;
        float maxDist = 0;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Transform enemyTransform = obj.transform;

            if (excluded.Contains(enemyTransform)) continue;

            // 카메라 뷰포트 안에 있는지 확인
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(enemyTransform.position);

            bool isVisible = viewportPos.z > 0 &&
                             viewportPos.x >= 0 && viewportPos.x <= 1 &&
                             viewportPos.y >= 0 && viewportPos.y <= 1;

            if (!isVisible) continue;

            // 가까운 적 계산
            float dist = Vector2.Distance(transform.position, enemyTransform.position);
            if (dist > maxDist)
            {
                maxDist = dist;
                farest = enemyTransform;
            }
        }

        return farest;
    }

    private IEnumerator DashToTarget(Transform target)
    {
        float dashSpeed = skillDashSpeed;
        float reachDist = 1.5f;

        while (target != null && Vector2.Distance(transform.position, target.position) > reachDist)
        {
            if (target == null || target.gameObject == null)
            {
                break;
            }



            Vector2 dir = (target.position - transform.position).normalized;
            Vector2 move = (Vector2)transform.position + dir * dashSpeed * Time.fixedDeltaTime;
            rb.MovePosition(move); // 감속 없음


            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dashSpeed * Time.fixedDeltaTime, LayerMask.GetMask("Wall")); // 벽 레이어 확인

            if (hit.collider != null)
            {
                break; // 대쉬 종료
            }

            if (dir.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (dir.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            yield return new WaitForFixedUpdate(); // FixedUpdate 기준
        }

        rb.linearVelocity = Vector2.zero;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // 부모 로직 먼저 실행

        // 조건 만족 안 하면 아무것도 안 하고 리턴 (부모에서 이미 isGround 처리됨)
        if (!isGround) return;

        if (isSkillEndPlusSkillCountUp && _nowskillTargetCount < maximumSkillEndPlusSkillCountUp)
        {
            _nowskillTargetCount += 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        float totalAttackDamage = TotalAttackDamage();

        if (!isGround && collision.tag == "Enemy" && (isSkillActive && isAttackWhileSkillUpgrade))            // 스킬을 쓰고 강화되었을 때
        {
            collision.GetComponent<EnemyHP>().TakeDamage((int)totalAttackDamage, ECharacterType.Levi);
        }
    }



    public override void EndFieldAct() // 필드전투가 종료될 때 실행
    {
        base.EndFieldAct();
        trail.GetComponent<TrailRenderer>().enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Character");
    }
}
