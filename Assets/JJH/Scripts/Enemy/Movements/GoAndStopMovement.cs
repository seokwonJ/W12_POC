using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Patterns/Movement/GoAndStopMovement")]
public class GoAndStopMovement : ScriptableObject, IMovementPattern
{
    private Enemy enemy;
    private Animator animator;
    public float stopXPosition; // 예시로 -5f로 설정

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        animator = enemy.GetComponent<Animator>();
        enemy.StartCoroutine(CoMove());
    }

    public void Move()
    {
        //if (enemy == null || !enemy.enabled) return;
        //Vector2 direction = Vector3.left;
        //if (enemy.transform.position.x <= stopXPosition)
        //{
        //    direction = Vector2.zero;
        //}
        //enemy.rb.linearVelocity = direction * enemy.speed;
    }

    IEnumerator CoMove()
    {
        // goDurationTime동안 왼쪽으로 enemy.speed만큼 이동
        

        while (enemy.transform.position.x > stopXPosition)
        {
            if (enemy == null || !enemy.enabled) break;
            Vector2 direction = Vector3.left;
            enemy.rb.linearVelocity = direction * enemy.speed;
            yield return null;
        }
        enemy.rb.linearVelocity = Vector2.zero; // 이동이 끝나면 속도를 0으로 설정
        animator.SetTrigger("Stop");
        enemy.transform.position = new Vector2(stopXPosition, enemy.transform.position.y); // 정확한 위치로 이동
    }
}


