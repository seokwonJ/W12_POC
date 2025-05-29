using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Worrior : Character
{
    [Header("궁극기")]
    public GameObject burstProjectile;
    public int burstCount = 3;
    public float burstInterval = 0.3f;
    public float burstFireDelay = 0.1f;

    public int upgradeNum;

    // 일반 공격: 직진형 투사체 발사
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<SwordAttack>().SetInit(direction, attackDamage, projectileSpeed); // 이 메서드가 없다면 그냥 방향 저장해서 쓰면 됨
    }

    // 스킬: 커다란 직진형 투사체 3발 연속 발사
    protected override IEnumerator FireSkill()
    {
        // 궁극기 3연사
        for (int i = 0; i < burstCount; i++)
        {
            yield return new WaitForSeconds(burstFireDelay);
            FireSkillProjectiles();
            yield return new WaitForSeconds(burstInterval);
        }
    }

    protected override void FireSkillProjectiles()
    {
        GameObject proj = Instantiate(burstProjectile, firePoint.position, Quaternion.identity);
        var sword = proj.GetComponent<SwordAttack>();
        if (sword != null)
        {
            sword.speed = 20;
        }
        proj.transform.localScale *= 3; // 커다란 검 휘두르기 느낌
    }
}