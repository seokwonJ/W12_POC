using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Patterns/Movement/GoZigZagMovement")]
public class GoZigZagMovement : ScriptableObject, IMovementPattern
{
    private Enemy enemy;
    public float zigzagInterval;
    bool isGoUpDiagonal = true;
    float zigzagTimer = 0f;
    Vector3 upDiagonal = new Vector3(-1, 0.5f, 0).normalized;
    Vector3 downDiagonal = new Vector3(-1, -0.5f, 0).normalized;

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Move()
    {
        UpdateZigzagTimer();

        if (enemy == null || !enemy.enabled) return;
        Vector2 direction = isGoUpDiagonal ? upDiagonal : downDiagonal;
        enemy.rb.linearVelocity = direction * enemy.speed;
    }

    private void UpdateZigzagTimer()
    {
        zigzagTimer += Time.fixedDeltaTime;
        if (zigzagTimer >= zigzagInterval)
        {
            isGoUpDiagonal = !isGoUpDiagonal;
            zigzagTimer = 0f;
        }
    }
}

