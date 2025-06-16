using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public int enemyHP;
    private int maxEnemyHP;
    public Image playerHP_Image; // 적 HP바 프리팹
    public bool isSpawning = true;
    public bool isDead = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D collider;
    private Animator animator;
    public Rigidbody2D rb;
    private WaitForSeconds flashDuration = new WaitForSeconds(0.1f);

    private float dieDelay = 0.5f;
    private Coroutine flashCoroutine;

    private void OnEnable()
    {
        Managers.Stage.CurEnemyCount++;
        collider.enabled = false;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        StartCoroutine(CoAlphaChange(1f, 0.2f)); // 활성화 시 알파값을 1로 변경
    }

    IEnumerator CoAlphaChange(float targetAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        while (elapsedTime < duration)
        {
            spriteRenderer.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = targetColor; // 최종 색상 적용
        collider.enabled = true;
        isSpawning = false;
    }
    private void OnDisable()
    {
        Managers.Stage.CurEnemyCount--;
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("애니메이터가 없으므로 부모 오브젝트에서 가져옵니다.");
            animator = GetComponentInParent<Animator>();
        }
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("Rigidbody2D가 없으므로 부모 오브젝트에서 가져옵니다.");
            rb = GetComponentInParent<Rigidbody2D>();
        }
        maxEnemyHP = enemyHP; // 최대 HP 저장

        if (GetComponent<Boss>() != null)
        {
            dieDelay = 2f; // 보스의 경우 사망 딜레이를 늘림
        }
    }

    public void TakeDamage(int hp, ECharacterType attacker = ECharacterType.None)
    {
        if (isDead) return;


        // 딜로그에 기록
        int damage = hp > enemyHP ? enemyHP : hp; // 적의 HP보다 큰 데미지는 적의 HP로 제한
        Managers.Record.AddStageDamgeRecord(attacker, damage);

        enemyHP -= hp;
        if (playerHP_Image != null)
        {
            playerHP_Image.fillAmount = (float)enemyHP / maxEnemyHP; // HP바 갱신
        }

        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(CoDamagedEffect());
        SoundManager.Instance.PlaySFX("HitSound");

        if (enemyHP <= 0)
        {

            Boss boss = GetComponent<Boss>(); // 보스라면 StageManager의 OnField를 False로 설정
            if (boss != null)
            {
                Managers.Stage.OnField = false; // 보스가 죽으면 필드 종료
            }

            Managers.Stage.PlusEnemyKill(transform.position);
            Die();
        }
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        if (collider != null) collider.enabled = false; // 사망시 콜라이더 비활성화
        if (animator != null) animator.StopPlayback(); // 사망시 애니메이션 정지
        gameObject.tag = "Untagged"; // 사망시 태그 제거
        rb.linearVelocity = Vector2.zero; // 죽을 때 속도 초기화
       
        StartCoroutine(CoDieEffect());
        //StartCoroutine(CoAlphaChange(0f, dieDelay * 2f)); // 활성화 시 알파값을 1로 변경
    }
    IEnumerator CoDamagedEffect()
    {
        spriteRenderer.material.EnableKeyword("_ISFLASHED");
        yield return flashDuration;
        spriteRenderer.material.DisableKeyword("_ISFLASHED");
    }

    IEnumerator CoDieEffect()
    {
        // 애니메이션 정지
        if (animator != null)
        {
            animator.enabled = false;
        }
        // Hp Bar제거
        Canvas hpBarCanvas = GetComponentInChildren<Canvas>();
        if (hpBarCanvas != null)
        {
            hpBarCanvas.enabled = false;
        }

        // _DieEffectValue가  dieDelay 시간에 걸쳐 1에서 0으로 감소
        float elapsedTime = 0f;
        while (elapsedTime < dieDelay)
        {
            // sin에 의한 부드러운 감소 효과
            //float dieEffectValue = Mathf.Sin((1f - (elapsedTime / dieDelay)) * Mathf.PI * 0.5f);
            float dieEffectValue = 1f - (elapsedTime / dieDelay); // 선형 감소
            spriteRenderer.material.SetFloat("_DieEffectValue", dieEffectValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
