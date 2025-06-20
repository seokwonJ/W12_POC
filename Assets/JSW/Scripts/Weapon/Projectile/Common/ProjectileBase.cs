﻿using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public float damage = 10;
    public float knockbackPower = 1;

    protected Vector2 direction;

    protected virtual void Start()
    {
        Destroy(gameObject, lifetime);
    }

    protected virtual void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public virtual void SetInit(Vector2 dir, float damageNum, float speedNum)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        damage = damageNum;
        speed = speedNum;
    }


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null)
                enemy.TakeDamage((int)damage, ECharacterType.None);

            DestroyProjectile(gameObject);
        }
    }

    public virtual void DestroyProjectile(GameObject projectile)
    {
        Destroy(projectile);
    }
}
