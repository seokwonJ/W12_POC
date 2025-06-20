﻿using System.Collections;
using UnityEngine;
[CreateAssetMenu(menuName = "Patterns/Attack/DemonDragonAttack")]
public class DemonDragonAttack : ScriptableObject, IAttackPattern
{
    private Enemy enemy;
    private EnemyHP enemyHP;
    private Animator anim;
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    [Header("공격 패턴 관련")]
    private float prevSpawnMoveTime = 3f;
    private float attackCooldown = 3.5f; // 공격 패턴 사이 간격
    private WaitForSeconds attackWait;
    private bool isOnRight = true;
    
    [Header("중간 크기의 발사체 입에서 발사하는 패턴 관련")]
    public GameObject mediumProjectilePrefab; // 중간 크기 발사체 프리팹
    private int mediumProjectileCount = 6; // 중간 크기 발사 횟수
    private float mediumProjectileSpeed = 9f; // 중간 크기 발사체 속도
    private float mediumProjectileCoolDown = 0.5f; // 중간 크기 발사체 생성 간격
    private WaitForSeconds mediumProjectileWait;
    private float beforeMeidiumProjectileCoolDown = 0.6f; // 중간 크기 발사체 생성 전 애니메이션만 바뀌는 시간
    private WaitForSeconds beforeMediumProjectileWait;

    [Header("큰 크기의 발사체 오른쪽에서 발사하는 패턴 관련")]
    public GameObject largeProjectilePrefab; // 큰 크기 발사체 프리팹
    private int largeProjectileCount = 9; // 큰 크기 발사 횟수
    private int largeProjectileNumPerSpawn = 5; // 한 번에 생성되는 큰 크기 발사체 수
    private float largeProjectileSpeed = 16f; // 큰 크기 발사체 속도
    private float largeProjectileCoolDown = 0.3f; // 큰 크기 발사체 생성 간격
    private WaitForSeconds largeProjectileWait;
    private float afterLargePjojectileCoolDown = 2.5f; // 큰 크기 발사체 생성 후 대기 시간
    private WaitForSeconds afterLargeProjectileWait;

    private Vector2 spawnMissleMin; 
    private Vector2 spawnMissileMax; 

    [Header("대쉬 패턴 관련")]

    private int dashActivateCount = 0; // 대쉬 패턴을 실행한 횟수
    private float dashSpeed = 30f; // 대쉬 속도
    private float beforeDashCoolDown = 1f; // 대쉬 전 애니메이션만 바뀌는 시간
    private WaitForSeconds beforeDashCoolDownWait;
    private float dashDuration = 2.5f; // 대쉬 지속 시간
    private WaitForSeconds dashDurationWait;

