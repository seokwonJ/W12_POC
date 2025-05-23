using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public float speed = 3f;
    public float retreatSpeed = 4f;
    public float approachRange = 7f;
    public float attackRange = 4f;
    public float retreatDistance = 5f;
    public float fireCooldown = 2f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    private Transform player;
    private float lastFireTime;
    private enum State { Approach, Attack, Retreat }
    private State currentState = State.Approach;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Approach:
                if (distance <= attackRange)
                {
                    currentState = State.Attack;
                }
                else
                {
                    MoveTowards(player.position, speed);
                }
                break;

            case State.Attack:
                if (Time.time - lastFireTime > fireCooldown)
                {
                    FireProjectile();
                    lastFireTime = Time.time;
                    currentState = State.Retreat;
                }
                break;

            case State.Retreat:
                if (distance >= retreatDistance)
                {
                    currentState = State.Approach;
                }
                else
                {
                    Vector2 dir = (transform.position - player.position).normalized;
                    MoveTowards(transform.position + (Vector3)dir, retreatSpeed);
                }
                break;
        }
    }

    private void MoveTowards(Vector2 target, float moveSpeed)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    private void FireProjectile()
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 dir = (player.position - firePoint.position).normalized;
        proj.GetComponent<Rigidbody2D>().linearVelocity = dir * 8f;
    }
}
