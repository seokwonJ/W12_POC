using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab; // Inspector에서 넣어줌
    public Transform firePoint; // 투사체 생성 위치 (없으면 자신의 위치 사용)
    public float projectileSpeed = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        if (projectilePrefab == null) return;
        
        // 발사 위치 결정 (firePoint가 없으면 자신의 위치 사용)
        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        Quaternion spawnRotation = firePoint != null ? firePoint.rotation : transform.rotation;
        
        // 투사체 생성
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, spawnRotation);

        // Rigidbody2D가 있으면 속도 적용
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = projectile.AddComponent<Rigidbody2D>();
            
        rb.gravityScale = 0;
        // 발사 방향도 firePoint 기준으로 설정
        Vector3 fireDirection = firePoint != null ? firePoint.right : transform.right;
        rb.linearVelocity = fireDirection * projectileSpeed;
    }
}