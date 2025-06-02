using UnityEngine;

public class LeviAttack : ProjectileBase
{
    private float knockbackPower = 1;

    protected override void Start()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null) enemy.TakeDamage(damage);
        }
    }

    public  void SetInit(Vector2 dir, int damageNum, float speedNum, float lifeTimeNum)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        Destroy(gameObject, lifeTimeNum);
    }

    public override void DestroyProjectile(GameObject projectile)
    {
    }
}
