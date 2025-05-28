using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [Header("MP 시스템")]
    public float maxMP = 100f;
    protected float currentMP = 0f;
    public Image mpImage;

    [Header("일반 공격")]
    public GameObject normalProjectile;
    public Transform firePoint;
    public float normalFireInterval = 1f;
    public int mpPerShot = 10;
    public float enemyDetectRadius = 10f;

    [Header("궁극기 & 점프")]
    public float jumpForce = 10f;
    public GameObject burstProjectile;
    public int burstCount = 3;
    public float burstInterval = 0.3f;
    public float burstFireDelay = 0.1f;

    protected Rigidbody2D rb;
    protected FixedJoint2D fixedJoint;
    protected bool isGround = true;
    protected bool isUltimateActive = false;

    public float maxFallSpeed = -10f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedJoint = GetComponent<FixedJoint2D>();
        StartCoroutine(NormalAttackRoutine());
    }

    protected virtual void FixedUpdate()
    {
        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
    }

    protected IEnumerator NormalAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(normalFireInterval);
            if (!isGround) continue;

            Transform target = FindNearestEnemy();
            if (target != null)
            {
                FireNormalProjectile(target.position);

                currentMP += mpPerShot;
                currentMP = Mathf.Min(currentMP, maxMP);
                mpImage.fillAmount = currentMP / maxMP;

                if (currentMP >= maxMP && !isUltimateActive)
                {
                    fixedJoint.connectedBody = null;
                    fixedJoint.enabled = false;
                    StartCoroutine(ActivateUltimate());
                }
            }
        }
    }

    // 🔥 직업별로 반드시 구현해야 하는 부분
    protected abstract void FireNormalProjectile(Vector3 targetPos);

    protected virtual IEnumerator ActivateUltimate()
    {
        if (!isGround) yield break;

        isGround = false;
        transform.SetParent(null);
        RiderManager.Instance.RiderCountDown();

        isUltimateActive = true;
        currentMP = 0;
        mpImage.fillAmount = 0;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        //for (int i = 0; i < burstCount; i++)
        //{
        //    yield return new WaitForSeconds(burstFireDelay);
        //    FireBurstProjectiles();
        //    yield return new WaitForSeconds(burstInterval);
        //}

        isUltimateActive = false;
    }

    protected virtual void FireBurstProjectiles()
    {
        //// 기본 360도 탄막
        //int count = 10;
        //float angleStep = 360f / count;

        //for (int i = 0; i < count; i++)
        //{
        //    float angle = i * angleStep;
        //    Quaternion rotation = Quaternion.Euler(0, 0, angle);
        //    GameObject proj = Instantiate(burstProjectile, transform.position, rotation);
        //    //proj.GetComponent<Projectile>().SetDirection(rotation * Vector2.right);
        //}
    }

    protected Transform FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, enemyDetectRadius);
        float minDist = float.MaxValue;
        Transform nearest = null;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearest = hit.transform;
                }
            }
        }

        return nearest;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        ContactPoint2D contact = collision.contacts[0];
        if (Vector2.Dot(contact.normal, Vector2.up) < 0.9f) return;

        isGround = true;
        if (isUltimateActive) return;

        RiderManager.Instance.RiderCountUp();
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyDetectRadius);
    }
}