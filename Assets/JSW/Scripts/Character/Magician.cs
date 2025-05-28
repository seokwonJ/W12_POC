using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Magician : Character
{
    // �Ϲ� ���� ����
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<MagicBall>().SetDirection(direction);
    }

    protected override IEnumerator ActivateUltimate()
    {
        if (isGround)
        {
            isGround = false;
            transform.SetParent(null); // ���� ������ ������ ��� �и�
            RiderManager.Instance.RiderCountDown();

            isUltimateActive = true;
            currentMP = 0;
            mpImage.fillAmount = currentMP / maxMP;

            // ����
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            // �ñر� 3����
            for (int i = 0; i < burstCount; i++)
            {
                yield return new WaitForSeconds(burstFireDelay);
                FireBurstProjectiles();
                yield return new WaitForSeconds(burstInterval);
            }

            isUltimateActive = false;
        }
    }

    // �ñر� �߻� ����
    protected override void FireBurstProjectiles()
    {
        Transform target = FindNearestEnemy();
        if (target != null)
        {
            GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

            MagicBall mb = proj.GetComponent<MagicBall>();
            mb.SetDirection((target.position - firePoint.position).normalized);
            mb.speed = 5;
            proj.transform.localScale *= 3;
        }
    }
}