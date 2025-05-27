using UnityEngine;

public class Enemy2 : MonoBehaviour
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
        // �̵� ���⸸ ���
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
            return; // �˹� �߿� �̵� �� ��
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
    private float knockbackTime = 0.05f; // �˹� ���� �ð�
    private float knockbackTimer = 0f;



    public void ApplyKnockback(Vector2 direction, float power)
    {
        isKnockback = true;
        knockbackTimer = knockbackTime;
        rb.linearVelocity = Vector2.zero; // ���� ������ ����
        rb.AddForce(direction.normalized * power, ForceMode2D.Impulse);
    }
}
