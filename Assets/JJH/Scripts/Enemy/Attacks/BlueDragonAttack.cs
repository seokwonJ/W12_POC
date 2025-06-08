using System.Collections;
using UnityEngine;
[CreateAssetMenu(menuName = "Patterns/Attack/BlueDragonAttack")]
public class BlueDragonAttack : ScriptableObject, IAttackPattern
{
    private Enemy enemy;

    [Header ("공격 패턴 관련")]
    private float prevSpawnMoveTime = 2f;
    private float attackCooldown = 2f; // 공격 패턴 사이 간격
    private WaitForSeconds attackWait;
    private float easyTime = 25f; // 공격 패턴 사이 간격이 늘어나는 간격

    [Header("중간 크기의 발사체 입에서 발사하는 패턴 관련")]
    public GameObject mediumProjectilePrefab; // 중간 크기 발사체 프리팹
    private int mediumProjectileCount = 4; // 중간 크기 발사 횟수
    private float mediumProjectileSpeed = 9f; // 중간 크기 발사체 속도
    private float mediumProjectileCoolDown = 0.5f; // 중간 크기 발사체 생성 간격
    private WaitForSeconds mediumProjectileWait;

    [Header("큰 크기의 발사체 오른쪽에서 발사하는 패턴 관련")]
    public GameObject largeProjectilePrefab; // 큰 크기 발사체 프리팹
    private int largeProjectileCount = 5; // 큰 크기 발사 횟수
    private int largeProjectileNumPerSpawn = 5; // 한 번에 생성되는 큰 크기 발사체 수
    private float largeProjectileSpeed = 14f; // 큰 크기 발사체 속도
    private float largeProjectileCoolDown = 0.3f; // 큰 크기 발사체 생성 간격
    private WaitForSeconds largeProjectileWait;
    private float afterLargePjojectileCoolDown = 1.5f; // 큰 크기 발사체 생성 후 대기 시간
    private WaitForSeconds afterLargeProjectileWait;

    private int min_right_side_index; // 오른쪽에서 생성되는 발사체의 최소 인덱스
    private int max_right_side_index; // 오른쪽에서 생성되는 발사체의 최대 인덱스

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        attackWait = new WaitForSeconds(attackCooldown);
        mediumProjectileWait = new WaitForSeconds(mediumProjectileCoolDown);
        largeProjectileWait = new WaitForSeconds(largeProjectileCoolDown);
        afterLargeProjectileWait = new WaitForSeconds(afterLargePjojectileCoolDown);

        min_right_side_index = ControlField.MIN_RIGHT_SIDE_INDEX;
        max_right_side_index = ControlField.MAX_RIGHT_SIDE_INDEX;
    }

    public void Attack()
    {
        enemy.StartCoroutine(CoAttackPattern());
        enemy.StartCoroutine(CoTimer());
    }

    IEnumerator CoTimer()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= easyTime)
            {
                attackCooldown += 0.7f; // easyTime마다 공격 패턴 사이 간격 증가
                Debug.Log($"Attack cooldown이 {attackCooldown}초로 증가해 더 쉬워짐");
                timer = 0; // 타이머 초기화
            }
            yield return null;
        }
    }

    IEnumerator CoAttackPattern()
    {
        yield return new WaitForSeconds(prevSpawnMoveTime);
        while (true)
        {
            if (enemy == null || !enemy.enabled)
            {
                yield break; // 적이 죽었거나 존재하지 않으면 코루틴 종료
            }

            float randomNum = Random.Range(0, 2);
            switch (randomNum)
            {
                case 0:
                    // 중간 크기의 발사체를 입에서 생성
                    yield return enemy.StartCoroutine(CoSpawnMediumProjectiles());
                    break;
                case 1:
                    // 큰 크기의 발사체를 오른쪽 side에서 생성
                    yield return enemy.StartCoroutine(CoSpawnLargeProjectiles());
                    break;
            }
            yield return attackWait;
        }
    }

    IEnumerator CoSpawnMediumProjectiles()
    {

        // 4,5,6,7을 랜덤한 순서로 갖는 배열 생성
        int[] projectileNums = { 4, 5, 6, 7 };
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
    }

    IEnumerator CoSpawnMediumProjectilesOnce(int projectileCount)
    {
        // 발사체를 3-5개 랜덤한 수를 생성
        // 각각의 발사체가 왼쪽 위 방향부터 왼쪽 아래 방향까지 균등한 각도로 날아가도록 설정
        for (int i = 0; i < projectileCount; i++)
        {
            float angle = Mathf.Lerp(-45f, 45f, (float)i / (projectileCount - 1)); // 45도에서 135도 사이의 균등한 각도
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.left; // 위쪽 방향과 곱해서 Vector2로 변경
            //
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
        int[] randomIndices = new int[largeProjectileNumPerSpawn];
        for (int i = 0; i < largeProjectileNumPerSpawn; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(min_right_side_index, max_right_side_index + 1);
            } while (System.Array.IndexOf(randomIndices, randomIndex) != -1); // 중복 방지
            randomIndices[i] = randomIndex;
        }

        // 큰 크기의 발사체들을 랜덤한 인덱스에서 생성
        for (int i =0; i < largeProjectileNumPerSpawn; i++)
        {

            Transform spawnPoint = Managers.Stage.controlField.spawnPoints[randomIndices[i]];
            Vector3 randomPosition = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0); // 약간의 랜덤 위치 조정
            GameObject proj = Instantiate(largeProjectilePrefab, spawnPoint.position + randomPosition, Quaternion.Euler(0, 0, 180));
            proj.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * largeProjectileSpeed; 

        }
        //SoundManager.Instance.PlaySFX("BlueDragonShootProjectile");
        yield return null; // 프레임 대기

    }

}
