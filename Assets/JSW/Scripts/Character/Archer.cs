using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Archer : Character
{
    public bool stage3; // 이건 Archer 고유 옵션이니 유지

    // 일반 공격 오버라이드
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<Arrow>().SetDirection(direction);

        if (stage3)
        {
            GameObject proj2 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            proj2.GetComponent<Arrow>().SetDirection(direction + Vector2.up);

            GameObject proj3 = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            proj3.GetComponent<Arrow>().SetDirection(direction + Vector2.down);
        }
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

    // 궁극기 연사 오버라이드 (필요 시)
    protected override void FireBurstProjectiles()
    {
        int projectileCount = 10;
        float angleStep = 360f / projectileCount;
        Vector3 startPos = transform.position;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject proj = Instantiate(burstProjectile, startPos, rotation);
            proj.GetComponent<Arrow>().SetDirection(rotation * Vector2.right);
        }
    }
}