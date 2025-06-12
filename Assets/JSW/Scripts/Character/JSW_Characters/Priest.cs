using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Priest : Character
{
    [Header("��ų")]
    public GameObject skillProjectile;
    public int skillCount = 3;
    public float skillInterval = 0.3f;
    public float skillFireDelay = 0.1f;
    public float skillDuration = 5f;

    public bool isSkillBuff;

    [Header("��ȭ")]
    public float nomalAttackSize;
    public bool isAddAttackDamage;
    public bool isnomalAttackSizePerMana;
    public bool isCanTeleport;
    public int upgradeNum;
    public GameObject player;

    [Header("����Ʈ")]
    public GameObject skillActiveEffect;

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

        float nownomalAttackSize = nomalAttackSize;
        //if (isnomalAttackSizePerMana) nownomalAttackSize *= currentMP / 50;

        Transform enemyTarget = FindNearestEnemy(); // Ÿ�� �����ϴ� �޼��� �ʿ�

        if (isAddAttackDamage) proj.GetComponent<PriestAttack>().SetInit(direction, abilityPower + attackDamage, projectileSpeed, nownomalAttackSize, enemyTarget);
        else
        {
            proj.GetComponent<PriestAttack>().SetInit(direction, attackDamage, projectileSpeed, nownomalAttackSize, enemyTarget);
        }

        SoundManager.Instance.PlaySFX("MagicianAttack");
    }

    Coroutine skillCoroutine;

    // ��ų: ����� ���ݷ� ��
    protected override IEnumerator FireSkill()
    {
        if (isCanTeleport) StartCoroutine(TeleportToPlayer());

        yield return new WaitForSeconds(skillFireDelay);

        animator.Play("SKILL", -1, 0f);

        if (skillCoroutine != null)
        {
            StopCoroutine(skillCoroutine);

            isSkillBuff = false;

            List<GameObject> nowCharacters = Managers.PlayerControl.Characters;

            foreach (GameObject ridingCharacter in nowCharacters)
            {
                ridingCharacter.GetComponent<Character>().attackDamage -= 10;
            }

            skillCoroutine = StartCoroutine(CharactersAttackPowerUp());
        }
        else
        {
            skillCoroutine = StartCoroutine(CharactersAttackPowerUp());
        }
        //Instantiate(skillActiveEffect, transform.position, Quaternion.identity, transform);
        SoundManager.Instance.PlaySFX("MagicianSkill");

        yield return new WaitForSeconds(skillInterval);
    }

    // ��ų ���ݷ� ��
    protected IEnumerator CharactersAttackPowerUp()
    {
        List<GameObject> nowCharacters = Managers.PlayerControl.Characters;

        isSkillBuff = true;

        foreach (GameObject ridingCharacter in nowCharacters)
        {
            print("������ �Ŀ� ��!!!!");
            ridingCharacter.GetComponent<Character>().attackDamage += 10;
        }
        

        yield return new WaitForSeconds(skillDuration);

        isSkillBuff = false;

        foreach (GameObject ridingCharacter in nowCharacters)
        {
            print("������ �Ŀ� �ٿ�!!!!");
            ridingCharacter.GetComponent<Character>().attackDamage -= 10;
        }

        skillCoroutine = null;
    }

    IEnumerator TeleportToPlayer()
    {
        yield return new WaitForSeconds(5f);
        if (!isGround) transform.position = player.transform.position + Vector3.up;
    }

    public override void EndFieldAct() // �ʵ������� ����� �� ����
    {
        base.EndFieldAct();

        if (isSkillBuff)
        {
            isSkillBuff = false;

            List<GameObject> nowCharacters = Managers.PlayerControl.Characters;

            foreach (GameObject ridingCharacter in nowCharacters)
            {
                ridingCharacter.GetComponent<Character>().attackDamage -= 10;
            }

            skillCoroutine = null;
        }
    }
}