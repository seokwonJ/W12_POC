using UnityEngine;

public class LightSoldierAttack : ProjectileBase
{
    protected override void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        transform.up = Vector2.right;
    }

    public void SetInit(Vector2 dir, int damageNum, float speedNum, float lifetimeNum, float scaleNum)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        lifetime = lifetimeNum;
        transform.localScale = Vector3.one * scaleNum;
    }


    public override void DestroyProjectile(GameObject projectile)
    {
    }
}
