using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Image playerHP_Image;
    [HideInInspector]
    public GameObject hpBarObject;
    public Renderer rendererFlyer;

    // 추가: HPBar 프리팹과 캔버스 Transform을 Inspector에서 할당
    public GameObject hpBarPrefab;
    public Transform canvasTransform;
    public Vector3 hpBarOffset;

    private PlayerStatus _playerStatus;
    private Coroutine flashCoroutine;
    private WaitForSeconds flashDuration = new WaitForSeconds(0.1f);
    private SpriteRenderer spriteRendererCore;


    private void OnEnable()
    {
        Managers.Status.OnHpChanged += RefreshHPBar;
    }

    private void OnDisable()
    {
        Managers.Status.OnHpChanged -= RefreshHPBar;
    }

    private void Awake()
    {
        spriteRendererCore = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        _playerStatus = Managers.PlayerControl.NowPlayer.GetComponent<PlayerStatus>();

        // HPBar 프리팹을 직접 생성하고 연결
        if (hpBarPrefab != null && canvasTransform != null)
        {
            hpBarObject = Instantiate(hpBarPrefab, canvasTransform);
            HPBar hpBarScript = hpBarObject.GetComponent<HPBar>();
            if (hpBarScript != null)
            {
                hpBarScript.target = this.transform;
                hpBarScript.offset = hpBarOffset;
            }
            playerHP_Image = hpBarObject.GetComponent<Image>();
        }
        // Start에서 fillAmount 갱신 코드는 이벤트 기반으로 대체
    }

    // HP가 바뀔 때마다 자동으로 호출되는 UI 갱신 메서드
    public void RefreshHPBar()
    {
        if (playerHP_Image != null)
            playerHP_Image.fillAmount = Managers.Status.Hp / Managers.Status.MaxHp;
    }

    public virtual void TakeDamage(int damage)
    {
        Managers.Status.Hp -= (damage - _playerStatus.defensePower);
        SoundManager.Instance.PlaySFX("PlayerHitSound");
        if (playerHP_Image != null)
        {
            float fill = Managers.Status.Hp / Managers.Status.MaxHp;
            playerHP_Image.fillAmount = fill;
            Debug.Log($"[PlayerHP] HP: {Managers.Status.Hp}, MaxHP: {Managers.Status.MaxHp}, fillAmount: {fill}");

            if (flashCoroutine != null) StopCoroutine(flashCoroutine);
            flashCoroutine = StartCoroutine(CoDamagedEffect());
        }
        else
        {
            Debug.LogWarning("[PlayerHP] playerHP_Image is null! HPBar가 할당되지 않았습니다.");
        }

        if (Managers.Status.Hp <= 0)
        {
            // GetComponent<TmpPlayerControl>().GatherCharacters(); // 주석 처리: 해당 메서드가 없음
            Managers.SceneFlow.GameOver();
            if (hpBarObject != null) Destroy(hpBarObject);
        }
    }

    public virtual void TakeHeal(int damage)
    {
        if (Managers.Status.Hp + (damage - _playerStatus.defensePower) >= Managers.Status.MaxHp) Managers.Status.Hp = Managers.Status.MaxHp;
        else { Managers.Status.Hp += (damage - _playerStatus.defensePower); }

        //SoundManager.Instance.PlaySFX("PlayerHitSound");

        if (playerHP_Image != null)
        {
            float fill = Managers.Status.Hp / Managers.Status.MaxHp;
            playerHP_Image.fillAmount = fill;
            Debug.Log($"[PlayerHP] HP: {Managers.Status.Hp}, MaxHP: {Managers.Status.MaxHp}, fillAmount: {fill}");

            if (flashCoroutine != null) StopCoroutine(flashCoroutine);
            flashCoroutine = StartCoroutine(CoDamagedEffect());
        }
        else
        {
            Debug.LogWarning("[PlayerHP] playerHP_Image is null! HPBar가 할당되지 않았습니다.");
        }

        if (Managers.Status.Hp <= 0)
        {
            // GetComponent<TmpPlayerControl>().GatherCharacters(); // 주석 처리: 해당 메서드가 없음
            Managers.SceneFlow.GameOver();
            if (hpBarObject != null) Destroy(hpBarObject);
        }
    }

    IEnumerator CoDamagedEffect()
    {
        spriteRendererCore.color = Color.red; // 코어 색상을 빨간색으로 변경
        yield return flashDuration;
        spriteRendererCore.color = Color.white; // 코어 색상을 원래대로 되돌림
    }

}
