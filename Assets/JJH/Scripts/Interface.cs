using UnityEngine;

public interface IMovementPattern
{
    void Init(Enemy enemy);  // 초기화(위치, 파라미터)
    void Move();                 // 매 프레임 호출
}

public interface IAttackPattern
{
    void Init(Enemy enemy);
    void Attack();               // 매 프레임 혹은 타이밍에 호출
}

public interface IEnemyPattern
{
    void Init(Enemy enemy); // 초기화(위치, 파라미터)
    void Move();               // 매 프레임 호출
}