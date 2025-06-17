using UnityEngine;

public class SniperAttack : ProjectileBase
{
    public TrailRenderer trailRenderer;

    protected override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }


    public override void SetInit(Vector2 dir, float speedNum, float scaleNum)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        speed = speedNum;
        trailRenderer.widthMultiplier = trailRenderer.widthMultiplier * scaleNum;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {

    }

    public override void DestroyProjectile(GameObject projectile)
    {
    }
}
