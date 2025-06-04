using System.Collections;
using UnityEngine;
[CreateAssetMenu(menuName = "Patterns/Attack/TowardPlayer")]
public class TowardPlayerAttack : ScriptableObject, IAttackPattern
{
    private Enemy enemy;
    private WaitForSeconds fireWait;

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        fireWait = new WaitForSeconds(enemy.fireCooldown);
    }

    public void Attack()
    {
        enemy.StartCoroutine(AttackCo());
    }

    IEnumerator AttackCo()
    {
        while (true)
        {
            yield return fireWait;
            if (enemy == null || !enemy.enabled)
            {
                yield break; // 적이 죽었거나 존재하지 않으면 코루틴 종료
            }
            GameObject proj = Instantiate(enemy.projectilePrefab, enemy.firePoint.position, Quaternion.identity);
            Vector2 dir = (enemy.player.transform.position - enemy.transform.position).normalized;
            proj.GetComponent<Rigidbody2D>().linearVelocity = dir * enemy.projectileSpeed;
        }
    }
}
