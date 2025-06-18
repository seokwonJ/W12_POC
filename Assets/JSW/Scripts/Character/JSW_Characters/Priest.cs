using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Priest : Character
{
    [Header("��ų")]
    public GameObject skillProjectile;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillDuration = 5f;
    private bool isSkillLanding;
    public int skillHealAmount = 10;
    public bool isSkillBuff;


    [Header("��ȭ")]
    public bool isUpgradeSkillCharacterAttackUp;
    public float SkillCharacterAttackUpNum = 10;
    public bool isUpgradeSkillPlayerSpeedUp;
    public float skillPlayerSpeedUpPercent = 130;
    public bool isUpgradeAttackEnemyDefenseDown;
    public float attackEnemyDefenseDownPercent = 30;
    public float attackEnemyDefenseDownDuration = 3;

    public int upgradeNum;
    public GameObject player;

    [Header("����Ʈ")]
    public GameObject priestSkillAllActiveEffect;
    public GameObject priestSkillActiveEffect;
    public GameObject priestSkillDurationEffect;
    public GameObject PriestHealEffect;

    protected override void Start()
    {
        base.Start();
        player = FindAnyObjectByType<PlayerMove>().gameObject;
    }

    // �Ϲ� ����: ����� ������ ���� ����
    protected override void FireNormalProjectile(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - firePoint.position).normalized;

        // ���⿡ ���� ĳ���� ��������Ʈ �¿� ����
        if (direction.x > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction.x < 0) transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        GameObject proj = Instantiate(normalProjectile, firePoint.position, Quaternion.identity);

        float totalAttackDamage = TotalAttackDamage();

        Transform enemyTarget = FindNearestEnemy(); // Ÿ�� �����ϴ� �޼��� �ʿ�


       proj.GetComponent<PriestAttack>().SetInit(direction, totalAttackDamage, projectileSpeed * (projectileSpeedUpNum / 100), projectileSize * (projectileSizeUpNum / 100), knockbackPower * (knockbackPowerUpNum / 100), isUpgradeAttackEnemyDefenseDown, attackEnemyDefenseDownPercent, attackEnemyDefenseDownDuration, enemyTarget);
        

        SoundManager.Instance.PlaySFX("PriestAttack");
    }

    Coroutine skillCoroutine;
    float skillUpNum;

    // ��ų: ����� ���ݷ� ��
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

    // ��ų ���ݷ� ��
    protected IEnumerator CharactersAttackPowerUp()
    {
        nowCharacters = Managers.PlayerControl.Characters;

        isSkillBuff = true;

        nowCharactersEffects = new List<GameObject>();

        foreach (GameObject ridingCharacter in nowCharacters)
        {
            print("������ �Ŀ� ��!!!!");
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
            print("������ �Ŀ� �ٿ�!!!!");
            ridingCharacter.GetComponent<Character>().attackBase -= skillUpNum;
        }

        foreach (GameObject ridingCharacterEffect in nowCharactersEffects)
        {
            Destroy(ridingCharacterEffect);
        }

        if (isUpgradeSkillPlayerSpeedUp) Managers.PlayerControl.NowPlayer.GetComponent<PlayerStatus>().speed /= (skillPlayerSpeedUpPercent / 100);

        skillCoroutine = null;
    }



    // �������� ���
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision); // �θ� ���� ���� ����

        // ���� ���� �� �ϸ� �ƹ��͵� �� �ϰ� ���� (�θ𿡼� �̹� isGround ó����)
        if (!isGround) return;

        // �߰� ��ų ó��
        if (isSkillLanding)
        {
            SoundManager.Instance.PlaySFX("PriestHealActive");
            isSkillLanding = false;
            Instantiate(PriestHealEffect, transform.position + Vector3.up * 1.7f, Quaternion.identity, transform);
            Managers.PlayerControl.NowPlayer.GetComponentInChildren<PlayerHP>().TakeHeal(skillHealAmount);
        }
    }

    public override void EndFieldAct() // �ʵ������� ����� �� ����
    {
        base.EndFieldAct();

        isSkillLanding = false;

        if (isSkillBuff)
        {
            PowerDown();
        }
    }
}