using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : Character
{
    [Header("��ų")]
    public GameObject skillProjectile;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSpeed = 10f;
    public int skillShotCount = 8;

    [Header("��ȭ")]
    public float nomalAttackSize;
    public bool isBackwardCannonShot;
    public bool isFirstHitDealsBonusDamage;

    public int upgradeNum;

    public GameObject player;

    [Header("����Ʈ")]
    public GameObject skillActiveEffect;


    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMove>().gameObject;
    }

    // �Ϲ� ����: ������ �ѹ߾� �߻� ������ ��� ���� �˹�
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        if (isBackwardCannonShot)
        {
            Vector2 direction2 = -1 * (targetPos + Vector3.up - firePoint.position).normalized;

            GameObject proj2 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

            float totalAttackDamage = TotalAttackDamage();

            proj2.GetComponent<PirateAttack>().SetInit(direction2, totalAttackDamage, projectileSpeed, nomalAttackSize, false,isFirstHitDealsBonusDamage);
        }

        Vector2 direction = (targetPos + Vector3.up - firePoint.position).normalized;

        // ���⿡ ���� ĳ���� ��������Ʈ �¿� ����
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float nownomalAttackSize = nomalAttackSize;
        //proj.GetComponent<PirateAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackSize, isFirstHitDealsBonusDamage);

        Transform enemyTarget = FindNearestEnemy(); // Ÿ�� �����ϴ� �޼��� �ʿ�
        proj.GetComponent<PirateAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackSize, isFirstHitDealsBonusDamage, false, enemyTarget);

        SoundManager.Instance.PlaySFX("PirateAttack");

    }

    // ��ų: ���� �� ���߿� �����˵� ������ �߻�
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillInterval);

        SoundManager.Instance.PlaySFX("PirateSkillActive");
        SoundManager.Instance.PlaySFX("PirateAttack");
        Instantiate(skillActiveEffect, transform.position + Vector3.up, Quaternion.identity);

        yield return StartCoroutine(FireSkillCanon());
    }

    // �ñر� �߻� ����
    IEnumerator FireSkillCanon()
    {
        List<Transform> originalTargets = GetFarthestEnemies(skillShotCount);
        List<Transform> activeTargets = new List<Transform>(originalTargets);

        animator.Play("SKILL", -1, 0f);

        for (int i = 0; i < skillShotCount; i++)
        {
            rb.linearVelocity = Vector3.zero;

            // ����ִ� Ÿ�� �� �ϳ� ����
            activeTargets.RemoveAll(t => t == null || !t.gameObject.activeInHierarchy);

            Transform target = null;
            if (activeTargets.Count > 0)
            {
                target = activeTargets[i % activeTargets.Count]; // ���� ��ȸ
            }

            GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

            float totalSkillDamage = TotalSkillDamage();

            PirateAttack mb = proj.GetComponent<PirateAttack>();

            if (target != null)
            {
                Vector3 dir = target.position - proj.transform.position;
                mb.SetInit(dir.normalized, totalSkillDamage, skillSpeed, nomalAttackSize, isFirstHitDealsBonusDamage, true, target);
            }
            else
            {
                // ��ȿ�� Ÿ���� ������ ���� ���� �߻�
                float randomAngle = Random.Range(0f, 360f);
                Vector2 randomDir = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
                mb.SetInit(randomDir.normalized, totalSkillDamage, skillSpeed, nomalAttackSize, isFirstHitDealsBonusDamage,true, null);
            }

            
        yield return new WaitForSeconds(skillFireDelay);
        }
    }

    private List<Transform> GetFarthestEnemies(int count)
    {
        List<Transform> enemies = new List<Transform>();
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy.transform);
        }

        enemies.Sort((a, b) =>
            Vector2.Distance(firePoint.position, b.position)
            .CompareTo(Vector2.Distance(firePoint.position, a.position))
        );

        return enemies.GetRange(0, Mathf.Min(count, enemies.Count));
    }
}
