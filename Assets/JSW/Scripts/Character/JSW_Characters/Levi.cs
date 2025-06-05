using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levi : Character
{
    [Header("������ ����")]
    public float dobleAttackCoolTime = 0.05f;
    public float NormalAttackProjectileDuration = 2f;

    [Header("��ų")]
    public bool isSkillLanding;
    public int skillDamage;
    public float skillTargetCount;
    public float skillInterval = 0.3f;
    public float skillDashSpeed;
    public GameObject trail;

    [Header("��ȭ")]
    public bool isAttackWhileFalling;
    public bool isGainPowerFromSkillDamage;
    public int GainPowerFromSkillDamageCount;
    public float GainPowerFromSkillDamageDuration;
    public bool isManaOnLandingBasedOnTimeAway;
    public float ManaOnLandingBasedOnTime;
    public bool isAttackSpeedBoostAfterQuickReboard;
    public bool isMoreSkillDamageWithJumpPower;
    public int upgradeNum;

    [Header("����Ʈ")]
    public GameObject skillDashStartEffect;
    public GameObject skillActiveEffect;


    // �Ϲ� ����: ���� �ι� ������ ���� �߻�
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        // ���⿡ ���� ĳ���� ��������Ʈ �¿� ����
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<LeviAttack>().SetInit(direction, attackDamage, projectileSpeed, NormalAttackProjectileDuration);

        SoundManager.Instance.PlaySFX("LeviAttack");
    }

    protected override void Update()
    {
        if (isManaOnLandingBasedOnTimeAway && !isGround) ManaOnLandingBasedOnTime += Time.deltaTime;
        base.Update();
    }

    protected override IEnumerator NormalAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(normalFireInterval);
            if (!isGround) continue;

            Transform target = FindNearestEnemy();
            if (target != null)
            {
                Vector3 targetpos = target.position;
                animator.Play("ATTACK", -1, 0f);
                FireNormalProjectile(targetpos);
                yield return new WaitForSeconds(dobleAttackCoolTime);
                FireNormalProjectile(targetpos);
            }
        }
    }

    // ��ų : ĳ���� ���� ���� �ָ��ִ� ������ �����Ͽ� �������� ��
    protected override IEnumerator FireSkill()
    {
        trail.GetComponent<TrailRenderer>().enabled = false;
        trail.transform.SetParent(this.transform);
        trail.transform.position = this.transform.position;
        //trail.SetActive(true);
        trail.GetComponent<TrailRenderer>().enabled = true;

        animator.Play("SKILL", -1, 0f);
        
        // �ٶ󺸴� ���⿡ ���� ����Ʈ �¿� ����
        if (transform.localScale.x < 0) // ����
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

        for (int i = 0; i < skillTargetCount; i++)
        {
            target = FindFarestEnemyExcluding(hitEnemies);
            if (target == null) break;

            hitEnemies.Add(target);

            // ����
            yield return StartCoroutine(DashToTarget(target));

            // �߰��� �׾��� ���
            if (target == null) continue;

            // ������ �ֱ�
            EnemyHP enemyHP = target.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                if (isMoreSkillDamageWithJumpPower) enemyHP.TakeDamage((int)(attackDamage + skillDamage + jumpForce));
                else enemyHP.TakeDamage(attackDamage + skillDamage);

                Instantiate(skillActiveEffect,enemyHP.transform.position,Quaternion.identity);
            }

            yield return new WaitForSeconds(skillInterval); // �ణ�� ������
        }

        transform.up = Vector3.up;

        rb.linearVelocity = new Vector2(-10, jumpForce);
        //trail.SetActive(false);
        trail.transform.SetParent(null);


        animator.Play("SKILLEND", -1, 0f);

        yield return new WaitForSeconds(0.5f);

        if (isAttackSpeedBoostAfterQuickReboard) StartCoroutine(AttackSpeedBoostAfterQuickReboard());
        if (isGainPowerFromSkillDamage) StartCoroutine(GainPowerFromSkillDamageCountUpGrade());

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

            // ī�޶� ����Ʈ �ȿ� �ִ��� Ȯ��
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(enemyTransform.position);

            bool isVisible = viewportPos.z > 0 &&
                             viewportPos.x >= 0 && viewportPos.x <= 1 &&
                             viewportPos.y >= 0 && viewportPos.y <= 1;

            if (!isVisible) continue;

            // ����� �� ���
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
            rb.MovePosition(move); // ���� ����


            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dashSpeed * Time.fixedDeltaTime, LayerMask.GetMask("Wall")); // �� ���̾� Ȯ��

            if (hit.collider != null)
            {
                break; // �뽬 ����
            }

            if (dir.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (dir.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            yield return new WaitForFixedUpdate(); // FixedUpdate ����
        }

        rb.linearVelocity = Vector2.zero;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            if (isManaOnLandingBasedOnTimeAway) currentMP += Mathf.Clamp(ManaOnLandingBasedOnTime * 3, 0, 50); ManaOnLandingBasedOnTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGround && collision.tag == "Enemy" && (isSkillActive || isAttackWhileFalling))            // ��ų�� �����ְų�, ������ ��Ÿ ��ȭ�Ǿ��� ��
        {
            if (isGainPowerFromSkillDamage && isSkillActive) GainPowerFromSkillDamageCount += 1;
            collision.GetComponent<EnemyHP>().TakeDamage(attackDamage);
        }
    }

    IEnumerator GainPowerFromSkillDamageCountUpGrade()
    {
        attackDamage += GainPowerFromSkillDamageCount;
        yield return new WaitForSeconds(GainPowerFromSkillDamageDuration);
        attackDamage -= GainPowerFromSkillDamageCount;
        GainPowerFromSkillDamageCount = 0;
    }

    IEnumerator AttackSpeedBoostAfterQuickReboard()
    {
        float timer = 0;
        float minusNormalFireInterval = 0.4f;

        while (true)
        {

            if (isGround)
            {
                normalFireInterval -= minusNormalFireInterval;

                yield return new WaitForSeconds(2f);

                normalFireInterval += minusNormalFireInterval;

                break;
            }

            if (timer > 3) break;
            timer += Time.deltaTime;

            yield return null;
        }
    }
}
