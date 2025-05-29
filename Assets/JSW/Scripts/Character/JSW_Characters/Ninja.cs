using System.Collections;
using UnityEngine;

public class Ninja : Character
{
    public float skillInterval = 0.3f;
    [Header("���� ��ų")]
    public bool isSkillLanding;
    public float skillPower;
    public float skillPowerTime;

    public int upgradeNum;

    // �Ϲ� ����: ���Ÿ� ����ü ǥâ ����� ������ ������
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<Kunai>().SetInit(direction, attackDamage, projectileSpeed);
    }

    // ��ų : ���� �� ������ 3�ʰ� ���ݷ� ��ȭ
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillInterval);
    }

    // �������� ���
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        ContactPoint2D contact = collision.contacts[0];
        if (Vector2.Dot(contact.normal, Vector2.up) < 0.9f) return;

        if (isUltimateActive) return;
        isGround = true;
        if (isSkillLanding)
        {
            isSkillLanding = false;
            StartCoroutine(PowerUp(skillPower));
        }
        RiderManager.Instance.RiderCountUp();
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
    }

    IEnumerator PowerUp(float power)
    {
        // ���ݷ� += power
        Debug.Log("���ݷ� ��!");
        yield return new WaitForSeconds(skillPowerTime);
        Debug.Log("���ݷ� ���ƿ�");
    }
}
