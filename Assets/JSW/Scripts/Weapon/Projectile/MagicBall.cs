using System.Collections;
using UnityEngine;

public class MagicBall : ProjectileBase
{
    public GameObject exlosionEffect;
    private bool _isCritical;

    public void SetInit(Vector2 dir, float damageNum, float speedNum, float scaleNum, float knockbackPowerNum, bool isCritical, bool isUpgradeSkillExplosionAttack, float skillExplosionAttackTime)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        transform.localScale = Vector3.one * scaleNum;
        knockbackPower = knockbackPowerNum;
        _isCritical = isCritical;

        if (isUpgradeSkillExplosionAttack) StartCoroutine(explosionAttack(skillExplosionAttackTime));
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null)
                enemy.TakeDamage((int)damage, ECharacterType.Magician);

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

    public override void DestroyProjectile(GameObject projectile)
    {
    }

    IEnumerator explosionAttack(float timer)
    {

        yield return new WaitForSeconds(timer);

        GameObject explosion = Instantiate(exlosionEffect,transform.position,Quaternion.identity);
        explosion.transform.localScale = transform.localScale + Vector3.one;

        SoundManager.Instance.PlaySFX("MagicianExplosion");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosion.transform.localScale.magnitude);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                EnemyHP otherEnemyHP = hit.GetComponent<EnemyHP>();

                otherEnemyHP.TakeDamage((int)damage, ECharacterType.Magician);

                Vector3 knockbackDirection = hit.transform.position - transform.position;
                if (enemy != null && otherEnemyHP.enemyHP > 0)
                {
                    enemy.ApplyKnockback(knockbackDirection, knockbackPower);
                }

                print("데미지 주나요?! + " + transform.localScale.magnitude);
            }
        }

        Destroy(gameObject);
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.magnitude + 1);
    }
}
