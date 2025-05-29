using System.Collections;
using UnityEngine;

public class Ninja : Character
{
    public float burstInterval = 0.3f;
    [Header("닌자 스킬")]
    public bool isUltimateLanding;
    public float UltimagePower;
    public float UltimagePowerTime;

    // 일반 공격: 원거리 투사체 표창 가까운 적에게 던지기
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<Kunai>().SetDirection(direction);
    }

    // 스킬 : 점프 후 착지시 3초간 공격력 강화
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(burstInterval);
    }

    // 착지했을 경우
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        ContactPoint2D contact = collision.contacts[0];
        if (Vector2.Dot(contact.normal, Vector2.up) < 0.9f) return;

        isGround = true;
        if (isUltimateActive) return;
        if (isUltimateLanding)
        {
            isUltimateLanding = false;
            StartCoroutine(PowerUp(UltimagePower));
        }
        RiderManager.Instance.RiderCountUp();
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
    }

    IEnumerator PowerUp(float power)
    {
        // 공격력 += power
        Debug.Log("공격력 업!");
        yield return new WaitForSeconds(UltimagePowerTime);
        Debug.Log("공격력 돌아옴");
    }
}
