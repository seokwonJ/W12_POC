using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Patterns/Movement/FollowPlayer")]
public class FollowPlayerMovement : ScriptableObject, IMovementPattern
{
    private Enemy enemy;

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Move()
    {
        if (enemy == null || !enemy.enabled) return;
        // 최소거리보다 더 가까이 있으면 멈추기
        if (Vector2.Distance(enemy.transform.position, enemy.player.transform.position) < enemy.minPlayerDistance)
        {
            enemy.rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 direction = (enemy.player.transform.position - enemy.transform.position).normalized;
        enemy.rb.linearVelocity = direction * enemy.speed;
    }
}


