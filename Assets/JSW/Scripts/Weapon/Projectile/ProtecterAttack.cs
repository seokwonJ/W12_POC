using UnityEngine;

public class ProtecterAttack : ProjectileBase
{
    public override void DestroyProjectile(GameObject projectile)
    {
        Destroy(projectile);
    }

    public void SetInit(Vector2 dir, int damageNum, float speedNum, float scaleNum)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        transform.localScale = Vector3.one * scaleNum;
    }
}
