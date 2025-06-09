using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archer : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public int skillProjectileCount = 10;
    public bool isUpgradeTripleShot; // 이건 Archer 고유 옵션이니 유지
    public Dictionary<GameObject, int> hitEnemies;

    [Header("강화")]
    public float knockbackPower;
    public float arrowSize;
    public int upgradeNum;

    [Header("이펙트")]
    public GameObject skillActive;

    // 일반 공격 : 화살 발사 
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        if (targetPos == null) return;


        Vector2 direction = (targetPos - firePoint.position).normalized;

        // 방향에 따라 캐릭터 스프라이트 좌우 반전
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    
        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<Arrow>().SetInit(direction, attackDamage, projectileSpeed, knockbackPower, arrowSize, this, false);

        if (isUpgradeTripleShot)
        {
            GameObject proj2 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            proj2.GetComponent<Arrow>().SetInit(Quaternion.Euler(0, 0, 10) * direction, attackDamage, projectileSpeed, knockbackPower, arrowSize, this, false);

            GameObject proj3 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            proj3.GetComponent<Arrow>().SetInit(Quaternion.Euler(0, 0, -10) * direction, attackDamage, projectileSpeed, knockbackPower, arrowSize, this, false);
        }

        SoundManager.Instance.PlaySFX("ArcherAttack");

        Destroy(proj, 3);
    }

    // 스킬 : 사방에 화살 여러번 발사
    protected override IEnumerator FireSkill()
    {
        for (int i = 0; i < skillCount; i++)
        {
            yield return new WaitForSeconds(skillFireDelay);
            FireSkillProjectiles();
            animator.Play("SKILL", -1, 0f);
            SoundManager.Instance.PlaySFX("ArcherSkill");
            Instantiate(skillActive, transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(skillInterval);
        }
    }

    // 궁극기 연사 오버라이드 (필요 시)
    protected override void FireSkillProjectiles()
    {
        float angleStep = 360f / skillProjectileCount;
        Vector3 startPos = transform.position;

        // 스킬 데미지 받은 것들
        hitEnemies = new Dictionary<GameObject, int>();

        for (int i = 0; i < skillProjectileCount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject proj = Instantiate(skillProjectile, startPos, rotation);
            proj.GetComponent<Arrow>().SetInit(rotation * Vector2.right, attackDamage, projectileSpeed, knockbackPower, arrowSize, this, true);
        }
    }
}