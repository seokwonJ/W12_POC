using UnityEngine;
using UnityEngine.EventSystems;

public class AttackState : IEnemyState
{
    Enemy enemy;
    private float lastFireTime;
    public float ProejectileSpeed { get; set; } = 8f; 
    public float fireCooldown { get; set; } = 2f;
    public AttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
    }
    public void Update()
    {
        Attack();
    }
    public void Exit()
    {
    }
    public void Attack()
    {
        if (Time.time - lastFireTime > fireCooldown)
        {
            FireProjectile();
            lastFireTime = Time.time;
            enemy.ChangeState(enemy.ApproachState);
        }
        enemy.MoveDirection = Vector2.zero;
    }

    private void FireProjectile()
    {
        if (enemy.projectilePrefab != null && enemy.firePoint != null)
        {
            GameObject projectile = Object.Instantiate(enemy.projectilePrefab, enemy.firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (enemy.player.position - enemy.firePoint.position).normalized;
                rb.linearVelocity = direction * ProejectileSpeed;
            }
        }
    }
}
