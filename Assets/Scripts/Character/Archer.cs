using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Archer : MonoBehaviour
{
    [Header("MP 시스템")]
    public float maxMP = 100;
    private float currentMP = 0;

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
    private FixedJoint2D fixedJoint;
    private bool isUltimateActive = false;

    public float maxFallSpeed = -10f; // 음수로 설정해야 아래로 가는 속도 제한
    private bool isGround = true;
    public Image mpImage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedJoint = GetComponent<FixedJoint2D>();
        StartCoroutine(NormalAttackRoutine());
    }

    void FixedUpdate()
    {
        // 현재 y속도가 최대 낙하 속도를 넘으면 제한
        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
    }

    IEnumerator NormalAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(normalFireInterval);
            if (isGround)
            {
                // 가장 가까운 적 찾기
                Transform target = FindNearestEnemy();
                if (target != null)
                {
                    Vector2 direction = (target.position - firePoint.position).normalized;

                    GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
                    proj.GetComponent<Arrow>().SetDirection(direction);
                }

                // MP 증가
                currentMP += mpPerShot;
                mpImage.fillAmount = currentMP / maxMP;
                currentMP = Mathf.Min(currentMP, maxMP);

                // 궁극기 발동 조건 확인
                if (currentMP >= maxMP && !isUltimateActive)
                {

                    fixedJoint.connectedBody = null;
                    fixedJoint.enabled = false;

                    StartCoroutine(ActivateUltimate());
                }
            }
        }
    }

    IEnumerator ActivateUltimate()
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
            proj.GetComponent<Arrow>().SetDirection(rotation * Vector2.right);
        }
    }


    Transform FindNearestEnemy()
    {
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isGround = true;
            if (isUltimateActive) return;

            RiderManager.Instance.RiderCountUp();
            fixedJoint.enabled = true;
            fixedJoint.connectedBody = collision.transform.GetComponent<Rigidbody2D>();
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        isGround = false;
    //        transform.SetParent(null); // 점프 등으로 떨어질 경우 분리
    //    }
    //}
}