using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Patterns/Movement/GoAndStopMovement")]
public class GoAndStopMovement : ScriptableObject, IMovementPattern
{
    private Enemy enemy;
    public float stopXPosition; // X 좌표에서 멈출 위치

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Move()
    {
        if (enemy == null || !enemy.enabled) return;
        Vector2 direction = Vector3.left;
        if (enemy.transform.position.x <= stopXPosition)
        {
            direction = Vector2.zero;
        }
        enemy.rb.linearVelocity = direction * enemy.speed;
    }
}


