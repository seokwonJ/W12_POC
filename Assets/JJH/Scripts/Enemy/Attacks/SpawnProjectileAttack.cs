using System.Collections;
using UnityEngine;
[CreateAssetMenu(menuName = "Patterns/Attack/SpawnProjectileAttack")]
public class SpawnProjectileAttack : ScriptableObject, IAttackPattern
{
    private Enemy enemy;
    private WaitForSeconds fireWait;
    public float prevSpawnMoveTime;
    

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        fireWait = new WaitForSeconds(enemy.fireCooldown);
    }

    public void Attack()
    {
        enemy.StartCoroutine(SpawnProjectileCo());
    }

    IEnumerator SpawnProjectileCo()
    {
        yield return new WaitForSeconds(prevSpawnMoveTime);

        while (true)
        {
            if (enemy == null || !enemy.enabled)
            {
                yield break; // 적이 죽었거나 존재하지 않으면 코루틴 종료
            }
            // 발사체를 3-5개 랜덤한 수를 생성
            // 각각의 발사체가 왼쪽 위 방향부터 왼쪽 아래 방향까지 균등한 각도로 날아가도록 설정
            int projectileCount = Random.Range(4, 7); // 4에서 6개 사이의 발사체 생성
            for (int i = 0; i < projectileCount; i++)
            {
                float angle = Mathf.Lerp(-45f, 45f, (float)i / (projectileCount - 1)); // 45도에서 135도 사이의 균등한 각도
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.left; // 위쪽 방향과 곱해서 Vector2로 변경
                //
                GameObject proj = Instantiate(enemy.projectilePrefab, enemy.firePoint.position, Quaternion.Euler(0, 0, angle + 180));
                proj.GetComponent<Rigidbody2D>().linearVelocity = direction * enemy.projectileSpeed;
            }
            SoundManager.Instance.PlaySFX("BlueDragonShootProjectile");

            yield return fireWait;
        }
    }
}
