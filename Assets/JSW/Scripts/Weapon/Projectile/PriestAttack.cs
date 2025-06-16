using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PriestAttack : ProjectileBase
{
    public Transform target;

    protected override void Update()
    {
        if (target != null)
        {
            // 타겟 방향 계산
            Vector2 dir = ((Vector2)target.position - (Vector2)transform.position).normalized;
            direction = dir;

            // 각도 계산해서 회전
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            if (Vector2.Distance(target.position, transform.position) < 0.1f && target.GetComponent<EnemyHP>().enemyHP <= 0) target = null;   
        }

        // 이동
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    public void SetInit(Vector2 dir, float damageNum, float speedNum, float scaleNum,float knockbackPowerNum, Transform target = null)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        transform.localScale = Vector3.one * scaleNum;
        knockbackPower = knockbackPowerNum;

        if (target != null)
        {
            this.target = target;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<EnemyHP>();
            if (enemy != null)
                enemy.TakeDamage((int)damage, ECharacterType.Priest);


            // 넉백
            Vector2 knockbackDirection = (other.transform.position - transform.position).normalized;

            Enemy enemyComponenet = enemy.GetComponent<Enemy>();
            if (enemyComponenet != null)
            {
                enemyComponenet.ApplyKnockback(knockbackDirection, knockbackPower);
            }

            DestroyProjectile(gameObject);
        }
    }
}
