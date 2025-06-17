using System.Collections;
using UnityEngine;
[CreateAssetMenu(menuName = "Patterns/Attack/NecromancerAttack")]
public class NecromancerAttack : ScriptableObject, IAttackPattern
{
    private Enemy enemy;
    private EnemyHP enemyHP;
    private Animator anim;
    private GameObject player;
    private SpriteRenderer spriteRenderer;
    private EnemySpawner enemySpawner; 

    [Header("공격 패턴 관련")]
    private float prevSpawnMoveTime = 3f;
    private float attackCooldown = 3.5f; // 공격 패턴 사이 간격
    private WaitForSeconds attackWait;
    private bool isOnRight = true;
    
    [Header("중간 크기의 발사체 지팡이에서 발사하는 패턴 관련")]
    public GameObject mediumProjectilePrefab; // 중간 크기 발사체 프리팹
    private int mediumProjectileCount = 2; // 중간 크기 발사 횟수
    private int mediumProjectileCountInOneRoutine = 43; // 중간 크기를 한 루틴에 만들어 내는 발사체 개수
    private float mediumProjectileAngle = 17.7f;
    private float mediumProjectileSpeed = 15f; // 중간 크기 발사체 속도
    private float mediumProjectileCoolDown = 1.5f; // 중간 크기 발사체 생성 간격
    private WaitForSeconds mediumProjectileWait;
    private float mediumOneProjectileCoolDown = 0.035f; // 중간 크기 발사체 생성 간격
    private WaitForSeconds mediumOneProjectileWait;
    private float beforeMeidiumProjectileCoolDown = 1f; // 중간 크기 발사체 생성 전 애니메이션만 바뀌는 시간
    private WaitForSeconds beforeMediumProjectileWait;

    [Header("큰 크기의 발사체 오른쪽에서 발사하는 패턴 관련")]
    public GameObject largeProjectilePrefab; // 큰 크기 발사 웨이브
    private int largeProjectileCount = 9; // 큰 크기 발사 횟수
    private int largeProjectileNumPerSpawn = 5; // 한 번에 생성되는 큰 크기 발사체 수
    private float largeProjectileSpeed = 16f; // 큰 크기 발사체 속도
    private float largeProjectileCoolDown = 0.3f; // 큰 크기 발사체 생성 간격
    private WaitForSeconds largeProjectileWait;
    private float afterLargePjojectileCoolDown = 2.5f; // 큰 크기 발사체 생성 후 대기 시간
    private WaitForSeconds afterLargeProjectileWait;

    private Vector2 spawnMissleMin; 
    private Vector2 spawnMissileMax;

    [Header("해골 발사체 발사하는 패턴 관련")]
    public EnemyWaveSO skullProjectileWave; // 해골 발사 웨이브
    private int skullProjectileCount = 2; // 해골 발사 횟수
    private WaitForSeconds afterSkullProjectileWait = new WaitForSeconds(6f);



    [Header("대쉬 패턴 관련")]

    private int dashActivateCount = 0; // 대쉬 패턴을 실행한 횟수
    private float dashSpeed = 30f; // 대쉬 속도
    private float beforeDashCoolDown = 1f; // 대쉬 전 애니메이션만 바뀌는 시간
    private WaitForSeconds beforeDashCoolDownWait;
    private float dashDuration = 2f; // 대쉬 지속 시간
    private WaitForSeconds dashDurationWait;

