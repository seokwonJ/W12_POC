using UnityEngine;

public class RetreatState : IEnemyState
{
    Enemy enemy;
    public RetreatState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
      
    }
    public void Update()
    {
        Retreat();
    }
    public void Exit()
    {

    }

    private void Retreat()
    {

    }
}
