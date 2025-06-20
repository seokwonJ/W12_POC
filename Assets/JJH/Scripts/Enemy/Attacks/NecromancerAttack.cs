using System.Collections;
using System.Collections.Generic;
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
    private WaitForSeconds mediumProjectileSoundWait = new WaitForSeconds(0.36f); // 중간 크기 발사체 사운드 간격

    [Header("해골 발사체 발사하는 패턴 관련")]
    public EnemyWaveSO skullProjectileWave; // 해골 발사 웨이브
    private int skullProjectileCount = 2; // 해골 발사 횟수
    private WaitForSeconds beforeSkullProjectileWait = new WaitForSeconds(1f);
    private WaitForSeconds afterSkullProjectileWait = new WaitForSeconds(8f);

    [Header("폭발 패턴 관련")]
    public GameObject explosionPrefab; // 폭발 프리팹
    private int explosionCount = 3; // 폭발 발사 횟수
    private WaitForSeconds explosionWait = new WaitForSeconds(1.5f);
    List<List<EExplosionPosType>> explsionTypeList;

    public void Init(Enemy enemy)
    {
        Debug.Log(Time.deltaTime);
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

        explsionTypeList = new List<List<EExplosionPosType>>()
        {
            new List<EExplosionPosType>() { EExplosionPosType.Center, EExplosionPosType.Up, EExplosionPosType.Left },
            new List<EExplosionPosType>() { EExplosionPosType.Center, EExplosionPosType.Up, EExplosionPosType.Right },
            new List<EExplosionPosType>() { EExplosionPosType.Center, EExplosionPosType.Down, EExplosionPosType.Left },
            new List<EExplosionPosType>() { EExplosionPosType.Center, EExplosionPosType.Down, EExplosionPosType.Right },
            new List<EExplosionPosType>() { EExplosionPosType.LittleUp, EExplosionPosType.LittleDown, EExplosionPosType.Left},
            new List<EExplosionPosType>() { EExplosionPosType.LittleUp, EExplosionPosType.LittleDown, EExplosionPosType.Right},
            new List<EExplosionPosType>() { EExplosionPosType.Up, EExplosionPosType.Down, EExplosionPosType.Left, EExplosionPosType.Right },
        };
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

            int randomNum = Random.Range(0, 3);

            switch (randomNum)
            {
                case 0:
                    // 중간 크기의 발사체 지팡이에서 발사하는 패턴 관련
                    yield return enemy.StartCoroutine(CoSpawnMediumProjectiles());
                    break;
                case 1:
                    // 해골 미사일 발사체를 소환
                    anim.SetTrigger("Skull"); // 해골 발사 애니메이션 시작
                    yield return beforeSkullProjectileWait;
                    SoundManager.Instance.PlaySFX("NecromancerSkull");
                    enemySpawner.SpawnEnemys(skullProjectileWave, skullProjectileCount);
                    yield return afterSkullProjectileWait;
                    break;
                case 2:
                    // 폭발 패턴
                    yield return enemy.StartCoroutine(CoExplosion());
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
        enemy.StartCoroutine(CoPlayMediumProejctileSFX()); // 발사체 사운드 재생
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

    IEnumerator CoPlayMediumProejctileSFX()
    {
        for (int i = 0; i < 5; i++)
        {
            SoundManager.Instance.PlaySFX("BlueDragonShootProjectile");
            yield return mediumProjectileSoundWait; // 발사체 간격 대기
        }
    }

    IEnumerator CoExplosion()
    {
        for (int i = 0; i < explosionCount; i++)
        {
            Vector3 explosionDefaultPosition = player.transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f); // 플레이어 주변 랜덤 위치 
            int randomIndex = Random.Range(0, explsionTypeList.Count);
            List<EExplosionPosType> explosionPosTypes = explsionTypeList[randomIndex];
            for (int j = 0; j < explosionPosTypes.Count; j++)
            {
                Vector3 explosionPosition = explosionDefaultPosition;
                switch (explosionPosTypes[j])
                {
                    case EExplosionPosType.Center:
                        break;
                    case EExplosionPosType.Up:
                        explosionPosition += Vector3.up * 4f; 
                        break;
                    case EExplosionPosType.Down:
                        explosionPosition += Vector3.down * 4f; 
                        break;
                    case EExplosionPosType.Left:
                        explosionPosition += Vector3.left * 4f;
                        break;
                    case EExplosionPosType.Right:
                        explosionPosition += Vector3.right * 4f;
                        break;
                    case EExplosionPosType.LittleUp:
                        explosionPosition += Vector3.up * 2f; 
                        break;
                    case EExplosionPosType.LittleDown:
                        explosionPosition += Vector3.down * 2f; 
                        break;
                }
                 Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
            }

            yield return explosionWait; // 폭발 사이 간격 대기
        }
    }
}

public enum EExplosionPosType // 플레이어를 중심으로 한 상대적 위치
{
    Center,
    Up,
    Down,
    Left,
    Right,
    LittleUp,
    LittleDown,
}