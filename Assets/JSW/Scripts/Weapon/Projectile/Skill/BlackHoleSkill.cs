using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BlackHoleSkill : MonoBehaviour
{
    public GameObject explosionEffect;

    private float _duration = 5f;
    private float _pullForce = 5f;
    private float _pullInterval = 0.1f;
    private float _damage = 10;           // 블랙홀 끝날 때 줄 데미지
    private bool _isUpgradeSkillSizeDownExplosion;
    private float _explosionDamage;
    private bool _isUpgradeSkillEnemyDenfenseDown;
    private float _skillEnemyDenfenseDownPercent;
    private float _skillEnemyDenfenseDownDuration;

    private List<Transform> enemiesInRange = new List<Transform>();
    private Coroutine pullCoroutine;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        pullCoroutine = StartCoroutine(PullLoop());
        StartCoroutine(DestroyAfterDuration());
    }

    public void SetInit(float scaleNum, float damageNum, float pullForce, float duration, float pullInterval, bool isUpgradeSkillSizeDownExplosion, float explosionDamage, bool isUpgradeSkillEnemyDenfenseDown, float skillEnemyDenfenseDownPercent, float skillEnemyDenfenseDownDuration)
    {
        transform.localScale = Vector3.one * scaleNum;
        _damage = damageNum;
        _duration = duration;
        _pullForce = pullForce;
        _pullInterval = pullInterval;
        _isUpgradeSkillSizeDownExplosion = isUpgradeSkillSizeDownExplosion;
        _explosionDamage = explosionDamage;
        _isUpgradeSkillEnemyDenfenseDown = isUpgradeSkillEnemyDenfenseDown;
        _skillEnemyDenfenseDownPercent = skillEnemyDenfenseDownPercent;
        _skillEnemyDenfenseDownDuration = skillEnemyDenfenseDownDuration;

        if (_isUpgradeSkillSizeDownExplosion) transform.localScale /= 2;
    }

    private IEnumerator DestroyAfterDuration()
    {
        int count = 0;

        while (true)
        {
            count += 1;
            SoundManager.Instance.PlaySFX("BlackHoleSkillDuration");

            yield return new WaitForSeconds(1);
            if (count / _duration >= 1) break;
        }



        if (_isUpgradeSkillSizeDownExplosion)    // 폭발
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            explosion.transform.localScale = transform.localScale + Vector3.one * 2;
            SoundManager.Instance.PlaySFX("BlackHoleSkillExplosion");

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosion.transform.localScale.magnitude);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.GetComponent<Enemy>();
                    EnemyHP otherEnemyHP = hit.GetComponent<EnemyHP>();

                    otherEnemyHP.TakeDamage((int)_explosionDamage, ECharacterType.Magician);

                    Vector3 knockbackDirection = hit.transform.position - transform.position;
                    if (enemy != null && otherEnemyHP.enemyHP > 0)
                    {
                        enemy.ApplyKnockback(knockbackDirection, _pullForce);
                    }

                    print("데미지 주나요?! + " + transform.localScale.magnitude);
                }
            }

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
            yield return new WaitForSeconds(_pullInterval);
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
                enemyKnockback.ApplyKnockback(dirToCenter, _pullForce);
            }
        }
    }

    private void DealDamage()
    {
        foreach (var enemy in new List<Transform>(enemiesInRange))
        {
            if (enemy == null) continue;

            if (_isUpgradeSkillEnemyDenfenseDown)
            {
                 enemy.GetComponent<EnemyHP>().ReduceArmor((int)_skillEnemyDenfenseDownPercent, _skillEnemyDenfenseDownDuration);
            }

            EnemyHP hp = enemy.GetComponent<EnemyHP>();

            if (hp != null)
            {
                hp.TakeDamage((int)_damage, ECharacterType.BlackHole);
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