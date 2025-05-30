using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Patterns/Movement/GoHorizontalMovement")]
public class GoHorizontalMovement : ScriptableObject, IMovementPattern
{
    private Enemy enemy;

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Move()
    {
        if (enemy == null || !enemy.enabled) return;
        Vector2 direction = Vector3.left;
        enemy.rb.linearVelocity = direction * enemy.speed;
    }
}


