using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;      // 대시 속도
    public float dashDuration = 0.2f;  // 대시 지속 시간
    public float dashCooldown = 1f;    // 대시 재사용 대기 시간

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;

    private PlayerStatus _playerStatus;
    private PlayerAfterImageSpawner DashAfterImageSpawner;
    private GameObject _core;
    private CircleCollider2D _coreCollider;
    private Vector3 _colliderSize;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerStatus = GetComponent<PlayerStatus>();
        DashAfterImageSpawner = GetComponent<PlayerAfterImageSpawner>();
        _core = GetComponentInChildren<PlayerHP>().gameObject;
        _coreCollider = _core.GetComponent<CircleCollider2D>();
        _colliderSize = _core.transform.localScale;
    }

    void Update()
    {
        moveSpeed = _playerStatus.speed;
        // 대시 중이 아니면 입력 받기
        if (!isDashing)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput = moveInput.normalized;
        }

        // 대시 쿨타임 감소
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        // 스페이스 누르면 대시 시작
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && dashCooldownTimer <= 0)
        {
            DashAfterImageSpawner.enabled = true;

            Managers.Cam.DashPlayer();
            SoundManager.Instance.PlaySFX("PlayerDash");
            _coreCollider.enabled = false;

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

            _core.transform.localScale = Vector3.Lerp(_core.transform.localScale, Vector3.zero, Time.deltaTime * 20);

            if (dashTimer <= 0f)
            {
                _coreCollider.enabled = true;
                DashAfterImageSpawner.enabled = false;
                isDashing = false;
            }
        }
        else
        {
            rb.linearVelocity = moveInput * moveSpeed;

            _core.transform.localScale = Vector3.Lerp(_core.transform.localScale, _colliderSize, Time.deltaTime * 30);
        }
    }
}

