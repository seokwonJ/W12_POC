using UnityEditor.Searcher;
using UnityEngine;

[CreateAssetMenu(fileName = "NinjaUpgrade", menuName = "Upgrades/NinjaUpgrade")]
public class NinjaUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackPowerUp,                              // 기본 데미지 상승
        AttackSpeedUp,                              // 평타 간격
        ProjectileSpeedUp,                          // 투사체 이동속도 증가
        ProjectileSizeUp,                           // 탄 크기 증가
        KnockbackPowerUp,                           // 적 밀어내기 효율 증가
        CriticalProbabilityUp,                      // 크리 확률 상승
        CriticalDamageUp,                           // 크리 피해 배수 증가
        AttackRangeUp,                              // 적 감지/공격 가능 거리 확대
        ManaRegenSpeedDownAttackPowerUp,            // 마나 회복 속도 감소 + 공격력 증가
        ManaRegenSpeedUPAbilityPowerUp             // 마나 회복 속도 증가 + 스킬 대미지 증가
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        Ninja ninja = character.GetComponent<Ninja>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                ninja.attackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 ninja");
                break;
            case UpgradeType.AttackSpeedUp:
                ninja.attackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 ninja");
                ninja.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                ninja.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 ninja");
                ninja.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                ninja.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 ninja");
                ninja.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                ninja.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 ninja");
                ninja.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                ninja.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 ninja");
                ninja.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                ninja.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 ninja");
                ninja.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                ninja.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 ninja");
                ninja.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                ninja.manaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                ninja.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 ninja");
                ninja.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                ninja.manaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                ninja.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerDown_AbilityPowerPercent;
                Debug.Log("Debug9 ninja");
                ninja.upgradeNum = 9;
                break;
                //-------------- 특수 업그레이드 --------------

        }

        Debug.Log("업그레이드 성공");
    }
}
