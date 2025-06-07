using System.Collections;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject TextPrefab; // 코인 획득 텍스트 프리팹
    public int value;

    private Canvas canvas;
    private float maxFallSpeed = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private const float fadeInDuration = 0.3f; // 페이드인 지속 시간
    private const float itemDuration = 15f; // 유지 지속 시간
    private const float BlinkDuration = 5f; // 파괴 예고로 깜박이는 시간

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        canvas = GameObject.Find("TimerCanvas").GetComponent<Canvas>();
    }

    private void Start()
    {
        StartCoroutine(CoFadein());
        rb.AddForce(new Vector2(Random.Range(-2f, 2f), Random.Range(1f, 2f)) * 5f, ForceMode2D.Impulse);

        StartCoroutine(CoDestroyCounter());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flyer"))
        {
            Managers.Status.Gold += value;
            SoundManager.Instance.PlaySFX("PickUpItem"); // 코인 획득 사운드 재생
            // TextPrefab을 사용하여 코인 획득 텍스트 표시
            if (TextPrefab != null)
            {
                // 월드 좌표를 화면 좌표로 변경하여 생성
                Vector3 worldPosition = transform.position;
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
                GameObject textInstance = Instantiate(TextPrefab, canvas.transform);
                textInstance.transform.position = screenPosition; // 화면 좌표에 맞게 위치 설정
                textInstance.GetComponent<TextMeshProUGUI>().text = $"+ {value}";
            }
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

    IEnumerator CoDestroyCounter()
    {
        yield return new WaitForSeconds(itemDuration - BlinkDuration);
        yield return StartCoroutine(CoBlink());
        Destroy(gameObject);
    }

    IEnumerator CoBlink()
    {
        //BlinkDuration 동안 알파값을 0과 1로 번갈아가며 변경
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color;
        while (elapsedTime < BlinkDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.PingPong(elapsedTime * 2f, 1f); // 0과 1 사이에서 깜박임
            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }
    }
}
