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

    public bool isSkillBuff;

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

        float nownomalAttackSize = nomalAttackSize;
        //if (isnomalAttackSizePerMana) nownomalAttackSize *= currentMP / 50;

        Transform enemyTarget = FindNearestEnemy(); // 타겟 추적하는 메서드 필요

        if (isAddAttackDamage) proj.GetComponent<PriestAttack>().SetInit(direction, abilityPower + attackDamage, projectileSpeed, nownomalAttackSize, enemyTarget);
        else
        {
            proj.GetComponent<PriestAttack>().SetInit(direction, attackDamage, projectileSpeed, nownomalAttackSize, enemyTarget);
        }

        SoundManager.Instance.PlaySFX("MagicianAttack");
    }

    Coroutine skillCoroutine;

    // 스킬: 동료들 공격력 업
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

    // 스킬 공격력 업
    protected IEnumerator CharactersAttackPowerUp()
    {
        List<GameObject> nowCharacters = Managers.PlayerControl.Characters;

        isSkillBuff = true;

        foreach (GameObject ridingCharacter in nowCharacters)
        {
            print("데미지 파워 업!!!!");
            ridingCharacter.GetComponent<Character>().attackDamage += 10;
        }
        

        yield return new WaitForSeconds(skillDuration);

        isSkillBuff = false;

        foreach (GameObject ridingCharacter in nowCharacters)
        {
            print("데미지 파워 다운!!!!");
            ridingCharacter.GetComponent<Character>().attackDamage -= 10;
        }

        skillCoroutine = null;
    }

    IEnumerator TeleportToPlayer()
    {
        yield return new WaitForSeconds(5f);
        if (!isGround) transform.position = player.transform.position + Vector3.up;
    }

    public override void EndFieldAct() // 필드전투가 종료될 때 실행
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