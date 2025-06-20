using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PriestAttack : ProjectileBase
{
    public Transform target;
    private bool _isUpgradeAttackEnemyDefenseDown;
    private float _attackEnemyDefenseDownPercent;
    private float _attackEnemyDefenseDownDuration;
    private bool _isCritical;
    protected override void Update()
    {
        if (target != null)
        {
            // 타겟 방향 계산
            Vector2 dir = ((Vector2)target.position - (Vector2)transform.position).normalized;
            direction = dir;

            // 각도 계산해서 회전
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            if (Vector2.Distance(target.position, transform.position) < 0.1f && target.GetComponent<EnemyHP>().enemyHP <= 0) target = null;   
        }

        // 이동
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    public void SetInit(Vector2 dir, float damageNum, float speedNum, float scaleNum,float knockbackPowerNum,bool isCritical, bool isUpgradeAttackEnemyDefenseDown, float attackEnemyDefenseDownPercent,float attackEnemyDefenseDownDuration, Transform target = null)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        transform.localScale = Vector3.one * scaleNum;
        knockbackPower = knockbackPowerNum;
        _isUpgradeAttackEnemyDefenseDown = isUpgradeAttackEnemyDefenseDown;
        _attackEnemyDefenseDownPercent = attackEnemyDefenseDownPercent;
        _attackEnemyDefenseDownDuration = attackEnemyDefenseDownDuration;
        _isCritical = isCritical;

        if (target != null)
        {
            this.target = target;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null)
                enemy.TakeDamage((int)damage, ECharacterType.Priest);

            if (_isUpgradeAttackEnemyDefenseDown) enemy.GetComponent<EnemyHP>().ReduceArmor((int)_attackEnemyDefenseDownPercent, _attackEnemyDefenseDownDuration);

            // 넉백
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

            Enemy enemyComponenet = enemy.GetComponent<Enemy>();
            if (enemyComponenet != null)
            {
                enemyComponenet.ApplyKnockback(knockbackDirection, knockbackPower);
            }

            if (_isCritical)
            {
                Vector2 contactPoint = other.ClosestPoint(transform.position);
                Instantiate(Managers.PlayerControl.NowPlayer.GetComponent<PlayerEffects>().criticalEffect, contactPoint, Quaternion.identity);
            }

            DestroyProjectile(gameObject);
        }
    }
}
