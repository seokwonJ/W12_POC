using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleAttack : ProjectileBase
{
    private List<Transform> enemiesInRange = new List<Transform>();
    public float pullDelay = 0.3f;
    public float pullForce = 10f;

    public void SetInit(Vector2 dir, int damageNum, float speedNum, float scaleNum)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        damage = damageNum;
        speed = speedNum;
        transform.localScale = Vector3.one * scaleNum;

        // ��Ȧ Ȱ��ȭ ��� Ÿ�̸� ����
        StartCoroutine(DelayedPull());
    }

    protected override void Update()
    {
        // ��Ȧ ��ü�� �������� �ʰų� õõ�� �̵��ص� ����
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(other.transform))
            {
                enemiesInRange.Add(other.transform);
            }
        }
    }

    private IEnumerator DelayedPull()
    {
        yield return new WaitForSeconds(pullDelay);
        PullEnemies();
    }

    private void PullEnemies()
    {
        foreach (var enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                Vector2 dirToBlackHole = (transform.position - enemy.position).normalized;
                Enemy enemyKnockback = enemy.GetComponent<Enemy>();
                if (enemyKnockback != null)
                {
                    enemyKnockback.ApplyKnockback(dirToBlackHole, pullForce);
                }

                // ������ ����
                EnemyHP hp = enemy.GetComponent<EnemyHP>();
                if (hp != null)
                {
                    hp.TakeDamage(damage, ECharacterType.Magician);
                }
            }
        }
    }

    public override void DestroyProjectile(GameObject projectile)
    {
        Destroy(projectile);
    }
}