using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int enemyHP;
    public Material damagedMat;
    private WaitForSeconds flashDuration = new WaitForSeconds(0.1f);

    public void TakeDamage(int hp)
    {
        enemyHP -= hp;
        //StartCoroutine(getDmagedEffect());

        if (enemyHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {


        Destroy(gameObject);
    }
    //IEnumerator getDmagedEffect()
    //{
    //    //damagedMat.SetFloat("_FlashAmount", 1f);
    //    //yield return flashDuration;
    //    //damagedMat.SetFloat("_FlashAmount", 0f);
    //}
}
