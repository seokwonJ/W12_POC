using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = (player.position - transform.position).normalized * speed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHP>().TakeDamage(50);
            Destroy(gameObject);
        }
    }
}
