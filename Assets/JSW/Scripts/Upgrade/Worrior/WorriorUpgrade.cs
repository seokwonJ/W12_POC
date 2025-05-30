using UnityEditor.Searcher;
using UnityEngine;

[CreateAssetMenu(fileName = "WorriorUpgrade", menuName = "Upgrades/WorriorUpgrade")]
public class WorriorUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // 일반 공격 속도 증가 (쿨타임 감소)
        AttackPower,       // 공격력 증가
        ManaRegen,         // 마나 초당 증가량 증가
        NormalAttackSize,         // 일반 공격 크기 증가
        NormalProjectileSpeed,    // 일반공격 투사체 속도 증가
        FallSpeed,     // 떨어지는 속도 증가
        SkillProjectileCount,   // 궁극기 날리는 갯수 증가
        NormalProjectileLifetime,       // 일반공격 라이프타임 증가
        CollisionOnFallDamage,        // 떨어지면서 적과 부딪힐 경우 데미지 입힘 
        ShipDamageReduction         // 배에 타있으면 배가 받는 데미지 피해 줄여줌
    }
    public UpgradeType type;
    public float value;


    public override void ApplyUpgrade(GameObject character)
    {
        Worrior worrior = character.GetComponent<Worrior>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                worrior.normalFireInterval -= 0.1f;              // 공격 쿨타임 0.1초 감소
                Debug.Log("Debug0 Worrior");
                break;
            case UpgradeType.AttackPower:
                worrior.attackDamage += 10;                    // ad 10 증가
                Debug.Log("Debug1 Worrior");
                worrior.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // 초당 mp 증가량 3증가
                worrior.mpPerSecond += 3;
                Debug.Log("Debug2 Worrior");
                worrior.upgradeNum = 2;
                break;
            case UpgradeType.NormalAttackSize:                     // nomalAttackSize  1.5배 증가
                worrior.nomalAttackSize *= 1.5f;
                Debug.Log("Debug3 Worrior");
                worrior.upgradeNum = 3;
                break;
            case UpgradeType.NormalProjectileSpeed:                    // 투사체 속도 15증가
                Debug.Log("Debug9 Worrior");
                worrior.upgradeNum = 4;
                break;
            case UpgradeType.FallSpeed:                         // maxfallsize  -10
                worrior.maxFallSpeed += 10;
                Debug.Log("Debug4 Worrior");
                worrior.upgradeNum = 5;
                break;
            case UpgradeType.SkillProjectileCount:                 // 스킬 횟수가 +2 증가
                worrior.skillCount += 2;
                Debug.Log("Debug5 Worrior");
                worrior.upgradeNum = 5;
                break;
            case UpgradeType.NormalProjectileLifetime:               // attack생존시간 5플러스
                worrior.nomalAttackLifetime += 5;
                Debug.Log("Debug6 Worrior");
                worrior.upgradeNum = 7;
                break;
            case UpgradeType.CollisionOnFallDamage:                   //  떨어지면서 적과 부딪힐 경우 데미지 입힘 true로 바꿔줌
                worrior.isfallingCanAttack = true;
                Debug.Log("Debug7 Worrior");
                worrior.upgradeNum = 8;
                break;
            case UpgradeType.ShipDamageReduction:                    // 배에 타있으면 배가 받는 데미지 피해 줄여줌 true로 바꿔줌
                worrior.isShieldFlyer = true;
                Debug.Log("Debug8 Worrior");
                worrior.upgradeNum = 9;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}
