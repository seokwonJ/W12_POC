using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
#if UNITY_EDITOR
    [TextArea]
    [SerializeField] private string editorNote = "";
    #pragma warning restore 0414
#endif
    public ScriptableObject movementSO;
    public ScriptableObject attackSO;

    public float speed;
    public float minPlayerDistance;

    public float fireCooldown;
    public int damage; 
    public float projectileSpeed;

    public GameObject projectilePrefab;

    private IMovementPattern movement;
    private IAttackPattern attack;


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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHP>().TakeDamage(20);
            Destroy(gameObject);
        }
        else if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
