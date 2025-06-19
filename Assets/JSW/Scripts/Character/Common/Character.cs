using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [Header("기본 업그레이드")]
    public float attackPowerUpNum = 100;
    public float attackSpeedUpNum = 100;
    public float projectileSpeedUpNum = 100;
    public float projectileSizeUpNum = 100;
    public float knockbackPowerUpNum = 100;
    public float criticalProbabilityUpNum = 100;
    public float criticalDamageUpNum = 100;
    public float attackRangeUpNum = 100;
    public float manaRegenSpeedUpNum = 100;
    public float abilityPowerUpNum = 100;

    // 캐릭터 기본 능력치
    [Header("MP 시스템")]
    public float maxMP = 100f;            // 최대 마나량
    public float mpPerSecond = 7;         // 초당 마나회복량
    protected float currentMP = 0f;
    //public Image mpImage;

    [Header("일반 공격")]
    public GameObject normalProjectile;         // 기본 공격 투사체
    public Transform firePoint;                 // 공격 투사체
    public float normalFireInterval = 1f;       // 공격 후 쿨타임 (공속)
    public float enemyDetectRadius = 10f;       // 사거리

    [Header("점프")]
    public float jumpForce = 10f;               // 점프력
    public float maxFallSpeed = 10f;           // 최대 떨어지는 속도

    [Header("공격력")]
    public float attackBase;        // base 공격력
    public float attackDamage;      // 기본공격 공격력 계수
    public float abilityPower;      // 스킬 공격력 계수
    public float projectileSpeed;       // 투사체 속도
    public float criticalProbability;   // 크리티컬 확률
    public float criticalDamage;        //크리티컬 피해 배수

    [Header("기타 효과")]
    public float knockbackPower;   // 넉백 정도
    public float projectileSize;   // 투사체 크기 

    [Header("애니메이션")]
    public Animator animator;
    public Transform playerTransform;

    [Header("VFX")]
    public GameObject skillReadyEffect;
    public GameObject skillJumpEffect;
    public AfterImageSpawner fallingAfterImageSpawner;

    protected Rigidbody2D rb;
    protected FixedJoint2D fixedJoint;
    protected bool isGround = false;
    protected bool isSkillActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedJoint = GetComponent<FixedJoint2D>();
    }

    protected virtual void Start()
    {
        playerTransform = Managers.PlayerControl.NowPlayer.transform;
    }

    protected void OnEnable()
    {
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
        if (!isGround && !isSkillActive)
        {
            Vector3 direction = playerTransform.position - transform.position;
            if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            animator.SetBool("5_Fall", true);
        }
        if (!isGround) return;

        currentMP += Time.deltaTime * (mpPerSecond * (manaRegenSpeedUpNum / 100));
        currentMP = Mathf.Min(currentMP, maxMP);

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
            yield return new WaitForSeconds(normalFireInterval / (attackSpeedUpNum / 100));
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
        SoundManager.Instance.PlaySFX("SkillReady");
        yield return new WaitForSeconds(0.5f);

        fixedJoint.connectedBody = null;
        fixedJoint.enabled = false;

        isGround = false;
        //transform.SetParent(null); 필요에 의해 DH가 지웠음
        Managers.PlayerControl.NowPlayer.GetComponent<TmpPlayerControl>().SetOrderInLayer(transform);
        Managers.Status.RiderCount--;

        currentMP = 0;

        //점프
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        SoundManager.Instance.PlaySFX("Jump");

        if (skillJumpEffect != null) Instantiate(skillJumpEffect, transform.position - Vector3.up * 0.45f, Quaternion.identity, playerTransform);

        yield return StartCoroutine(FireSkill());

        // 스킬 끝내는 부분
        isSkillActive = false;
        fallingAfterImageSpawner.enabled = true;
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, enemyDetectRadius * (attackRangeUpNum / 100));
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
        if (!collision.gameObject.CompareTag("Flyer")) return;

        ContactPoint2D contact = collision.contacts[0];
        if (Vector2.Dot(contact.normal, Vector2.up) < 0.9f) return;

        if (isSkillActive || isGround) return;
        isGround = true;
        animator.SetBool("5_Fall", false);
        animator.Play("IDLE", -1, 0f);

        Managers.PlayerControl.NowPlayer.GetComponent<TmpPlayerControl>().SetOrderInLayer(transform);
        Managers.Status.RiderCount++;
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = collision.rigidbody;
        fallingAfterImageSpawner.enabled = false;
        SoundManager.Instance.PlaySFX("Landing");
    }

    // 사거리 나타내는 함수
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyDetectRadius / 2);
    }

    public virtual void EndFieldAct() // 필드전투가 종료될 때 실행
    {
        StopAllCoroutines();

        isGround = true;
        animator.SetBool("5_Fall", false);
        animator.Play("IDLE", -1, 0f);

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = Managers.PlayerControl.NowPlayer.GetComponent<Rigidbody2D>();
        fallingAfterImageSpawner.enabled = false;
        isSkillActive = false;
    }

    public void FixCharacter() // 게임 시작할 때 플레이어 고정하기. EndFieldAct에서 코루틴 파트만 제외된거임
    {
        isGround = true;
        animator.Play("IDLE", -1, 0f);

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        fixedJoint.enabled = true;
        fixedJoint.connectedBody = Managers.PlayerControl.NowPlayer.GetComponent<Rigidbody2D>();
        fallingAfterImageSpawner.enabled = false;
        isSkillActive = false;
    }

    public float TotalAttackDamage()
    {
        float totalDamage;

        totalDamage = (attackBase * (attackPowerUpNum / 100)) * (attackDamage / 100);
        ;
        print("평타 데미지 !!!!! + " + totalDamage);
        return totalDamage;
    }

    public float TotalSkillDamage()
    {
        float totalDamage;

        totalDamage = (attackBase * (attackPowerUpNum / 100)) * ((abilityPowerUpNum / 100) * (abilityPower / 100));

        bool isCritical = IsCriticalHit();

        //if (isCritical) totalDamage *= (criticalDamage / 100);

        print("스킬 데미지 !!!!! + " + totalDamage);
        return totalDamage;
    }


    public bool IsCriticalHit()
    {
        return Random.value < (criticalProbability * criticalProbabilityUpNum / 100) / 100;
    }

}

public enum ECharacterType
{
    None,
    Archer,
    Fox,
    Levi,
    Ninja,
    Magician,
    Pirate,
    Tanker,
    Priest,
    Sniper,
    BlackHole
}