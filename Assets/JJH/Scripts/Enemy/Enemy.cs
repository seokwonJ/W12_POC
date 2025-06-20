using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
#if UNITY_EDITOR
    [TextArea]
    [SerializeField] private string editorNote = "";
    #pragma warning restore 0414
#endif
    public ScriptableObject movementSO;
    public ScriptableObject attackSO;
    public bool isBoss; // 보스 여부

    [Header("움직임 관련")]
    public float speed;
    private float defaultSpeed; // 원래 속도
    public float minPlayerDistance;
    public bool isKnockbackable; // 넉백 가능 여부
    public bool isSlowable = true; // 느려짐 가능 여부
    public bool isKnockback = false; // 넉백 상태인지 여부
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

    public EEnemyState CurrentState { get; private set; }

    public Vector2 MoveDirection { get; set; } = Vector2.zero;

    protected virtual void Awake()
    {
        Init();
    }

    protected void Init()
    {
        defaultSpeed = speed; // 원래 속도를 저장

        player = Managers.PlayerControl.NowPlayer;
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("Rigidbody2D가 없으므로 부모 오브젝트에서 가져옵니다.");
            rb = GetComponentInParent<Rigidbody2D>();
        }
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


    public void ChangeState(EEnemyState eEnemyState)
    {
        CurrentState = eEnemyState;
    }


    protected void Update()
    {
        if (spriteRenderer != null && isFilpping && !enemyHP.isDead && !enemyHP.isSpawning)
        {
            //  방향에 따라 스프라이트를 뒤집음
            float directionX = transform.position.x - player.transform.position.x;
            spriteRenderer.flipX = directionX <= 0 ? false : true; // directionX가 0 이하이면 정방향, 그 외에는 역방향으로 설정

        }
    }

    protected void FixedUpdate()
    {
        if (isKnockback || enemyHP.isDead || enemyHP.isSpawning)
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
            Debug.Log($"{gameObject.name} 적이 플레이어와 충돌해서 {damage} 데미지");
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            //OnPlayerCollided();
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

    Coroutine co = null;        // 함수 밖에  나왔어야 초기화가 안됨
    public void ApplySlow(int value, float durationTime, bool isPercent = true)
    {
        if (!isSlowable) return;
        float resultValue = isPercent ? defaultSpeed * (value / 100f) : value;
        float resultSpeed = defaultSpeed - resultValue;
        if (resultSpeed <= speed)
        {
            if (co != null) StopCoroutine(co);
            co = StartCoroutine(CoApplySlow(resultSpeed, durationTime));
        }
    }

    IEnumerator CoApplySlow(float finalSpeed, float durationTime)
    {
        speed = Mathf.Max(finalSpeed, 1f); // 속도가 1 이하로 내려가지 않도록 제한
        Debug.Log($"{gameObject.name} 적의 속도 감소, 현재 속도: {speed}");
        yield return new WaitForSeconds(durationTime);
        speed = defaultSpeed; // 원래 속도로 복원
        Debug.Log($"{gameObject.name} 적의 속도 복원, 원래 속도: {defaultSpeed}");
    }
}

public enum EEnemyState
{
    Idle,
    Chase,
    Escape,
    Attack,
    PrepareAttack,
}
