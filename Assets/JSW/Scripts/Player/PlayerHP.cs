﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Image playerHP_Image;
    [HideInInspector]
    public GameObject hpBarObject;
    public SpriteRenderer rendererFlyer;

    // 추가: HPBar 프리팹과 캔버스 Transform을 Inspector에서 할당
    public GameObject hpBarPrefab;
    public Transform canvasTransform;
    public Vector3 hpBarOffset;
    public bool isEndFieldNoDamage;

    private PlayerStatus _playerStatus;
    private PlayerMove _playerMove;
    private CircleCollider2D hitCollider;

    private Coroutine flashCoroutine;
    private Coroutine healFlashCoroutine;
    private WaitForSeconds flashDuration = new WaitForSeconds(0.1f);
    private float hitNoDamageDuration = 1f;
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
        _playerMove = Managers.PlayerControl.NowPlayer.GetComponent<PlayerMove>();
        hitCollider = GetComponent<CircleCollider2D>();

        // HPBar 프리팹을 직접 생성하고 연결
        //if (hpBarPrefab != null && canvasTransform != null)
        //{
        //    HPBar hpBarScript = hpBarObject.GetComponent<HPBar>();
        //    if (hpBarScript != null)
        //    {
        //        hpBarScript.target = this.transform;
        //        hpBarScript.offset = hpBarOffset;
        //    }
        //    playerHP_Image = hpBarObject.GetComponent<Image>();
        //}
        // Start에서 fillAmount 갱신 코드는 이벤트 기반으로 대체
    }

    // HP가 바뀔 때마다 자동으로 호출되는 UI 갱신 메서드
    public void RefreshHPBar()
    {
        if (playerHP_Image != null)
            playerHP_Image.fillAmount = Managers.Status.Hp / Managers.Status.MaxHp;
        isEndFieldNoDamage = false;     // 비행체 무적 해제
    }

    public virtual void TakeDamage(int damage)
    {
        if (isEndFieldNoDamage) return;

        Managers.Status.Hp -= (damage - _playerStatus.defensePower);
        SoundManager.Instance.PlaySFX("PlayerHitSound");

        if (playerHP_Image == null)
        {
            HPFill hpFill = transform.parent.GetComponentInChildren<HPFill>(true);
            playerHP_Image = hpFill.GetComponent<Image>();

        }
        float fill = Managers.Status.Hp / Managers.Status.MaxHp;
        playerHP_Image.fillAmount = fill;
        Debug.Log($"[PlayerHP] HP: {Managers.Status.Hp}, MaxHP: {Managers.Status.MaxHp}, fillAmount: {fill}");

        if (flashCoroutine != null) StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(CoDamagedEffect());


        if (Managers.Status.Hp <= 0)
        {
            // GetComponent<TmpPlayerControl>().GatherCharacters(); // 주석 처리: 해당 메서드가 없음
            Managers.SceneFlow.GameOver();
            if (hpBarObject != null) Destroy(hpBarObject);
        }
    }

    public virtual void TakeHeal(int Num)
    {
        if (isEndFieldNoDamage) return;

        if (Managers.Status.Hp + (Num) >= Managers.Status.MaxHp) Managers.Status.Hp = Managers.Status.MaxHp;
        else { Managers.Status.Hp += (Num); }

        //SoundManager.Instance.PlaySFX("PlayerHitSound");

        if (playerHP_Image != null)
        {
            float fill = Managers.Status.Hp / Managers.Status.MaxHp;
            playerHP_Image.fillAmount = fill;
            Debug.Log($"[PlayerHP] HP: {Managers.Status.Hp}, MaxHP: {Managers.Status.MaxHp}, fillAmount: {fill}");

            if (healFlashCoroutine != null) StopCoroutine(healFlashCoroutine);
            healFlashCoroutine = StartCoroutine(CoHealEffect());
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
        float elapsed = 0f;
        float blinkInterval = 0.3f; // 깜빡이는 간격
        float duration = hitNoDamageDuration; // 시간 추출

        hitCollider.enabled = false;

        Color colorBody = rendererFlyer.color;

        while (elapsed < duration)
        {
            // 투명
            Color color = spriteRendererCore.color;
            color.a = 0.3f;
            spriteRendererCore.color = color;

            colorBody = rendererFlyer.color;
            colorBody.a = 0.3f;
            rendererFlyer.color = colorBody;

            yield return new WaitForSeconds(blinkInterval / 2f);

            // 불투명
            color.a = 1f;
            spriteRendererCore.color = color;

            colorBody.a = 1f;
            rendererFlyer.color = colorBody;

            yield return new WaitForSeconds(blinkInterval / 2f);
            elapsed += blinkInterval;
        }

        // 깜빡임 끝나고 최종 색상 설정
        hitCollider.enabled = true;

        Color finalColor = Color.white;
        if (_playerMove.isCanDashing)
            finalColor = Color.green;

        finalColor.a = 1f;
        spriteRendererCore.color = finalColor;

        colorBody.a = 1f;
        rendererFlyer.color = colorBody;
    }

    IEnumerator CoHealEffect()
    {
        spriteRendererCore.color = Color.magenta; // 코어 색상을 빨간색으로 변경
        yield return flashDuration;
        if (_playerMove.isCanDashing)
        {
            spriteRendererCore.color = Color.green; // 코어 색상을 원래대로 되돌림
        }
        else
        {
            spriteRendererCore.color = Color.white; // 코어 색상을 원래대로 되돌림
        }
    }
}
