using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;      // ��� �ӵ�
    public float dashDuration = 0.2f;  // ��� ���� �ð�
    public float dashCooldown = 1f;    // ��� ���� ��� �ð�

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
        // ��� ���� �ƴϸ� �Է� �ޱ�
        if (!isDashing)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput = moveInput.normalized;
        }

        // ��� ��Ÿ�� ����
        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;

        // �����̽� ������ ��� ����
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

