using System.Collections;
using UnityEngine;

public class Worrior : MonoBehaviour
{
    [Header("MP �ý���")]
    public int maxMP = 100;
    private int currentMP = 0;

    [Header("�Ϲ� ����")]
    public GameObject normalProjectile;
    public Transform firePoint;
    public float normalFireInterval = 1f;
    public int mpPerShot = 10;
    public float enemyDetectRadius = 10f;

    [Header("���� & �ñر�")]
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

            // ���� ����� �� ã��
            Transform target = FindNearestEnemy();
            print("target " + target);
            if (target != null)
            {
                Vector2 direction = (target.position - firePoint.position).normalized;

                GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);
            }

            // MP ����
            currentMP += mpPerShot;
            currentMP = Mathf.Min(currentMP, maxMP);

            // �ñر� �ߵ� ���� Ȯ��
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

    void FireBurstProjectiles()
    {
        GameObject proj = Instantiate(burstProjectile, firePoint.position, Quaternion.identity);
        proj.GetComponent<SwordAttack>().speed = 20;
        proj.transform.localScale *= 3;
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
        // �þ� ���� �ð�ȭ
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyDetectRadius);
    }
}
