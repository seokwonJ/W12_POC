using System.Collections;
using UnityEngine;

public class EnemyAttack1 : MonoBehaviour
{
    public int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
        }
        else if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
