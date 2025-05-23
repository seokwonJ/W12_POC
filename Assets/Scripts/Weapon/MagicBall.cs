using UnityEngine;

public class MagicBall : MonoBehaviour
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
        if (other.CompareTag("Enemy")) // �±״� �ʿ信 ���� ����
        {
            // ������ ������ ���� (�� ��ũ��Ʈ���� �޵��� ���� ����)
            Debug.Log("Hit enemy!");
            Destroy(gameObject);
        }
    }
}
