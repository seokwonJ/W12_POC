using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int enemyHP;

    public void TakeDamage(int hp)
    {
        enemyHP -= hp;
        Debug.Log("적이 피해를 받음: " + hp);
        StartCoroutine(getDmagedEffect());

        if (enemyHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("적이 죽음");

        Destroy(gameObject);
    }
    IEnumerator getDmagedEffect()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
