using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Magician : Character
{
    [Header("궁극기")]
    public GameObject burstProjectile;
    public int burstCount = 3;
    public float burstInterval = 0.3f;
    public float burstFireDelay = 0.1f;

    // 일반 공격: 가까운 적에게 관통 공격
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<MagicBall>().SetInit(direction,abilityPower,projectileSpeed);
    }

    // 스킬: 느리고 커다란 관통 공격
    protected override IEnumerator FireSkill()
    {
        for (int i = 0; i < burstCount; i++)
        {
            yield return new WaitForSeconds(burstFireDelay);
            FireSkillProjectiles();
            yield return new WaitForSeconds(burstInterval);
        }
    }

    // 궁극기 발사 구현
    protected override void FireSkillProjectiles()
    {
        Transform target = FindNearestEnemy();
        if (target != null)
        {
            GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

            MagicBall mb = proj.GetComponent<MagicBall>();
            mb.SetInit((target.position - firePoint.position).normalized, abilityPower, projectileSpeed);
            mb.speed = 5;
            proj.transform.localScale *= 3;
        }
    }
}