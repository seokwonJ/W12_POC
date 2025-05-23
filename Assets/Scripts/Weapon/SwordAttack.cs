using UnityEngine;

public class SwordAttack : MonoBehaviour
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
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        transform.up = Vector2.right;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // 태그는 필요에 따라 설정
        {
            // 데미지 입히는 로직 (적 스크립트에서 받도록 설계 가능)
            other.GetComponent<EnemyHP>().TakeDamage(damage);
            Debug.Log("Hit enemy!");
            Destroy(gameObject);
        }
    }
}
