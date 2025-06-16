using UnityEngine;

public class Kunai : ProjectileBase
{
    protected override void Update()
    {
        base.Update(); // 이동
        transform.right = direction;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null)
                enemy.TakeDamage((int)damage, ECharacterType.Ninja);

            DestroyProjectile(gameObject);
        }
    }
}
