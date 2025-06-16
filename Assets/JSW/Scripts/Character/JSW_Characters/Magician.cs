using System.Collections;
using UnityEngine;

public class Magician : Character
{
    [Header("��ų")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillSize = 1f;
    public float skillProjectileSpeed = 15;

    [Header("��ȭ")]
    public bool isAddAttackDamage;
    public bool isnomalAttackSizePerMana;
    public int upgradeNum;
    public GameObject player;

    [Header("����Ʈ")]
    public GameObject skillActiveEffect;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMove>().gameObject;
    }

    // �Ϲ� ����: ����� ������ ���� ����
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        // ���⿡ ���� ĳ���� ��������Ʈ �¿� ����
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float totalAttackDamage = TotalAttackDamage();
        

        if (isnomalAttackSizePerMana) projectileSize *= currentMP / 50;

        if (isAddAttackDamage) proj.GetComponent<MagicBall>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100));
        else
        {
            proj.GetComponent<MagicBall>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100));
        }

        SoundManager.Instance.PlaySFX("MagicianAttack");
    }

    // ��ų: ������ Ŀ�ٶ� ���� ���� 3�� �߻�
    protected override IEnumerator FireSkill()
    {
        for (int i = 0; i < skillCount; i++)
        {
            yield return new WaitForSeconds(skillFireDelay);
            animator.Play("SKILL", -1, 0f);
            FireSkillProjectiles();
            Instantiate(skillActiveEffect, transform.position, Quaternion.identity, transform);
            SoundManager.Instance.PlaySFX("MagicianSkill");
            yield return new WaitForSeconds(skillInterval);
        }
    }

    // ��ų �߻� ����
    protected override void FireSkillProjectiles()
    {
        Transform target = FindNearestEnemy();

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        MagicBall mb = proj.GetComponent<MagicBall>();

        float totalSkillDamage = TotalSkillDamage();

        if (target != null)
        {
            mb.SetInit((target.position - firePoint.position).normalized, totalSkillDamage, projectileSpeed * (projectileSpeedUpNum / 100), skillSize, knockbackPower * (knockbackPowerUpNum / 100));
        }
        else
        {
            mb.SetInit((Random.insideUnitSphere).normalized, totalSkillDamage, projectileSpeed * (projectileSpeedUpNum / 100), skillSize, knockbackPower * (knockbackPowerUpNum / 100));

        }
    }

}