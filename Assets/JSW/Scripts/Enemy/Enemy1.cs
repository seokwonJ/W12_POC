using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f;
    public float retreatSpeed = 4f;
    public float approachRange = 7f;
    public float attackRange = 4f;
    public float retreatDistance = 5f;
    public float fireCooldown = 2f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    private Transform player;
    private Rigidbody2D rb;
    private float lastFireTime;
    private enum State { Approach, Attack, Retreat }
    private State currentState = State.Approach;

    private Vector2 moveDirection = Vector2.zero;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Approach:
                if (distance <= attackRange)
                {
                    currentState = State.Attack;
                    moveDirection = Vector2.zero;
                }
                else
                {
                    moveDirection = (player.position - transform.position).normalized * speed;
                }
                break;

            case State.Attack:
                if (Time.time - lastFireTime > fireCooldown)
                {
                    FireProjectile();
                    lastFireTime = Time.time;
                    currentState = State.Retreat;
                }
                moveDirection = Vector2.zero;
                break;

            case State.Retreat:
                if (distance >= retreatDistance)
                {
                    currentState = State.Approach;
                }
                else
                {
                    moveDirection = (transform.position - player.position).normalized * retreatSpeed;
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        if (isKnockback)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnockback = false;
            }
            return; // 넉백 중엔 이동 안 함
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    private void FireProjectile()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 dir = (player.position - firePoint.position).normalized;
        proj.GetComponent<Rigidbody2D>().linearVelocity = dir * 8f;
    }

    private bool isKnockback = false;
    private float knockbackTime = 0.05f; // 넉백 지속 시간
    private float knockbackTimer = 0f;

    public void ApplyKnockback(Vector2 direction, float power)
    {
        isKnockback = true;
        knockbackTimer = knockbackTime;
        rb.linearVelocity = Vector2.zero; // 기존 움직임 제거
        rb.AddForce(direction.normalized * power, ForceMode2D.Impulse);
    }
}