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

    private bool _isUpgradeAttackEnemyDefenseDown;
    private float _attackEnemyDefenseDownPercnet;
    private float _attackEnemyDefenseDownDuration;

    private bool _isUpgradeAttackEnemySpeedDown;
    private float _attackEnemySpeedDownPercnet;
    private float _attackEnemySpeedDownDuration;
    private bool _isCritical;

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

    public void SetInit(Vector2 dir, float damageNum, float speedNum, float scaleNum, float knockbackPowerNum, bool isCritical, Transform ownerTransform, float totalTravelTimeNum, Fox characterFox, bool isSkill, bool isUpgradeAttackEnemyDefenseDown, float attackEnemyDefenseDownPercnet, float attackEnemyDefenseDownDuration, bool isUpgradeAttackEnemySpeedDown, float attackEnemySpeedDownPercnet, float attackEnemySpeedDownDuration)
    {
        owner = ownerTransform;

        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        damage = damageNum;
        speed = speedNum;
        this.knockbackPower = knockbackPowerNum;
        transform.localScale = Vector3.one * scaleNum;
        totalTravelTime = totalTravelTimeNum;
        _characterFox = characterFox;
        this.isSkill = isSkill;

        _isCritical = isCritical;

        _isUpgradeAttackEnemyDefenseDown = isUpgradeAttackEnemyDefenseDown;
        _attackEnemyDefenseDownPercnet = attackEnemyDefenseDownPercnet;
        _attackEnemyDefenseDownDuration = attackEnemyDefenseDownDuration;

        _isUpgradeAttackEnemySpeedDown = isUpgradeAttackEnemySpeedDown;
        _attackEnemySpeedDownPercnet = attackEnemySpeedDownPercnet;
        _attackEnemySpeedDownDuration = attackEnemySpeedDownDuration;
        
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {

            GameObject enemy = other.gameObject;

            if (_isUpgradeAttackEnemyDefenseDown) enemy.GetComponent<EnemyHP>().ReduceArmor((int)_attackEnemyDefenseDownPercnet, _attackEnemyDefenseDownDuration);
            if (_isUpgradeAttackEnemySpeedDown) enemy.GetComponent<Enemy>().ApplySlow((int)_attackEnemySpeedDownPercnet, _attackEnemySpeedDownDuration);

            if (!isReturning)
            {
                // 넉백
                Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

                Enemy enemyComponenet = enemy.GetComponent<Enemy>();
                if (enemyComponenet != null)
                {
                    enemyComponenet.ApplyKnockback(knockbackDirection, knockbackPower);
                }
            }

            if (!isSkill)
            {
                enemy.GetComponent<EnemyHP>().TakeDamage((int)damage, ECharacterType.Fox);

                if (_isCritical)
                {
                    Vector2 contactPoint = other.ClosestPoint(transform.position);
                    Instantiate(Managers.PlayerControl.NowPlayer.GetComponent<PlayerEffects>().criticalEffect, contactPoint, Quaternion.identity);
                }
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

        }
    }
}
