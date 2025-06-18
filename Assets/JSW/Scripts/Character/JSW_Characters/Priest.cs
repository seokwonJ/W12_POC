using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Priest : Character
{
    [Header("스킬")]
    public GameObject skillProjectile;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillDuration = 5f;
    private bool isSkillLanding;
    public int skillHealAmount = 10;
    public bool isSkillBuff;


    [Header("강화")]
    public bool isUpgradeSkillCharacterAttackUp;
    public float SkillCharacterAttackUpNum = 10;
    public bool isUpgradeSkillPlayerSpeedUp;
    public float skillPlayerSpeedUpPercent = 130;
    public bool isUpgradeAttackEnemyDefenseDown;
    public float attackEnemyDefenseDownPercent = 30;
    public float attackEnemyDefenseDownDuration = 3;

    public int upgradeNum;
    public GameObject player;

    [Header("이펙트")]
    public GameObject priestSkillAllActiveEffect;
    public GameObject priestSkillActiveEffect;
    public GameObject priestSkillDurationEffect;
    public GameObject PriestHealEffect;

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

        Transform enemyTarget = FindNearestEnemy(); // 타겟 추적하는 메서드 필요


       proj.GetComponent<PriestAttack>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), isUpgradeAttackEnemyDefenseDown, attackEnemyDefenseDownPercent, attackEnemyDefenseDownDuration, enemyTarget);
        

        SoundManager.Instance.PlaySFX("PriestAttack");
    }

    Coroutine skillCoroutine;
    float skillUpNum;

    // 스킬: 동료들 공격력 업
    protected override IEnumerator FireSkill()
    {
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
            float nowSkillUpNum = TotalSkillDamage();

            if (isUpgradeSkillCharacterAttackUp) nowSkillUpNum += SkillCharacterAttackUpNum;

            ridingCharacter.GetComponent<Character>().attackBase += nowSkillUpNum;
            skillUpNum = nowSkillUpNum;
        }

        if (isUpgradeSkillPlayerSpeedUp) Managers.PlayerControl.NowPlayer.GetComponent<PlayerStatus>().speed *= (skillPlayerSpeedUpPercent / 100);

        yield return new WaitForSeconds(skillDuration);

        PowerDown();
    }

    public void PowerDown()
    {
        isSkillBuff = false;

        foreach (GameObject ridingCharacter in nowCharacters)
        {
            print("데미지 파워 다운!!!!");
            ridingCharacter.GetComponent<Character>().attackBase -= skillUpNum;
        }

        foreach (GameObject ridingCharacterEffect in nowCharactersEffects)
        {
            Destroy(ridingCharacterEffect);
        }

        if (isUpgradeSkillPlayerSpeedUp) Managers.PlayerControl.NowPlayer.GetComponent<PlayerStatus>().speed /= (skillPlayerSpeedUpPercent / 100);

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