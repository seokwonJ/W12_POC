using System.Collections;
using UnityEngine;

public class SkullPattern : MonoBehaviour
{
    private EnemyHP enemyHP;
    private Enemy enemy;
    private GameObject player;
    private Animator animator;

    public float dashSpeed;
    public float dashDuration;
    public float attackPlayerDistance;
    public float dashCooldown;
    private bool isDashReady = true;
    private WaitForSeconds dashWait;
    public float changeStateCooldown; // 공격 쿨타임
    public bool isChasingDash; // Dash를 하면서 플레이어를 계속 따라갈 것인지 여부
    public WaitForSeconds changeStateWait;


    private void Awake()
    {
        Init();
    }

    private void FixedUpdate()
    {
        if (enemyHP == null || enemyHP.isDead || enemy.isKnockback || enemyHP.isSpawning) return;
        Move();
    }

    public void Init()
    {
        enemy = GetComponent<Enemy>();
        enemyHP = GetComponent<EnemyHP>();
        animator = GetComponent<Animator>();
        player = Managers.PlayerControl.NowPlayer;

        dashWait = new WaitForSeconds(dashCooldown);
        changeStateWait = new WaitForSeconds(changeStateCooldown);
        enemy.ChangeState(EEnemyState.Chase); // 초기 상태를 Chase로 설정
        StartCoroutine(CoChangeState());
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
                enemyHP.rb.linearVelocity = direction * enemy.speed;
                break;

            case EEnemyState.Attack:
                break;

            case EEnemyState.Idle:
            case EEnemyState.PrepareAttack:
                enemyHP.rb.linearVelocity = Vector2.zero;
                break;
        }

    }

    IEnumerator CoChangeState()
    {
        while (true)
        {
            yield return changeStateWait;
            yield return null;

            if (enemyHP == null || enemyHP.isDead) break;

            if (enemy.CurrentState == EEnemyState.Idle)
            {
                enemy.ChangeState(EEnemyState.Chase);
            }

            else if (isDashReady && enemy.CurrentState == EEnemyState.Chase && Vector2.Distance(enemyHP.transform.position, player.transform.position) < attackPlayerDistance && IsVisibleFrom(enemyHP.GetComponent<Renderer>(), Camera.main))
            {
                Debug.Log("SkullPattern: CoChangeState - Attack State Triggered");
                enemy.ChangeState(EEnemyState.PrepareAttack);
                animator.SetBool("isDash", true);
                yield return changeStateWait; // 대쉬 애니메이션 보여주는 시간

                enemy.ChangeState(EEnemyState.Attack);
                yield return CoDash();

                StartCoroutine(CoDashCoolDown());
                enemy.ChangeState(EEnemyState.Idle);
                animator.SetBool("isDash", false);
                
            }
        }
    }

    IEnumerator CoDash()
    {
        Vector2 directionToPlayer = (player.transform.position - enemyHP.transform.position).normalized;
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            elapsedTime += Time.deltaTime;
            if (isChasingDash)
            {
                directionToPlayer = (player.transform.position - enemyHP.transform.position).normalized; // IsChasingDash이면 대쉬를 하면서 플레이어를 계속 쫓아감
            }
            enemyHP.rb.linearVelocity = directionToPlayer * dashSpeed;
            yield return null;
        }
    }

    IEnumerator CoDashCoolDown()
    {
        isDashReady = false;
        yield return dashWait;
        isDashReady = true;
    }

    public bool IsVisibleFrom(Renderer renderer, Camera camera) // 이후 밖으로 빼서 다른 함수에서도 사용가능하게 하기
    {
        if (renderer == null || camera == null) return false;
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}

