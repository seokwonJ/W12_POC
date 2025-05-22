using System.Collections;
using UnityEngine;

public class Magician : MonoBehaviour
{
    [Header("MP 시스템")]
    public int maxMP = 100;
    private int currentMP = 0;

    [Header("일반 공격")]
    public GameObject normalProjectile;
    public Transform firePoint;
    public float normalFireInterval = 1f;
    public int mpPerShot = 10;
    public float enemyDetectRadius = 10f;

    [Header("점프 & 궁극기")]
    public float jumpForce = 10f;
    public GameObject burstProjectile;
    public int burstCount = 3;
    public float burstInterval = 0.3f;
    public float burstFireDelay = 0.1f;

    private Rigidbody2D rb;
    private bool isUltimateActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(NormalAttackRoutine());
    }

    IEnumerator NormalAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(normalFireInterval);

            // 가장 가까운 적 찾기
            Transform target = FindNearestEnemy();
            print("target " + target);
            if (target != null)
            {
                Vector2 direction = (target.position - firePoint.position).normalized;

                GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
                proj.GetComponent<MagicBall>().SetDirection(direction);
            }

            // MP 증가
            currentMP += mpPerShot;
            currentMP = Mathf.Min(currentMP, maxMP);

            // 궁극기 발동 조건 확인
            if (currentMP >= maxMP && !isUltimateActive)
            {
                StartCoroutine(ActivateUltimate());
            }
        }
    }

    IEnumerator ActivateUltimate()
    {
        isUltimateActive = true;
        currentMP = 0;

        // 점프
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        // 궁극기 3연사
        for (int i = 0; i < burstCount; i++)
        {
            yield return new WaitForSeconds(burstFireDelay);
            //FireBurstProjectiles();
            yield return new WaitForSeconds(burstInterval);
        }

        isUltimateActive = false;
    }

    void FireBurstProjectiles()
    {
        int projectileCount = 10;
        float angleStep = 360f / projectileCount;
        Vector3 startPos = transform.position;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject proj = Instantiate(burstProjectile, startPos, rotation);
            proj.GetComponent<MagicBall>().SetDirection(rotation * Vector2.right);
        }
    }

    Transform FindNearestEnemy()
    {
        print("ddd");


        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, enemyDetectRadius);
        float closestDist = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestEnemy = hit.transform;
                }
            }
        }

        return closestEnemy;
    }

    void OnDrawGizmosSelected()
    {
        // 시야 범위 시각화
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyDetectRadius);
    }
}
