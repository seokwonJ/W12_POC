using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value;
    private float maxFallSpeed = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private const float fadeInDuration = 0.3f; // 페이드인 지속 시간

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(CoFadein());
        rb.AddForce(new Vector2(Random.Range(-2f, 2f), Random.Range(1f, 2f)) * 5f, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Managers.Status.Gold += value;
            // to do 코인 획득 이펙트
            Debug.Log($"Coin collected! Value: {value}, Total Gold: {Managers.Status.Gold}");
            Destroy(gameObject);
        }
    }

    protected virtual void FixedUpdate()
    {
        if (rb.linearVelocity.y < -maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxFallSpeed);
        }
    }

    public void SetCoinValue(int coinValue)
    {
        value = coinValue;
    }

    IEnumerator CoFadein()
    {
        // fadeInDuration 시간동안 알파값을 0에서 1로 증가
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeInDuration);
            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }
    }
}
