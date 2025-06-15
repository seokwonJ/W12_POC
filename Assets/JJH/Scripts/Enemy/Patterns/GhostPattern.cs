using System.Collections;
using UnityEngine;

public class GhostPattern : MonoBehaviour
{
    private EnemyHP enemyHP;
    private Enemy enemy;
    private GameObject player;

    public float minPlayerDistance;
    public float maxPlayerDistance;
    public float changeStateCooldown;
    private WaitForSeconds changeStateWait;
    public int attackProjectileCount; // 공격 시 발사할 투사체 개수
    public float attackCooldown; // 공격 쿨타임
    public WaitForSeconds attackWait;


    private void Awake()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (enemyHP == null || enemyHP.isDead || enemy.isKnockback) return;
        Move();
    }

    public void Init()
    {
        enemy = GetComponent<Enemy>();
        enemyHP = GetComponent<EnemyHP>();
        player = Managers.PlayerControl.NowPlayer;


        changeStateWait = new WaitForSeconds(changeStateCooldown);
        attackWait = new WaitForSeconds(attackCooldown);
        StartCoroutine(CoChangeState());
        StartCoroutine(CoAttack());
    }

    IEnumerator CoChangeState()
    {
        while (true)
        {
            if (enemyHP == null || enemyHP.isDead) break;
            // 화면 밖에 있다면 EEnemyState.Chase로 변경
            if (!IsVisibleFrom(enemyHP.GetComponent<Renderer>(), Camera.main))
            {
                enemy.ChangeState(EEnemyState.Chase);
            }
            else if (Vector2.Distance(enemyHP.transform.position, player.transform.position) > maxPlayerDistance)
            {
                enemy.ChangeState(EEnemyState.Chase);
            }
            else if (Vector2.Distance(enemyHP.transform.position, player.transform.position) < minPlayerDistance) 
            {
                // minPlayerDistance을 0이하로 하면 도망가지 않음
                enemy.ChangeState(EEnemyState.Escape);               
            }
            else
            {
                enemy.ChangeState(EEnemyState.Idle);             
            }

            yield return changeStateWait;
        }
    }
    private void Move()
    {
        if (enemyHP == null || enemyHP.isDead || enemyHP.isSpawning) return;
        // 플레이어를 향한 방향 변수 이름

        Vector2 directionToPlayer = (player.transform.position - enemyHP.transform.position).normalized;
        Vector2 direction = directionToPlayer;
        switch (enemy.CurrentState)
        {
            case EEnemyState.Chase:
                direction = directionToPlayer;
                break;
            case EEnemyState.Escape:
                direction = -0.5f * directionToPlayer;
                break;
            case EEnemyState.Idle:
                direction = Vector2.zero;
                break;
        }

        enemyHP.rb.linearVelocity = direction * enemy.speed;
    }

    IEnumerator CoAttack()
    {
        while (true)
        {
            yield return attackWait;

            if (enemyHP == null || enemyHP.isDead) break;
            // 공격가능한 상태일 때만 공격
            if (enemy.CurrentState == EEnemyState.Chase || enemy.CurrentState == EEnemyState.Idle)
            {
                // attackProjectileCount만큼 투사체를 발사
                for (int i = 0; i < attackProjectileCount; i++)
                {
                    GameObject proj = Instantiate(enemy.projectilePrefab, enemy.firePoint.position, Quaternion.identity);
                    //Vector2 dir = (enemy.player.transform.position - enemy.transform.position).normalized;
                    // 발사 각도가 플레이어를 향해 60도 이내에서 attackProjectileCount만큼 균등하게 퍼져서 발사
                    float angle = 60f / (attackProjectileCount + 1) * (i + 1) - 30f; // -30f ~ +30f 범위로 각도 설정
                    Vector2 dir = Quaternion.Euler(0, 0, angle) * (enemy.player.transform.position - enemy.transform.position).normalized;
                    proj.transform.rotation = Quaternion.LookRotation(Vector3.forward, dir); // 투사체의 방향을 설정
                    proj.GetComponent<Rigidbody2D>().linearVelocity = dir * enemy.projectileSpeed;

                }

            }
        }
    }

    public bool IsVisibleFrom(Renderer renderer, Camera camera) // 이후 밖으로 빼서 다른 함수에서도 사용가능하게 하기
    {
        if (renderer == null || camera == null) return false;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}

