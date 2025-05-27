using System.Collections;
using UnityEngine;

public class Ninja : Character
{
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<Kunai>().SetDirection(direction);
    }

    protected override IEnumerator ActivateUltimate()
    {
        if (!isGround) yield break;

        isGround = false;
        transform.SetParent(null);
        RiderManager.Instance.RiderCountDown();

        isUltimateActive = true;
        currentMP = 0;
        mpImage.fillAmount = 0;

        // 닌자 궁극기: 점프 후 3연속 공격 (FireBurstProjectiles는 안 씀)
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        for (int i = 0; i < burstCount; i++)
        {
            yield return new WaitForSeconds(burstFireDelay);
            // 궁극기 사용 중 특수 연출을 넣고 싶다면 여기에 추가
            yield return new WaitForSeconds(burstInterval);
        }

        isUltimateActive = false;
    }

    protected override void FireBurstProjectiles()
    {
        // 안 써도 되지만 필요하면 360도 수리검
        int projectileCount = 8;
        float angleStep = 360f / projectileCount;
        Vector3 startPos = transform.position;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject proj = Instantiate(burstProjectile, startPos, rotation);
            proj.GetComponent<Kunai>().SetDirection(rotation * Vector2.right);
        }
    }
}