    public void Init(Enemy enemy)
    {
        dashActivateCount = 0;
        enemyHP = enemy.GetComponent<EnemyHP>();
        this.enemy = enemy;
        anim = enemy.GetComponent<Animator>();
        spriteRenderer = enemy.GetComponent<SpriteRenderer>();

        player = Managers.PlayerControl.NowPlayer;
        attackWait = new WaitForSeconds(attackCooldown);
        mediumProjectileWait = new WaitForSeconds(mediumProjectileCoolDown);
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
        Debug.Log("BlueDragonAttack->CoAttackPattern Start");
        yield return new WaitForSeconds(prevSpawnMoveTime);
        while (true)
        {
            if (enemy == null || !enemy.enabled || enemyHP.isDead)
            {
                yield break; // 적이 죽었거나 존재하지 않으면 코루틴 종료
            }

            int randomNum;
            if (isOnRight ^ Managers.PlayerControl.IsOnRightSide()) // 플레이어와 적이 다른쪽에 있다면 대쉬패턴을 포함한 랜덤 
            {
                if (dashActivateCount == 0) // 대쉬를 0번 했다면 대쉬
                {
                    randomNum = 3;
                }
                else if (dashActivateCount == 1) // 대쉬를 한 번 했다면 50% 확률로 대쉬
                {
                    randomNum = Random.Range(0, 4);
                }
                else // 대쉬를 두 번 이상 했다면 33% 확률로 대쉬
                {
                    randomNum = Random.Range(0, 3);
                }
            }
            else  // 플레이어와 적이 모두 오른쪽 혹은 왼쪽에 있다면 대쉬 패턴은 하지 않음
            {
                randomNum = Random.Range(0, 2);
            }

            randomNum = 0; // 테스트용, 나중에 주석 해제
            switch (randomNum)
            {
                case 0:
                    // 중간 크기의 발사체를 몸 중앙에서
                    yield return enemy.StartCoroutine(CoSpawnMediumProjectiles());
                    break;
                case 1:
                    // 큰 크기의 발사체를 오른쪽 혹은 왼쪽 side에서 생성
                    yield return enemy.StartCoroutine(CoSpawnLargeProjectiles());
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
        anim.SetBool("IsOnAttack", true); // 공격 애니메이션 시작    
        yield return beforeMediumProjectileWait; // 중간 크기 발사체 생성 전 대기

        // 23, 24, 25, 26, 27, 28을 랜덤한 순서로 갖는 배열 생성
        int[] projectileNums = { 23, 24, 25, 26, 27, 28 };
        for (int i = projectileNums.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = projectileNums[i];
            projectileNums[i] = projectileNums[j];
            projectileNums[j] = temp;
        }

        if (projectileNums.Length != mediumProjectileCount)
        {
            Debug.LogError("mediumProjectileCount와 projectileNums크기가 다릅니다. 배열 크기를 조정하세요.");
            yield break; // 배열 크기가 부족하면 코루틴 종료
        }

        // mediumProjectileCount의 크기만큼 반복하여 중간 크기의 발사체 생성
        for (int i = 0; i < mediumProjectileCount; i++)
        {

            yield return enemy.StartCoroutine(CoSpawnMediumProjectilesOnce(projectileNums[i]));
            yield return mediumProjectileWait; // 다음 발사체 생성까지 대기
        }

        anim.SetBool("IsOnAttack", false);// 공격 애니메이션 끝
    }

    IEnumerator CoSpawnMediumProjectilesOnce(int projectileCount)
    {
        // 발사체 여러개를 동시에 생성
        // 각각의 발사체가 360도 방향으로 균등한 각도로 날아가도록 설정
        float randomAngle = Random.Range(0f, 360f);
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = Mathf.Lerp(0f, 360f, (float)i / (projectileCount - 1)) + randomAngle;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.left; // 왼쪽 방향과 곱해서 Vector2로 변경
            GameObject proj = Instantiate(mediumProjectilePrefab, enemy.firePoint.position, Quaternion.Euler(0, 0, angle + 180));
            proj.GetComponent<Rigidbody2D>().linearVelocity = direction * mediumProjectileSpeed;
        }
        SoundManager.Instance.PlaySFX("BlueDragonShootProjectile");
        yield return null; // 프레임 대기
    }

    IEnumerator CoSpawnLargeProjectiles()
    {
        for (int i = 0; i < largeProjectileCount; i++)
        {
            yield return enemy.StartCoroutine(CoSpawnLargeProjectilesOnce());
            yield return largeProjectileWait; // 다음 발사체 생성까지 대기
        }
        yield return afterLargeProjectileWait; // 큰 크기 발사체 생성 후 대기
    }

    IEnumerator CoSpawnLargeProjectilesOnce()
    {
        // min_right_side_inde와 max_right_side_index 사이의 랜덤한 인덱스를 largeProjectileNumPerSpawn개수만큼 선택
        int[] randomYPositions = new int[largeProjectileNumPerSpawn];
        for (int i = 0; i < largeProjectileNumPerSpawn; i++)
        {
            int randomYPos;
            do
            {
                randomYPos = Random.Range((int)spawnMissleMin.y, (int)spawnMissileMax.y + 1);
            } while (System.Array.IndexOf(randomYPositions, randomYPos) != -1); // 중복 방지
            randomYPositions[i] = randomYPos;
        }

        float Xpos = isOnRight ? spawnMissileMax.x : spawnMissleMin.x; // 오른쪽에서 생성할지 왼쪽에서 생성할지 결정 
        // 큰 크기의 발사체들을 랜덤한 인덱스에서 생성
        for (int i = 0; i < largeProjectileNumPerSpawn; i++)
        {
            Vector3 spawnPos = new Vector3(Xpos, randomYPositions[i], 0);
            Vector3 randomPosition = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-1.5f, 1.5f), 0); // 약간의 랜덤 위치 조정
            Quaternion rotate = isOnRight ? Quaternion.Euler(0, 0, 180) : Quaternion.Euler(0, 0, 0); // 오른쪽에서 생성할 때는 180도, 왼쪽에서 생성할 때는 0도 회전
            Vector2 velocityDirection = isOnRight ? Vector2.left : Vector2.right; // 오른쪽에서 생성할 때는 왼쪽 방향, 왼쪽에서 생성할 때는 오른쪽 방향
            GameObject proj = Instantiate(largeProjectilePrefab, spawnPos+ randomPosition, rotate);
            proj.GetComponent<Rigidbody2D>().linearVelocity = velocityDirection * largeProjectileSpeed;

        }
        //SoundManager.Instance.PlaySFX("BlueDragonShootProjectile");
        yield return null; // 프레임 대기

    }


    IEnumerator CoDash()
    {
        dashActivateCount++; // 대쉬 패턴을 실행한 횟수 증가
        anim.SetBool("IsOnFast", true); // 대쉬 애니메이션 시작
        Vector2 backDirection = isOnRight ? Vector2.right : Vector2.left; // 대쉬 하기 전 살짝 뒤로 이동하는 방향
        enemy.rb.linearVelocity = backDirection * enemy.speed * 0.5f; // 대쉬 하기 전 살짝 뒤로 이동
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
