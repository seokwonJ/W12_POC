using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSkill : MonoBehaviour
{
    public float duration = 5f;
    public float pullForce = 5f;
    public float pullInterval = 0.1f;
    public float damage = 10; // 블랙홀 끝날 때 줄 데미지

    private List<Transform> enemiesInRange = new List<Transform>();
    private Coroutine pullCoroutine;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        pullCoroutine = StartCoroutine(PullLoop());
        StartCoroutine(DestroyAfterDuration());
    }

    public void SetInit(float scaleNum, float damageNum)
    {
        transform.localScale = Vector3.one * scaleNum;
        damage = damageNum;
    }

    private IEnumerator DestroyAfterDuration()
    {
        int count = 0;

        while(true)
        {
            count += 1;
            SoundManager.Instance.PlaySFX("BlackHoleSkillDuration");
            yield return new WaitForSeconds(1);
            if (count / duration >= 1) break;
        }

        if (pullCoroutine != null) StopCoroutine(pullCoroutine);
        _animator.SetBool("isBlackHole", true);
    }

    private IEnumerator PullLoop()
    {
        while (true)
        {
            PullEnemies();
            DealDamage();
            yield return new WaitForSeconds(pullInterval);
        }
    }

    private void PullEnemies()
    {
        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            var enemy = enemiesInRange[i];

            if (enemy == null) continue;

            Vector2 dirToCenter = (transform.position - enemy.position).normalized;
            Enemy enemyKnockback = enemy.GetComponent<Enemy>();
            if (enemyKnockback != null)
            {
                enemyKnockback.ApplyKnockback(dirToCenter, pullForce);
            }
        }
    }

    private void DealDamage()
    {
        foreach (var enemy in new List<Transform>(enemiesInRange))
        {
            if (enemy == null) continue;

            EnemyHP hp = enemy.GetComponent<EnemyHP>();
            if (hp != null)
            {
                hp.TakeDamage((int)damage, ECharacterType.BlackHole);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !enemiesInRange.Contains(other.transform))
        {
            enemiesInRange.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }
}