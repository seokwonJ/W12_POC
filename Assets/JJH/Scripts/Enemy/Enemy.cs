using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float fireCooldown = 2f;
    public float projectileSpeed;

    public ScriptableObject movementSO;
    public ScriptableObject attackSO;

    private IMovementPattern movement;
    private IAttackPattern attack;

    public GameObject projectilePrefab;

    public GameObject player;
    public Rigidbody2D rb;

    public Vector2 MoveDirection { get; set; } = Vector2.zero;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        if (movementSO != null )
        {
            ScriptableObject movementInstance = Instantiate(movementSO);
            movement = movementInstance as IMovementPattern;
        }

        if ( attackSO != null)
        {
            ScriptableObject attackInstance = Instantiate(attackSO);
            attack = attackInstance as IAttackPattern;
        }

        movement?.Init(this);
        attack?.Init(this);

        attack?.Attack();
    }


    private void FixedUpdate()
    {
        movement?.Move();
    }
}
