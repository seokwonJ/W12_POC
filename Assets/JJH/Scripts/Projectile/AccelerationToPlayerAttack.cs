using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AccelerationToPlayerAttack : EnemyAttack1
{
    private GameObject player;
    private Rigidbody2D rb;

    private float acceleration = 3f;   // 가속도 (초당 속도 증가량)
    private float maxSpeed = 15f;      // 최대 속도
    private float turnSpeed = 100f;    // 회전 속도 (Degrees per second)
    private float currentSpeed = 0f;  // 현재 속도

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        player = Managers.PlayerControl.NowPlayer;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // 1) 가속도 적용
        currentSpeed += acceleration * Time.fixedDeltaTime;
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);

        // 2) 목표 방향 구하기
        Vector2 direction = ((Vector2)player.transform.position - rb.position).normalized;

        // 3) 현재 회전 각도와 목표 회전 각도의 차이를 부드럽게 보간
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float angle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, turnSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(angle);

        // 4) 전진력 적용 (속도 기반)
        rb.linearVelocity = transform.right * currentSpeed;

        // 5) 회전 속도 감소
        if (turnSpeed > 0)
        {
            turnSpeed -= 8f * Time.fixedDeltaTime; // 회전 속도를 점차 감소시켜서 갈수록 방향전환이 어렵게
        }
    }
}
