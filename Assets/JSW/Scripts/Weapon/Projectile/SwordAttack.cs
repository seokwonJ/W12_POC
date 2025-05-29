using UnityEngine;

public class SwordAttack : ProjectileBase
{
    protected override void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        transform.up = Vector2.right;
    }

    public override void SetInit(Vector2 dir, int damageNum, float speedNum)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public override void DestroyProjectile(GameObject projectile)
    {
    }
}
