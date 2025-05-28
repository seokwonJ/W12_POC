using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    // 캐릭터 기본 능력치
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

    [Header("점프")]
    public float jumpForce = 10f;
    public float maxFallSpeed = -10f;

    protected Rigidbody2D rb;
    protected FixedJoint2D fixedJoint;
    protected bool isGround = true;
    protected bool isUltimateActive = false;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedJoint = GetComponent<FixedJoint2D>();
        StartCoroutine(NormalAttackRoutine());
    }

    // 떨어질 때 속력
    protected virtual void FixedUpdate()
    {
        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
    }

    // 일반공격 부분
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
                    StartCoroutine(ActiveSkill());
                }
            }
        }
    }

    // 일반공격 투사체 관련 부분
    protected abstract void FireNormalProjectile(Vector3 targetPos);


    //스킬 사용하는 부분
    protected virtual IEnumerator ActiveSkill()
    {
        if (!isGround) yield break;

        isGround = false;
        transform.SetParent(null);
        RiderManager.Instance.RiderCountDown();

        currentMP = 0;
        mpImage.fillAmount = 0;
        isUltimateActive = true;

        //점프
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        yield return StartCoroutine(FireSkill());

        // 궁극기 끝내는 부분
        isUltimateActive = false;
    }

    // 스킬 발동 부분
    protected virtual IEnumerator FireSkill()
    {
        yield return null;
    }


    // 스킬 투사체 관련 부분
    protected virtual void FireSkillProjectiles() { }


    // 사정거리내에 적을 발견하는 함수
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

    // 착지하는 부분 
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

    // 사거리 나타내는 함수
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyDetectRadius);
    }
}