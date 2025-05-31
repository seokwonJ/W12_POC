using System.Collections;
using UnityEngine;

public class LightSoldier : Character
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

        GameObject proj = Instantiate(normalProjectile, firePoint.position + Vector3.up, Quaternion.identity);
        GameObject proj2 = Instantiate(normalProjectile, firePoint.position + Vector3.down, Quaternion.identity);
        proj.GetComponent<LightSoldierAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackLifetime, nomalAttackSize); // 이 메서드가 없다면 그냥 방향 저장해서 쓰면 됨
        proj2.GetComponent<LightSoldierAttack>().SetInit(direction, attackDamage, projectileSpeed, nomalAttackLifetime, nomalAttackSize); // 이 메서드가 없다면 그냥 방향 저장해서 쓰면 됨
    }

    // 스킬: 커다란 직진형 투사체 3발 연속 발사
    protected override IEnumerator FireSkill()
    {
        yield return new WaitForSeconds(skillInterval);

        Camera cam = Camera.main;

        // 카메라 뷰포트의 전체 상단 ~ 하단을 월드 기준으로 변환
        Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0, 1, cam.transform.position.z * -1f));
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.transform.position.z * -1f));

        for (int i = 0; i < 14; i++)
        {
            float randomY = Random.Range(bottomLeft.y, topLeft.y); // 실제 월드 Y 범위

            Vector3 worldPos = new Vector3(topLeft.x, randomY, 0); // 왼쪽 가장자리에서 랜덤 y 위치

            GameObject proj = Instantiate(skillProjectile, worldPos, Quaternion.identity);
            var sword = proj.GetComponent<LightSoldierAttack>();
            if (sword != null)
            {
                sword.speed = 20;
                sword.SetInit(Vector3.right, attackDamage, 30, 10, nomalAttackSize);
            }

            proj.transform.localScale *= 3;
            rb.linearVelocity = new Vector2(0, 2.2f);
            yield return new WaitForSeconds(0.2f);
            rb.linearVelocity = Vector3.zero;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isfallingCanAttack && collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyHP>().TakeDamage(attackDamage);
        }
    }
}