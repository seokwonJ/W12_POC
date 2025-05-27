using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Worrior : Character
{
    // 일반 공격: 투사체 발사 없이 연출만 하거나, 범위 타격 등 가능
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<SwordAttack>().SetDirection(direction); // 이 메서드가 없다면 그냥 방향 저장해서 쓰면 됨
    }
    protected override IEnumerator ActivateUltimate()
    {
        if (isGround)
        {
            isGround = false;
            transform.SetParent(null); // 점프 등으로 떨어질 경우 분리
            RiderManager.Instance.RiderCountDown();

            isUltimateActive = true;
            currentMP = 0;
            mpImage.fillAmount = currentMP / maxMP;

            // 점프
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            // 궁극기 3연사
            for (int i = 0; i < burstCount; i++)
            {
                yield return new WaitForSeconds(burstFireDelay);
                FireBurstProjectiles();
                yield return new WaitForSeconds(burstInterval);
            }

            isUltimateActive = false;
        }
    }

    // 궁극기: 강화된 범위 공격
    protected override void FireBurstProjectiles()
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