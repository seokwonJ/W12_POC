using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
#if UNITY_EDITOR
    [TextArea]
    [SerializeField] private string editorNote = "";
    #pragma warning restore 0414
#endif
    public ScriptableObject movementSO;
    public ScriptableObject attackSO;

    [Header("움직임 관련")]
    public float speed;
    public float minPlayerDistance;
    public bool isKnockbackable; // 넉백 가능 여부
    protected bool isKnockback = false; // 넉백 상태인지 여부
    public bool isFilpping; // 스프라이트 뒤집기 여부

    [Header("공격 관련")]
    public int damage; 
    public float fireCooldown;
    public Transform firePoint; // 발사체 발사 위치
    public float projectileSpeed;
    public GameObject projectilePrefab;


    private IMovementPattern movement;
    private IAttackPattern attack;
    private SpriteRenderer spriteRenderer;
    private EnemyHP enemyHP; // 적의 HP 스크립트

    public GameObject player; // 이후 싱글턴에서 player를 가져오도록 변경
    public Rigidbody2D rb; 

    public Vector2 MoveDirection { get; set; } = Vector2.zero;

    protected virtual void Awake()
    {
        Init();
    }

    protected void Init()
    {
        player = Managers.PlayerControl.NowPlayer;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyHP = GetComponent<EnemyHP>();

        if (movementSO != null )
        {
            ScriptableObject movementInstance = Instantiate(movementSO);
            movement = movementInstance as IMovementPattern;
        }

        if ( attackSO != null)
        {
            ScriptableObject attackInstance = Instantiate(attackSO);
            attack = attackInstance as IAttackPattern;
        }

        movement?.Init(this);
        attack?.Init(this);

        attack?.Attack();
    }

    protected void Update()
    {
        if (spriteRenderer != null && isFilpping && !enemyHP.isDead)
        {
            //  방향에 따라 스프라이트를 뒤집음
            float directionX = transform.position.x - player.transform.position.x;
            spriteRenderer.flipX = directionX <= 0 ? false : true; // directionX가 0 이하이면 정방향, 그 외에는 역방향으로 설정

        }
    }

    protected void FixedUpdate()
    {
        if (isKnockback || enemyHP.isDead)
        {
            // 넉백 중에는 이동하지 않음
            return;
        }
        movement?.Move();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            OnPlayerCollided();
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
    protected virtual void OnPlayerCollided()
    {
        Destroy(gameObject);
    }


    public void ApplyKnockback(Vector2 direction, float knockbackPower)
    {
        if (!isKnockbackable) return;
        isKnockback = true;
        StartCoroutine(CoKnockbackTimer(0.05f));
        rb.linearVelocity = Vector2.zero; // 기존 움직임 제거
        rb.AddForce(direction.normalized * knockbackPower, ForceMode2D.Impulse);
    }

    IEnumerator CoKnockbackTimer(float knockbackTime)
    {
        while (isKnockback)
        {
            yield return new WaitForSeconds(knockbackTime);
            isKnockback = false; // 넉백 상태 해제
        }
    }
}
