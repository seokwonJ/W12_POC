using UnityEngine;

public class Enemy2JH : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 이동 방향만 계산
        moveDirection = (player.position - transform.position).normalized;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHP>().TakeDamage(50);
            Destroy(gameObject);
        }
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