    public void Init(Enemy enemy)
    {
        Debug.Log(Time.deltaTime);
        dashActivateCount = 0;
        enemyHP = enemy.GetComponent<EnemyHP>();
        this.enemy = enemy;
        anim = enemy.GetComponent<Animator>();
        spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        enemySpawner = FindAnyObjectByType<EnemySpawner>();

        player = Managers.PlayerControl.NowPlayer;
        attackWait = new WaitForSeconds(attackCooldown);
        mediumProjectileWait = new WaitForSeconds(mediumProjectileCoolDown);
        mediumOneProjectileWait = new WaitForSeconds(mediumOneProjectileCoolDown);
        beforeMediumProjectileWait = new WaitForSeconds(beforeMeidiumProjectileCoolDown);
        largeProjectileWait = new WaitForSeconds(largeProjectileCoolDown);
        afterLargeProjectileWait = new WaitForSeconds(afterLargePjojectileCoolDown);
        beforeDashCoolDownWait = new WaitForSeconds(beforeDashCoolDown);
        dashDurationWait = new WaitForSeconds(dashDuration);

        spawnMissleMin = EnemySpawner.SPAWN_AREA_MIN + new Vector2(-2f, -1f);
        spawnMissileMax = EnemySpawner.SPAWN_AREA_MAX + new Vector2(+2f, +1f);
    }

    public void Attack()
    {
        enemy.StartCoroutine(CoAttackPattern());
        //enemy.StartCoroutine(CoTimer());
    }

    IEnumerator CoAttackPattern()
    {
        Debug.Log("NecromancerAttack->CoAttackPattern Start");
        yield return new WaitForSeconds(prevSpawnMoveTime);
        while (true)
        {
            if (enemy == null || !enemy.enabled || enemyHP.isDead)
            {
                yield break; // 적이 죽었거나 존재하지 않으면 코루틴 종료
            }

            int randomNum = 1;

            switch (randomNum)
            {
                case 0:
                    // 중간 크기의 발사체 지팡이에서 발사하는 패턴 관련
                    yield return enemy.StartCoroutine(CoSpawnMediumProjectiles());
                    break;
                case 1:
                    // 큰 크기의 발사체 오른쪽에서 발사하는 패턴 관련
                    //yield return enemy.StartCoroutine(CoSpawnLargeProjectiles());

                    // 해골 미사일 발사체를 소환
                    enemySpawner.SpawnEnemys(skullProjectileWave, skullProjectileCount);
                    yield return afterSkullProjectileWait;
                    break;
                case 2:
                case 3:
                    // 대쉬
                    yield return enemy.StartCoroutine(CoDash());
                    break;
            }
            yield return attackWait;
        }
    }

    IEnumerator CoSpawnMediumProjectiles()
    {
        anim.SetTrigger("FireEnter"); // 공격 애니메이션 시작    
        yield return beforeMediumProjectileWait; // 중간 크기 발사체 생성 전 대기

        // mediumProjectileCount의 크기만큼 반복하여 중간 크기의 발사체 생성
        for (int i = 0; i < mediumProjectileCount; i++)
        {
            yield return enemy.StartCoroutine(CoSpawnMediumProjectilesOneRoutine(mediumProjectileCountInOneRoutine));
            yield return mediumProjectileWait; // 다음 발사체 생성까지 대기
        }

        anim.SetTrigger("FireExit");// 공격 애니메이션 끝
    }

    IEnumerator CoSpawnMediumProjectilesOneRoutine(int projectileCount)
    {
        // 발사체 여러개를 동시에 생성
        // 각각의 발사체가 360도 방향으로 균등한 각도로 날아가도록 설정

        float randomAngle = Random.Range(0f, 360f);
        SoundManager.Instance.PlaySFX("BlueDragonShootProjectile");
        int dir = Random.Range(0, 2) == 0 ? 1 : -1;
        for (int i = 0; i < projectileCount; i++)
        { 
            float angle = mediumProjectileAngle * i * dir + randomAngle ; // 시계 방향 혹은 시계 반대방향으로 회전
            for (int j = 0; j < 3; j++)
            {
                angle += 120f; // 120도씩 회전하여 3개의 발사체 생성
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.left; // 왼쪽 방향과 곱해서 Vector2로 변경
                GameObject proj = Instantiate(mediumProjectilePrefab, enemy.firePoint.position, Quaternion.Euler(0, 0, angle + 180));
                proj.GetComponent<Rigidbody2D>().linearVelocity = direction * mediumProjectileSpeed;
            }
            yield return mediumOneProjectileWait;
        }
        yield return null; // 프레임 대기
    }

