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

        // ���� �ñر�: ���� �� 3���� ���� (FireBurstProjectiles�� �� ��)
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        for (int i = 0; i < burstCount; i++)
        {
            yield return new WaitForSeconds(burstFireDelay);
            // �ñر� ��� �� Ư�� ������ �ְ� �ʹٸ� ���⿡ �߰�
            yield return new WaitForSeconds(burstInterval);
        }

        isUltimateActive = false;
    }

    protected override void FireBurstProjectiles()
    {
        // �� �ᵵ ������ �ʿ��ϸ� 360�� ������
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
