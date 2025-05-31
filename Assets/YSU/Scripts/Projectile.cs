using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f; // 몇 초 후 자동 삭제

    void Start()
    {
        // 일정 시간 후 자동 파괴
        Destroy(gameObject, lifetime);
    }

    // 2D 충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 벽이나 적에 부딪히면 파괴
        Debug.Log($"Projectile hit: {collision.name}");
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 일반 충돌 처리
        Debug.Log($"Projectile hit: {collision.collider.name}");
        Destroy(gameObject);
    }
}