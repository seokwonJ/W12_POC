using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    // 캐릭터 기본 능력치
    [Header("MP 시스템")]
    public float maxMP = 100f;
    public float mpPerSecond = 7;         // 초당 마나회복량
    protected float currentMP = 0f;       // 
    public Image mpImage;

    [Header("일반 공격")]
    public GameObject normalProjectile;         // 기본 공격 투사체
    public Transform firePoint;                 // 공격 투사체
    public float normalFireInterval = 1f;       // 공격 후 쿨타임 (공속)
    public float enemyDetectRadius = 10f;       // 사거리

    [Header("점프")]
    public float jumpForce = 10f;               // 점프력
    public float maxFallSpeed = 10f;           // 최대 떨어지는 속도

    [Header("공격력")]
    public int attackDamage;      // ad 물리공격력
    public int abilityPower;      // ap 주문력
    public float projectileSpeed;       // 투사체 속도

    [Header("애니메이션")]
    public Animator animator;
    public Transform playerTransform;

    public GameObject skillReadyEffect;
    public GameObject skillJumpEffect;
    public GameObject fallingTrail;

    protected Rigidbody2D rb;
    protected FixedJoint2D fixedJoint;
    protected bool isGround = false;
    protected bool isSkillActive = false;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedJoint = GetComponent<FixedJoint2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(NormalAttackRoutine());
    }

    // 떨어질 때 속력
    protected virtual void FixedUpdate()
    {
        if (rb.linearVelocity.y < -maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxFallSpeed);
        }
    }

    protected virtual void Update()
    {
        if (!Managers.Stage.OnField)
        {
            currentMP = 0f;
            isSkillActive = false;
            return;
        }
        if (!isGround && !isSkillActive)
        {
            Vector3 direction = playerTransform.position - transform.position;
            if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            animator.SetBool("5_Fall", true);
        }
        if (!isGround) return;

        currentMP += Time.deltaTime * mpPerSecond;
        currentMP = Mathf.Min(currentMP, maxMP);
        if (mpImage != null) mpImage.fillAmount = currentMP / maxMP;

        if (currentMP >= maxMP && !isSkillActive)
        {
            StartCoroutine(ActiveSkill());
            isSkillActive = true;
        }
    }

    // 일반공격 부분
    protected virtual IEnumerator NormalAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(normalFireInterval);
            if (!isGround) continue;

            Transform target = FindNearestEnemy();
            if (target != null)
            {
                animator.Play("ATTACK", -1, 0f);
                FireNormalProjectile(target.position);
            }
        }
    }

    // 일반공격 투사체 관련 부분
    protected abstract void FireNormalProjectile(Vector3 targetPos);


    //스킬 사용하는 부분
    protected virtual IEnumerator ActiveSkill()
    {
        if (!isGround) yield break;

        Instantiate(skillReadyEffect, transform.position, Quaternion.identity, transform);
        yield return new WaitForSeconds(0.5f);

        fixedJoint.connectedBody = null;
        fixedJoint.enabled = false;

        isGround = false;
        transform.SetParent(null);
        Managers.Status.RiderCount--;

        currentMP = 0;
        if (mpImage != null) mpImage.fillAmount = 0;

        //점프
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        SoundManager.Instance.PlaySFX("jumpSE2");

        if (skillJumpEffect != null) Instantiate(skillJumpEffect,transform.position - Vector3.up * 0.45f,Quaternion.identity, playerTransform);

        yield return StartCoroutine(FireSkill());

        // 스킬 끝내는 부분
        isSkillActive = false;
        fallingTrail.SetActive(true);
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

        if (isSkillActive || isGround) return;
        isGround = true;
        animator.SetBool("5_Fall", false);
        animator.Play("IDLE", -1, 0f);

        Managers.Status.RiderCount++;
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
        fallingTrail.SetActive(false);
    }

    // 사거리 나타내는 함수
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyDetectRadius/2);
    }
}