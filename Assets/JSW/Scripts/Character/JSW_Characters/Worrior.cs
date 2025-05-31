using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Worrior : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;

    [Header("강화")]
    public float nomalAttackSize;
    public float nomalAttackLifetime;
    public bool isfallingCanAttack;
    public bool isShieldFlyer;

    public int upgradeNum;

    private PlayerStatus _playerStatus;


    protected override void Start()
    {
        base.Start();
        _playerStatus = FindAnyObjectByType<PlayerStatus>();
    }

    // 일반 공격: 직진형 투사체 발사
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<SwordAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackLifetime, nomalAttackSize); // 이 메서드가 없다면 그냥 방향 저장해서 쓰면 됨
    }

    // 스킬: 커다란 직진형 투사체 3발 연속 발사
    protected override IEnumerator FireSkill()
    {

        if (isShieldFlyer) _playerStatus.defensePower -= 5;

        // 궁극기 3연사
        for (int i = 0; i < skillCount; i++)
        {
            yield return new WaitForSeconds(skillFireDelay);
            FireSkillProjectiles();
            yield return new WaitForSeconds(skillInterval);
        }
    }

    protected override void FireSkillProjectiles()
    {
        GameObject proj = Instantiate(skillProjectile, firePoint.position, Quaternion.identity);
        var sword = proj.GetComponent<SwordAttack>();
        if (sword != null)
        {
            sword.speed = 20;
        }
        proj.transform.localScale *= 3; // 커다란 검 휘두르기 느낌
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        ContactPoint2D contact = collision.contacts[0];
        if (Vector2.Dot(contact.normal, Vector2.up) < 0.9f) return;

        if (isUltimateActive || isGround) return;
        isGround = true;

        if (isShieldFlyer) _playerStatus.defensePower += 5;

        Managers.Rider.RiderCountUp();
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isfallingCanAttack && collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyHP>().TakeDamage(attackDamage);
        }
    }
}