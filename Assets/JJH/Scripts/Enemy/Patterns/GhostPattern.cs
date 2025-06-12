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
    public float attackCooldown; // 공격 쿨타임
    public WaitForSeconds attackWait;
    private bool isBrave;

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

        isBrave = Random.Range(0f, 1f) < 0.3f;

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
            // 클래스 내에 아래 메서드 추가
            else if (Vector2.Distance(enemyHP.transform.position, player.transform.position) > maxPlayerDistance)
            {
                enemy.ChangeState(EEnemyState.Chase);
            }
            else if (Vector2.Distance(enemyHP.transform.position, player.transform.position) < minPlayerDistance)
            {
                if (isBrave)
                {
                    enemy.ChangeState(EEnemyState.Chase);
                }
                else
                {
                    enemy.ChangeState(EEnemyState.Escape);
                }
            }
            else
            {
                if (isBrave)
                {
                    enemy.ChangeState(EEnemyState.Chase);
                }
                else
                {
                    enemy.ChangeState(EEnemyState.Idle);
                }
            }

            yield return changeStateWait;
        }
    }
    private void Move()
    {
        if (enemyHP == null || enemyHP.isDead) return;
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
                GameObject proj = Instantiate(enemy.projectilePrefab, enemy.firePoint.position, Quaternion.identity);
                Vector2 dir = (enemy.player.transform.position - enemy.transform.position).normalized;
                proj.GetComponent<Rigidbody2D>().linearVelocity = dir * enemy.projectileSpeed;
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

