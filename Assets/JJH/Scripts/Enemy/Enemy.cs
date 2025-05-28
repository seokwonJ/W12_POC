using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed { get; private set; } = 3f;
    public float retreatSpeed { get; private set; } = 4f;
    public float approachRange { get; private set; } = 20f;
    public float attackRange { get; private set; } = 10f;
    public float retreatDistance { get; private set; } = 11f;
    public float fireCooldown { get; private set; } = 2f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    public Transform player;
    private Rigidbody2D rb;
    public IEnemyState CurrentState { get; private set; }
    public IEnemyState ApproachState { get; private set; }
    public IEnemyState AttackState { get; private set; }
    //public IEnemyState RetreatState { get; private set; }

    public Vector2 MoveDirection { get; set; } = Vector2.zero;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        Init();
    }

    private void Init()
    {
        ApproachState = new ApproachState(this);
        AttackState = new AttackState(this);
        //RetreatState = new RetreatState(this);
        CurrentState = ApproachState;
        CurrentState.Enter();

    }

    private void Update()
    {
        CurrentState.Update();
    }

    private void FixedUpdate()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    public void ChangeState(IEnemyState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
}
