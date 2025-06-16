using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Priest : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillDuration = 5f;
    private bool isSkillLanding;
    public int skillHealAmount = 10;

    public bool isSkillBuff;
    public GameObject priestSkillAllActiveEffect;
    public GameObject priestSkillActiveEffect;
    public GameObject priestSkillDurationEffect;
    public GameObject PriestHealEffect;

    [Header("강화")]
    public float nomalAttackSize;
    public bool isAddAttackDamage;
    public bool isnomalAttackSizePerMana;
    public bool isCanTeleport;
    public int upgradeNum;
    public GameObject player;

    [Header("이펙트")]
    public GameObject skillActiveEffect;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMove>().gameObject;
    }

    // 일반 공격: 가까운 적에게 관통 공격
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        // 방향에 따라 캐릭터 스프라이트 좌우 반전
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float totalAttackDamage = TotalAttackDamage();

        float nownomalAttackSize = nomalAttackSize;
        //if (isnomalAttackSizePerMana) nownomalAttackSize *= currentMP / 50;

        Transform enemyTarget = FindNearestEnemy(); // 타겟 추적하는 메서드 필요

        if (isAddAttackDamage) proj.GetComponent<PriestAttack>().SetInit(direction, totalAttackDamage, projectileSpeed, nownomalAttackSize, enemyTarget);
        else
        {
            proj.GetComponent<PriestAttack>().SetInit(direction, totalAttackDamage, projectileSpeed, nownomalAttackSize, enemyTarget);
        }

        SoundManager.Instance.PlaySFX("PriestAttack");
    }

    Coroutine skillCoroutine;

    // 스킬: 동료들 공격력 업
    protected override IEnumerator FireSkill()
    {
        if (isCanTeleport) StartCoroutine(TeleportToPlayer());

        yield return new WaitForSeconds(skillFireDelay);

        animator.Play("SKILL", -1, 0f);

        Instantiate(priestSkillActiveEffect, transform.position, Quaternion.identity, transform);
        
        isSkillLanding = true;

        SoundManager.Instance.PlaySFX("PriestSkillActive");

        if (skillCoroutine != null)
        {
            StopCoroutine(skillCoroutine);
            PowerDown();
            skillCoroutine = StartCoroutine(CharactersAttackPowerUp());
        }
        else
        {
            skillCoroutine = StartCoroutine(CharactersAttackPowerUp());
        }

        //Instantiate(skillActiveEffect, transform.position, Quaternion.identity, transform);
        //SoundManager.Instance.PlaySFX("MagicianSkill");

        yield return new WaitForSeconds(skillInterval);
    }

    List<GameObject> nowCharacters;
    List<GameObject> nowCharactersEffects;

    // 스킬 공격력 업
    protected IEnumerator CharactersAttackPowerUp()
    {
        nowCharacters = Managers.PlayerControl.Characters;

        isSkillBuff = true;

        nowCharactersEffects = new List<GameObject>();

        foreach (GameObject ridingCharacter in nowCharacters)
        {
            print("데미지 파워 업!!!!");
            Instantiate(priestSkillAllActiveEffect, ridingCharacter.transform.position, Quaternion.identity, ridingCharacter.transform);
            nowCharactersEffects.Add(Instantiate(priestSkillDurationEffect, ridingCharacter.transform.position, Quaternion.identity, ridingCharacter.transform));
            ridingCharacter.GetComponent<Character>().attackBase += TotalSkillDamage();
        }

        yield return new WaitForSeconds(skillDuration);

        PowerDown();
    }

    IEnumerator TeleportToPlayer()
    {
        yield return new WaitForSeconds(5f);
        if (!isGround) transform.position = player.transform.position + Vector3.up;
    }

    public void PowerDown()
    {
        isSkillBuff = false;

        foreach (GameObject ridingCharacter in nowCharacters)
        {
            print("데미지 파워 다운!!!!");
            ridingCharacter.GetComponent<Character>().attackBase -= TotalSkillDamage(); ;
        }

        foreach (GameObject ridingCharacterEffect in nowCharactersEffects)
        {
            Destroy(ridingCharacterEffect);
        }

        skillCoroutine = null;
    }



    // 착지했을 경우
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // 부모 로직 먼저 실행

        // 조건 만족 안 하면 아무것도 안 하고 리턴 (부모에서 이미 isGround 처리됨)
        if (!isGround) return;

        // 추가 스킬 처리
        if (isSkillLanding)
        {
            SoundManager.Instance.PlaySFX("PriestHealActive");
            isSkillLanding = false;
            Instantiate(PriestHealEffect, transform.position + Vector3.up * 1.7f, Quaternion.identity, transform);
            Managers.PlayerControl.NowPlayer.GetComponentInChildren<PlayerHP>().TakeHeal(skillHealAmount);
        }
    }



    public override void EndFieldAct() // 필드전투가 종료될 때 실행
    {
        base.EndFieldAct();

        isSkillLanding = false;

        if (isSkillBuff)
        {
            PowerDown();
        }
    }

}