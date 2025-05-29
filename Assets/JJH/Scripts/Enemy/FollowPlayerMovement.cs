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
        Vector2 direction = (enemy.player.transform.position - enemy.transform.position).normalized;
        enemy.rb.linearVelocity = direction * enemy.speed;
    }
}


