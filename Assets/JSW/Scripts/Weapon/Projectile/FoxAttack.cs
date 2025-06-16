using UnityEngine;

public class FoxAttack : ProjectileBase
{
    private bool isReturning = false;
    private Transform owner;       // 되돌아올 대상

    private float travelTime = 0f;
    private float returnTime = 0f;
    private float totalTravelTime = 1f; // 왕복 시간 조절
    private float minSpeed = 3f;
    private float goMaxSpeed = 20f;
    private float returnMaxSpeed = 60f;
    private Fox _characterFox;

    public bool isSkill;
    public bool isReturnDamageScalesWithHitCount;
    public int fowardCount;
    public bool isOrbPausesBeforeReturning;


    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (!isReturning)
        {
            travelTime += Time.deltaTime;
            float t = Mathf.Clamp01(travelTime / totalTravelTime);

            // 감속 커브 (빠르게 시작해서 점점 느려짐)
            speed = Mathf.Lerp(goMaxSpeed, minSpeed, t * t);

            
            if (speed <= minSpeed + 0.01f) // 여유 범위 줘서 깔끔하게 전환
            {
                 isReturning = true;
                 returnTime = 0f;
            }
        }
        else if (owner != null)
        {
            returnTime += Time.deltaTime;

            if (isOrbPausesBeforeReturning && returnTime < 1) return;

            float t = Mathf.Clamp01(returnTime / totalTravelTime);
            speed = Mathf.Lerp(minSpeed, returnMaxSpeed, t *  0.5f);

            direction = ((Vector2)(owner.position - transform.position)).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            float arriveThreshold = 0.3f;
            if (Vector2.Distance(transform.position, owner.position) <= arriveThreshold)
            {
                Destroy(gameObject);
            }
        }

        base.Update();
    }

    public void SetInit(Vector2 dir, float damageNum, float speedNum, float scaleNum, Transform ownerTransform, bool isReturnDamageScalesWithHitCountResult, float totalTravelTimeNum, bool isOrbPausesBeforeReturningResult, Fox characterFox, bool isSkill)
    {
        owner = ownerTransform;

        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        damage = damageNum;
        speed = speedNum;
        transform.localScale = Vector3.one * scaleNum;
        isReturnDamageScalesWithHitCount = isReturnDamageScalesWithHitCountResult;
        totalTravelTime = totalTravelTimeNum;
        isOrbPausesBeforeReturning = isOrbPausesBeforeReturningResult;
        _characterFox = characterFox;
        this.isSkill = isSkill;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {

            GameObject enemy = other.gameObject;

            if (!isSkill)
            {
                enemy.GetComponent<EnemyHP>().TakeDamage((int)damage, ECharacterType.Fox);
                return;
            }

            int hitCount = 0;
            if (_characterFox.hitEnemies.TryGetValue(enemy, out hitCount))
            {
                hitCount++;
                _characterFox.hitEnemies[enemy] = hitCount;
            }
            else
            {
                _characterFox.hitEnemies[enemy] = 1;
                hitCount = 1;
            }

            float nowdamage = Mathf.Max(2f, damage - 5f * (hitCount - 1));
            enemy.GetComponent<EnemyHP>().TakeDamage((int)nowdamage, ECharacterType.Fox);


            //var enemy = other.GetComponent<EnemyHP>();
            //if (enemy != null)
            //{
            //    enemy.TakeDamage(damage);

            //    //if (isReturnDamageScalesWithHitCount)
            //    //{
            //    //    enemy.TakeDamage(fowardCount * 2);
            //    //}
            //}


            //// 아리 Q처럼, 돌아오는 중에도 데미지 줄 수 있도록
            //// 단, 파괴는 하지 않음 — projectile 살아있어야 하니까
            //if (!isReturning)
            //{
            //    fowardCount += 1;
            //}
        }
    }
}
