using UnityEngine;

public class ProtecterAttack : ProjectileBase
{
    private bool isProjectileCancelsProjectiles;

    public override void DestroyProjectile(GameObject projectile)
    {
        Destroy(projectile);
    }

    public void SetInit(Vector2 dir, int damageNum, float speedNum, float scaleNum, bool isProjectileCancelsProjectilesResult)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        transform.localScale = Vector3.one * scaleNum;
        isProjectileCancelsProjectiles = isProjectileCancelsProjectilesResult;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (isProjectileCancelsProjectiles)
        {
            if (other.tag == "EnemyAttack")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
