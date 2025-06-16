using UnityEditor.Searcher;
using UnityEngine;

[CreateAssetMenu(fileName = "LeviUpgrade", menuName = "Upgrades/LeviUpgrade")]
public class LeviUpgrade : CharacterUpgrade
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
        Levi levi = character.GetComponent<Levi>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                levi.attackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 levi");
                break;
            case UpgradeType.AttackSpeedUp:
                levi.attackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 levi");
                levi.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                levi.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 levi");
                levi.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                levi.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 levi");
                levi.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                levi.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 levi");
                levi.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                levi.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 levi");
                levi.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                levi.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 levi");
                levi.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                levi.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 levi");
                levi.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                levi.manaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                levi.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 levi");
                levi.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                levi.manaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                levi.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerDown_AbilityPowerPercent;
                Debug.Log("Debug9 levi");
                levi.upgradeNum = 9;
                break;
                //-------------- 특수 업그레이드 --------------

        }

        Debug.Log("업그레이드 성공");
    }
}
