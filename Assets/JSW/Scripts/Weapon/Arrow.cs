using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public int damage = 10;

    private Vector2 direction;

    void Start()
    {
        //Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + -90);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // 태그는 필요에 따라 설정
        {
            // 데미지 입히는 로직 (적 스크립트에서 받도록 설계 가능)
            Debug.Log("Hit enemy!");
            other.GetComponent<EnemyHP>().TakeDamage(damage);

            //Enemy2 rb2 = other.GetComponent<Enemy2>();
            //if (rb2!= null)
            //{
            //    Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
            //    rb2.ApplyKnockback(knockbackDirection, 1);
            //}
            //else
            //{
            //    EnemyAI rb1 = other.GetComponent<EnemyAI>();

            //    Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;
            //    rb1.ApplyKnockback(knockbackDirection, 1);
            //}
            Destroy(gameObject);
        }
    }
}
