using UnityEngine;

public class ApproachState : IEnemyState
{
    Enemy enemy;
    public ApproachState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
    }
    public void Update()
    {
        Move();
    }
    public void Exit()
    {

    }

    private void Move()
    {
        float distance = Vector2.Distance(enemy.transform.position, enemy.player.position);

        if (distance <= enemy.attackRange)
        {
            enemy.MoveDirection = Vector2.zero;
            enemy.ChangeState(enemy.AttackState);
        }
        else
        {
            enemy.MoveDirection = (enemy.player.position - enemy.transform.position).normalized * enemy.speed;
        }
    }
}
