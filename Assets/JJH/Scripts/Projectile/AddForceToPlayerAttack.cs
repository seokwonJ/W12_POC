using System.Collections;
using UnityEngine;

public class AddForceToPlayerAttack : EnemyAttack1
{
    private GameObject player;
    private Rigidbody2D rb;
    private WaitForSeconds changeDirectionWait = new WaitForSeconds(0.3f);
    private WaitForSeconds beforeAddForceWait = new WaitForSeconds(0.01f);
    private float addForceDurationTime = 4f;
    private float force = 2f;
    Vector2 direction;
    float elaspedTime = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        player = Managers.PlayerControl.NowPlayer;
        StartCoroutine(CoWaitForAddForce());
    }

    private void Update()
    {
        // 플레이어 방향으로 회전
        float angle = Mathf.Atan2(rb.linearVelocityY, rb.linearVelocityX) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // 180도 회전 보정

        elaspedTime += Time.deltaTime;
        if (elaspedTime > addForceDurationTime)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator CoWaitForAddForce()
    {
        yield return beforeAddForceWait;
        StartCoroutine (CoAddForceToPlayer());
    }

    IEnumerator CoAddForceToPlayer()
    {
        while (player != null)
        {
            direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * force, ForceMode2D.Impulse);
            yield return changeDirectionWait;
            force += 0.1f;
        }
    }
}
