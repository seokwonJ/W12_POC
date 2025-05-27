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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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

            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }
        else
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }
}