    //IEnumerator CoSpawnLargeProjectiles()
    //{
    //    for (int i = 0; i < largeProjectileCount; i++)
    //    {
    //        yield return enemy.StartCoroutine(CoSpawnLargeProjectilesOnce());
    //        yield return largeProjectileWait; // 다음 발사체 생성까지 대기
    //    }
    //    yield return afterLargeProjectileWait; // 큰 크기 발사체 생성 후 대기
    //}

    //IEnumerator CoSpawnLargeProjectilesOnce()
    //{
    //    // min_right_side_inde와 max_right_side_index 사이의 랜덤한 인덱스를 largeProjectileNumPerSpawn개수만큼 선택
    //    int[] randomYPositions = new int[largeProjectileNumPerSpawn];
    //    for (int i = 0; i < largeProjectileNumPerSpawn; i++)
    //    {
    //        int randomYPos;
    //        do
    //        {
    //            randomYPos = Random.Range((int)spawnMissleMin.y, (int)spawnMissileMax.y + 1);
    //        } while (System.Array.IndexOf(randomYPositions, randomYPos) != -1); // 중복 방지
    //        randomYPositions[i] = randomYPos;
    //    }

    //    float Xpos = isOnRight ? spawnMissileMax.x : spawnMissleMin.x; // 오른쪽에서 생성할지 왼쪽에서 생성할지 결정 
    //    // 큰 크기의 발사체들을 랜덤한 인덱스에서 생성
    //    for (int i = 0; i < largeProjectileNumPerSpawn; i++)
    //    {
    //        Vector3 spawnPos = new Vector3(Xpos, randomYPositions[i], 0);
    //        Vector3 randomPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-1.5f, 1.5f), 0); // 약간의 랜덤 위치 조정
    //        Quaternion rotate = isOnRight ? Quaternion.Euler(0, 0, 180) : Quaternion.Euler(0, 0, 0); // 오른쪽에서 생성할 때는 180도, 왼쪽에서 생성할 때는 0도 회전
    //        Vector2 velocityDirection = isOnRight ? Vector2.left : Vector2.right; // 오른쪽에서 생성할 때는 왼쪽 방향, 왼쪽에서 생성할 때는 오른쪽 방향
    //        GameObject proj = Instantiate(largeProjectilePrefab, spawnPos+ randomPosition, rotate);
    //        proj.GetComponent<Rigidbody2D>().linearVelocity = velocityDirection * largeProjectileSpeed;

    //    }
    //    //SoundManager.Instance.PlaySFX("BlueDragonShootProjectile");
    //    yield return null; // 프레임 대기

    //}


    IEnumerator CoDash()
    {
        dashActivateCount++; // 대쉬 패턴을 실행한 횟수 증가
        anim.SetBool("IsOnFast", true); // 대쉬 애니메이션 시작
        yield return beforeDashCoolDownWait;
        anim.SetBool("IsOnFast", false); // 대쉬 애니메이션 끝

        Vector2 directionToPlayer = (player.transform.position - enemy.transform.position).normalized;
        enemy.rb.linearVelocity = directionToPlayer * dashSpeed;
        yield return dashDurationWait;

        enemy.rb.linearVelocity = Vector2.zero; // 대쉬가 끝나면 속도를 0으로 설정
        isOnRight = !isOnRight; // 대쉬 후 방향을 반대로 변경
        spriteRenderer.transform.rotation = Quaternion.Euler(0, isOnRight ? 0 : 180, 0); // 스프라이트 방향 변경

        Vector3 newPos = isOnRight ? new Vector3(spawnMissileMax.x + 4f, 0, 0) : new Vector3(spawnMissleMin.x - 4f, 0, 0);
        enemy.transform.parent.position = newPos; // 대쉬 후 위치를 오른쪽 혹은 왼쪽으로 이동
        Vector2 direction = isOnRight ? Vector2.left : Vector2.right;
        enemy.rb.linearVelocity = direction * enemy.speed * 2;
        yield return new WaitForSeconds(1.5f);
        enemy.rb.linearVelocity = Vector2.zero;

    }
}
