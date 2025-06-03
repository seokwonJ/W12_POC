using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int enemyHP;
    private Renderer renderer;
    private WaitForSeconds flashDuration = new WaitForSeconds(0.1f);
    private float dieDelay = 1f;
    private WaitForSeconds dieEffectDuration;

    private void Awake()
    {
        dieEffectDuration = new WaitForSeconds(dieDelay);
        renderer = GetComponent<Renderer>();

    }

    public void TakeDamage(int hp)
    {
        enemyHP -= hp;
        StartCoroutine(getDmagedEffect());

        if (enemyHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {


        Destroy(gameObject, dieDelay);
    }
    IEnumerator getDmagedEffect()
    {
        Debug.Log("getDmagedEffect");
        renderer.material.EnableKeyword("_ISFLASHED");
        yield return flashDuration;
        renderer.material.DisableKeyword("_ISFLASHED");
    }
}
