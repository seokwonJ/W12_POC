using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character
{
    [Header("��ų")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public int skillProjectileCount = 10;
    public Dictionary<GameObject, int> hitEnemies;

    [Header("��ȭ")]
    public int upgradeNum;
    public bool isUpgradeTwoShot;
    public bool isUpgradeDieInstantly;
    public float dieInstantlyProbability;

    [Header("����Ʈ")]
    public GameObject skillActiveEffect;

    // �Ϲ� ���� : ȭ�� �߻� 
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        if (targetPos == null) return;


        Vector2 direction = (targetPos - firePoint.position).normalized;

        // ���⿡ ���� ĳ���� ��������Ʈ �¿� ����
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    
        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float totalAttackDamage = TotalAttackDamage();

        bool instantlyDie = false;
        if (isUpgradeDieInstantly && Random.value < dieInstantlyProbability / 100) instantlyDie = true;
        else instantlyDie = false;

        proj.GetComponent<Arrow>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), projectileSize * (projectileSizeUpNum / 100), this, false, instantlyDie);

        if (isUpgradeTwoShot)
        {
            if (isUpgradeDieInstantly && Random.value < dieInstantlyProbability / 100) instantlyDie = true;
            else instantlyDie = false;

            GameObject proj2 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            proj2.GetComponent<Arrow>().SetInit(Quaternion.Euler(0, 0, 5) * direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), projectileSize * (projectileSizeUpNum / 100), this, false, instantlyDie);
        }

        SoundManager.Instance.PlaySFX("ArcherAttack");

        Destroy(proj, 3);
    }

    // ��ų : ��濡 ȭ�� ������ �߻�
    protected override IEnumerator FireSkill()
    {
        for (int i = 0; i < skillCount; i++)
        {
            yield return new WaitForSeconds(skillFireDelay);
            FireSkillProjectiles(i);
            animator.Play("SKILL", -1, 0f);
            SoundManager.Instance.PlaySFX("ArcherSkill");
            Instantiate(skillActiveEffect, transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(skillInterval / skillCount);
        }
    }

    // �ñر� ���� �������̵� (�ʿ� ��)
    protected void FireSkillProjectiles(int skillCount)
    {
        float angleStep = 360f / skillProjectileCount;
        Vector3 startPos = transform.position;

        float totalSkillDamage = TotalSkillDamage();

        // ��ų ������ ���� �͵�
        hitEnemies = new Dictionary<GameObject, int>();

        for (int i = 0; i < skillProjectileCount; i++)
        {
            float angle = i * angleStep + 10 * skillCount;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject proj = Instantiate(skillProjectile, startPos, rotation);
            proj.GetComponent<Arrow>().SetInit(rotation * Vector2.right, totalSkillDamage, projectileSpeed * (projectileSpeedUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), projectileSize * (projectileSizeUpNum / 100), this, true,false);
        }
    }
}