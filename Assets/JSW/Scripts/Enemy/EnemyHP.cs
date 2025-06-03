using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int enemyHP;
    private bool isDead = false;

    private Renderer renderer;
    private Collider collider;
    private WaitForSeconds flashDuration = new WaitForSeconds(0.1f);
    private float dieDelay = 1f;


    private void Awake()
    {
        //dieEffectDuration = new WaitForSeconds(dieDelay);
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();

    }

    public void TakeDamage(int hp)
    {
        if (isDead) return;

        enemyHP -= hp;
        StartCoroutine(CoDamagedEffect());

        if (enemyHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        StartCoroutine(CoDieEffect());
    }
    IEnumerator CoDamagedEffect()
    {
        renderer.material.EnableKeyword("_ISFLASHED");
        yield return flashDuration;
        renderer.material.DisableKeyword("_ISFLASHED");
    }

    IEnumerator CoDieEffect()
    {
        // _DieEffectValue가  dieEffectDuration 시간에 걸쳐 1에서 0으로 감소
        yield return flashDuration;
        float elapsedTime = 0f;
        while (elapsedTime < dieDelay)
        {
            float dieEffectValue = 1f - (elapsedTime / dieDelay);
            renderer.material.SetFloat("_DieEffectValue", dieEffectValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);

    }
}
