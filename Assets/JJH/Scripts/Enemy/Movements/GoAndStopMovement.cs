using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Patterns/Movement/GoAndStopMovement")]
public class GoAndStopMovement : ScriptableObject, IMovementPattern
{
    private Enemy enemy;
    private Animator animator;
    public float goDurationTime;

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
        float elapsedTime = 0f;

        while (elapsedTime < goDurationTime)
        {
            if (enemy == null || !enemy.enabled) break;
            elapsedTime += Time.deltaTime;
            Vector2 direction = Vector3.left;
            enemy.rb.linearVelocity = direction * enemy.speed;
            yield return null;
        }
        enemy.rb.linearVelocity = Vector2.zero; // 이동이 끝나면 속도를 0으로 설정
        animator.SetTrigger("Stop");
    }
}


