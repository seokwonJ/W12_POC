using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public int enemyHP;
    private int maxEnemyHP;
    public int defaultArmor; // 방어력
    public int currentArmor; // 현재 방어력
    public Image playerHP_Image; // 적 HP바 프리팹
    public bool isSpawning = true;
    public bool isDead = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    private Animator animator;
    public Rigidbody2D rb;
    private WaitForSeconds flashDuration = new WaitForSeconds(0.1f);

    private float dieDelay = 0.5f;
    private Coroutine flashCoroutine;

    private void OnEnable()
    {
        Managers.Stage.CurEnemyCount++;
        col.enabled = false;
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
        col.enabled = true;
        isSpawning = false;
    }
    private void OnDisable()
    {
        Managers.Stage.CurEnemyCount--;
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
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
        currentArmor = defaultArmor; // 현재 방어력 초기화
        if (GetComponent<Boss>() != null)
        {
            dieDelay = 2f; // 보스의 경우 사망 딜레이를 늘림
        }
    }

    public void TakeDamage(int damage, ECharacterType attacker = ECharacterType.None)
    {
        if (isDead) return;

        int finalDamage = Mathf.Max(damage - currentArmor, 1); // 데미지에 방어력을 깎아 최종 데미지 계산
        finalDamage = finalDamage > enemyHP ? enemyHP : finalDamage; // 적의 HP보다 큰 데미지는 적의 HP로 제한

        Managers.Record.AddStageDamgeRecord(attacker, finalDamage); // 딜로그에 기록

        enemyHP -= finalDamage;
        if (playerHP_Image != null)
        {
            playerHP_Image.fillAmount = (float)enemyHP / maxEnemyHP; // HP바 갱신
        }

        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(CoDamagedEffect());
        SoundManager.Instance.PlaySFX("HitSound");

        if (enemyHP <= 0)
        {

            Boss boss = GetComponent<Boss>();
            if (boss != null)
            {
                StartCoroutine(CoDelayedEndField(1f)); // 보스는 필드 종료를 3초 후에 실행
            }

            Managers.Stage.PlusEnemyKill(transform.position);
            Die();
        }
    }

    IEnumerator CoDelayedEndField(float delayedTime)
    {
        yield return new WaitForSeconds(delayedTime);
        Managers.Stage.OnField = false; // 필드 종료
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        if (col != null) col.enabled = false; // 사망시 콜라이더 비활성화
        if (animator != null) animator.StopPlayback(); // 사망시 애니메이션 정지
        gameObject.tag = "Untagged"; // 사망시 태그 제거
        rb.linearVelocity = Vector2.zero; // 죽을 때 속도 초기화
        // Hp Bar제거
        Canvas hpBarCanvas = GetComponentInChildren<Canvas>();
        if (hpBarCanvas == null)
        {
            hpBarCanvas = transform.parent.GetComponentInChildren<Canvas>();
        }
        hpBarCanvas.enabled = false;

        // 애니메이션 정지
        if (animator != null)
        {
            animator.enabled = false;
        }
       
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

    Coroutine co = null;        // 함수 밖에  나왔어야 초기화가 안됨
    public void ReduceArmor(int value, float durationTime, bool isPercent=true)
    {
        int resultValue = isPercent ? Mathf.CeilToInt(defaultArmor * (value / 100f)) : value;
        int resultArmor = defaultArmor - resultValue;
        if (resultArmor <= currentArmor)
        {
            if (co != null)  StopCoroutine(co);
            co = StartCoroutine(CoReduceArmor(resultArmor, durationTime));
        }
    }

    IEnumerator CoReduceArmor(int resultArmor, float durationTime)
    {
        currentArmor = Mathf.Max(resultArmor, 0);
        Debug.Log($"{gameObject.name} 적의 방어력 감소, 현재 방어력: {currentArmor}");
        yield return new WaitForSeconds(durationTime);
        currentArmor = defaultArmor; // 방어력 회복
        Debug.Log($"{gameObject.name} 적의 방어력 다시 회복, 현재 방어력: {currentArmor}");
    }
}
