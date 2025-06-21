using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;      // 대시 속도
    public float dashDuration = 0.2f;  // 대시 지속 시간
    public float dashCooldown;    // 대시 재사용 대기 시간

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private bool isDashing = false;
    public  bool isCanDashing;
    private bool isDashInvinvible = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private float dashLateInvincibleTime = 0.2f;
    private float dashLateInvincibleTimer = 0;

    private PlayerStatus _playerStatus;
    private PlayerAfterImageSpawner DashAfterImageSpawner;
    private GameObject _core;
    private CircleCollider2D _coreCollider;
    private SpriteRenderer _coreSpriteRenderer;
    private Vector3 _coreSize;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerStatus = GetComponent<PlayerStatus>();
        DashAfterImageSpawner = GetComponent<PlayerAfterImageSpawner>();
        _core = GetComponentInChildren<PlayerHP>().gameObject;
        _coreCollider = _core.GetComponent<CircleCollider2D>();
        _coreSpriteRenderer = _core.GetComponent<SpriteRenderer>();
        _coreSize = _core.transform.localScale;
        
    }

    void Update()
    {
        moveSpeed = _playerStatus.speed;
        Managers.Artifact.playerAction?.Invoke(this);
        // 대시 중이 아니면 입력 받기
        if (!isDashing)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput = moveInput.normalized;
        }

        // 대시 쿨타임 감소

        
        if (!isCanDashing)
        {
            if (dashCooldownTimer > 0) dashCooldownTimer -= Time.deltaTime;
            else
            {
                _coreSpriteRenderer.color = Color.green;
                isCanDashing = true;
            }
        }


        if (isDashInvinvible)
        {
            dashLateInvincibleTimer -= Time.deltaTime;
            _core.transform.localScale = Vector3.Lerp(_core.transform.localScale, _coreSize, Time.deltaTime * 20);

            if (dashLateInvincibleTimer <= 0)
            {
                isDashInvinvible = false;
                _coreCollider.enabled = true;
                _core.transform.localScale = _coreSize;
            }
        }
       

        // 스페이스 누르면 대시 시작
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && dashCooldownTimer <= 0)
        {
            DashAfterImageSpawner.enabled = true;

            Managers.Cam.DashPlayer();
            SoundManager.Instance.PlaySFX("PlayerDash");
            _coreCollider.enabled = false;
            isCanDashing = false;

            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = moveInput * dashSpeed;
            dashTimer -= Time.fixedDeltaTime;

            _core.transform.localScale = Vector3.Lerp(_core.transform.localScale, Vector3.zero, Time.deltaTime * 30);

            if (dashTimer <= 0f)
            {
                isDashInvinvible = true;
                dashLateInvincibleTimer = dashLateInvincibleTime;
                _coreSpriteRenderer.color = Color.white;

                DashAfterImageSpawner.enabled = false;
                isDashing = false;
            }
        }
        else
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }
}

