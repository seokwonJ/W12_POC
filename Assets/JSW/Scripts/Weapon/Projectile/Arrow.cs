using UnityEngine;

public class Arrow : ProjectileBase
{
    private Archer _characterArcher;
    private bool _isSkill;
    private bool _isDieInstantly;
    private bool _isSameEnemyDamage;
    private bool _isCritical;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;

            if (!_isSkill)
            {
                if (_isSameEnemyDamage)
                {
                    float bonusDamage = _characterArcher.SameEnemyTotalDamage(enemy);
                    damage *= bonusDamage;
                }

                Enemy enemyComponent = enemy.GetComponent<Enemy>();

                if (_isDieInstantly && !enemyComponent.isBoss) enemy.GetComponent<EnemyHP>().TakeDamage(9999, ECharacterType.Archer);
                else enemy.GetComponent<EnemyHP>().TakeDamage((int)(damage), ECharacterType.Archer);

            }
            else
            {
                int hitCount = 0;
                if (_characterArcher.hitEnemies.TryGetValue(enemy, out hitCount))
                {
                    hitCount++;
                    _characterArcher.hitEnemies[enemy] = hitCount;
                }
                else
                {
                    _characterArcher.hitEnemies[enemy] = 1;
                    hitCount = 1;
                }

                float nowdamage = Mathf.Max(2f, damage - 2f * (hitCount - 1));

                Enemy enemyComponent = enemy.GetComponent<Enemy>();

                if (_isDieInstantly && !enemyComponent.isBoss) enemy.GetComponent<EnemyHP>().TakeDamage(9999, ECharacterType.Archer);
                else enemy.GetComponent<EnemyHP>().TakeDamage((int)nowdamage, ECharacterType.Archer);
            }

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

            Destroy(gameObject);
        }
    }

    public void SetInit(Vector2 dir, float damageNum, float speedNum, float scaleNum, float knockbackPowerNum, bool isCritical, Archer archer, bool isSkill, bool isUpgradeDieInstantly, bool isUpgradeSameEnemyDamage)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        damage = damageNum;
        speed = speedNum;
        knockbackPower = knockbackPowerNum;
        transform.localScale = Vector3.one * scaleNum;
        _characterArcher = archer;
        this._isSkill = isSkill;
        _isDieInstantly = isUpgradeDieInstantly;
        _isSameEnemyDamage = isUpgradeSameEnemyDamage;
        _isCritical = isCritical;
    }

}
