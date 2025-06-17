using UnityEngine;

public class Arrow : ProjectileBase
{
    private Archer _characterArcher;
    private bool isSkill;
    private bool isDieInstantly;
    protected override void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;

            if (!isSkill)
            {
                enemy.GetComponent<EnemyHP>().TakeDamage((int)damage, ECharacterType.Archer);
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

                // 보스 불리언 체크 해줘야함!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                if (isDieInstantly) enemy.GetComponent<EnemyHP>().TakeDamage(9999, ECharacterType.Archer);
                else enemy.GetComponent<EnemyHP>().TakeDamage((int)nowdamage, ECharacterType.Archer);

            }

            // 넉백
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

            Enemy enemyComponenet = enemy.GetComponent<Enemy>();
            if (enemyComponenet != null)
            {
                enemyComponenet.ApplyKnockback(knockbackDirection, knockbackPower);
            }

            Destroy(gameObject);
        }
    }

    public void SetInit(Vector2 dir, float damageNum, float speedNum, float knockbackPowerNum, float scaleNum, Archer archer, bool isSkill, bool isUpgradeDieInstantly)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        damage = damageNum;
        speed = speedNum;
        knockbackPower = knockbackPowerNum;
        transform.localScale = Vector3.one * scaleNum;
        _characterArcher = archer;
        this.isSkill = isSkill;
    }

}
