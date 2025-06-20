﻿using UnityEngine;

public class LeviAttack : ProjectileBase
{
    private float _attackPerDamageMinus;
    private bool _isCritical;


    protected override void Start()
    {
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null) enemy.TakeDamage((int)damage, ECharacterType.Levi);

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

            if (damage - _attackPerDamageMinus > 5) damage -= _attackPerDamageMinus;
            else damage = 5;
        }
    }

    public void SetInit(Vector2 dir, float damageNum, float speedNum, float sizeNum, float knockbackPowerNum, bool isCritical, float lifeTimeNum, float attackPerDamageMinus)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        knockbackPower = knockbackPowerNum;
        _isCritical = isCritical;

        _attackPerDamageMinus = attackPerDamageMinus;
        Destroy(gameObject, lifeTimeNum);
    }

    public override void DestroyProjectile(GameObject projectile)
    {
    }
}
