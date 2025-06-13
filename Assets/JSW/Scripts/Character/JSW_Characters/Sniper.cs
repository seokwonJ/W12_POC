using System.Collections;
using UnityEngine;

public class Sniper : Character
{
    [Header("��ų")]
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

    [Header("���ݼ� ����")]
    public int AttackMaxCount = 3;
    private int _AttackCurrentCount;
    public float realoadTime;

    [Header("��ȭ")]
    public float nomalAttackSize;

    public int upgradeNum;

    public GameObject player;

    [Header("����Ʈ")]
    public GameObject skillActiveEffect;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMove>().gameObject;
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
            if (!isGround) continue;

            Transform target = FindMuchHPEnemy();
            if (target != null)
            {
                animator.Play("ATTACK", -1, 0f);
                _AttackCurrentCount -= 1;
                FireNormalProjectile(target.position);
            }

            if (_AttackCurrentCount <= 0)
            {
                Debug.Log("�������� ���� ��");
                yield return new WaitForSeconds(realoadTime);
                _AttackCurrentCount = AttackMaxCount;
            }
        }
    }

    // �Ϲ� ����: ����� ������ ���� ����
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        FireSniper();
    }

    // ��ų: ������ Ŀ�ٶ� ���� ���� 3�� �߻�
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillInterval);

        _isSkillReady = true;
        _beforeSkillVelocity = rb.linearVelocity;

        yield return new WaitForSeconds(skillSlowDelay);


        for (int i = 0; i < skillCount; i++)
        {
            animator.Play("SKILL", -1, 0f);
            FireSkillProjectiles();
            //Instantiate(skillActiveEffect, transform.position, Quaternion.identity, transform);
            SoundManager.Instance.PlaySFX("MagicianSkill");
            yield return new WaitForSeconds(skillFireDelay);
        }

        _isSkillReady = false;
    }

    // ��ų �߻� ����
    protected override void FireSkillProjectiles()
    {
        FireSniper();
    }

    void FireSniper()
    {
        Transform newTargetPos = FindMuchHPEnemy();

        if (newTargetPos == null) return;
        
        Vector2 direction = (newTargetPos.position - firePoint.position).normalized;

        // �ð������� ���� ����ü
        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        proj.GetComponent<SniperAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackSize);

        // ĳ���� ��������Ʈ ���� ����
        if (direction.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        // Raycast�� �� ���� ó��
        float maxDistance = 100f; // ���ϴ� ��Ÿ� ����
        RaycastHit2D[] hits = Physics2D.RaycastAll(firePoint.position, direction, maxDistance, LayerMask.GetMask("Enemy")); // "Enemy" ���̾�� �����Ը� �����Ǿ�� ��

        float currentDamage = attackDamage;

        foreach (var hit in hits)
        {
            EnemyHP enemy = hit.collider.GetComponent<EnemyHP>();
            if (enemy != null)
            {
                enemy.TakeDamage(Mathf.RoundToInt(currentDamage), ECharacterType.Sniper);
                currentDamage *= 0.9f; // 10% ����
                print("���� ������ : " + currentDamage);
            }
        }

        SoundManager.Instance.PlaySFX("MagicianAttack");
    }


    protected Transform FindMuchHPEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, enemyDetectRadius);
        float maxHP = 0;
        Transform nearest = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {   
                float hp = hit.GetComponent<EnemyHP>().enemyHP;
                if (hp > maxHP)
                {
                    maxHP = hp;
                    nearest = hit.transform;
                }
            }
        }
        return nearest;
    }

    public override void EndFieldAct() // �ʵ������� ����� �� ����
    {
        base.EndFieldAct();

        if (_isSkillReady) _isSkillReady = false;
    }
}