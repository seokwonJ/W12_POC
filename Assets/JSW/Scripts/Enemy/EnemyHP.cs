using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int enemyHP;

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


        Destroy(gameObject);
    }
    IEnumerator getDmagedEffect()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
