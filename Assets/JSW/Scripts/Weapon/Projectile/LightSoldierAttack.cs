using UnityEngine;

public class LightSoldierAttack : ProjectileBase
{
    public bool isGain1ManaPerHit;
    public LightSoldier lightSoldier;

    protected override void Start()
    {
    }

    protected override void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        transform.up = Vector2.right;
    }

    public void SetInit(Vector2 dir, int damageNum, float speedNum, float lifetimeNum, float scaleNum, LightSoldier lightSoldierOwner, bool isGain1ManaPerHitresult)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        lifetime = lifetimeNum;
        transform.localScale = Vector3.one * scaleNum;
        isGain1ManaPerHit = isGain1ManaPerHitresult;
        lightSoldier = lightSoldierOwner;

        Destroy(gameObject, lifetime);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                if (isGain1ManaPerHit) lightSoldier.GainMp();
            }
            DestroyProjectile(gameObject);
        }
    }

    public override void DestroyProjectile(GameObject projectile)
    {
    }
}
