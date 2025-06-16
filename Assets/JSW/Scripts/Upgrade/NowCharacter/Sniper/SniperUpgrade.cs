using UnityEngine;

[CreateAssetMenu(fileName = "SniperUpgrade", menuName = "Upgrades/SniperUpgrade")]
public class SniperUpgrade : CharacterUpgrade
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
        Sniper sniper = character.GetComponent<Sniper>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                sniper.attackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 sniper");
                break;
            case UpgradeType.AttackSpeedUp:
                sniper.attackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 sniper");
                sniper.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                sniper.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 sniper");
                sniper.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                sniper.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 sniper");
                sniper.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                sniper.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 sniper");
                sniper.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                sniper.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 sniper");
                sniper.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                sniper.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 sniper");
                sniper.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                sniper.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 sniper");
                sniper.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                sniper.manaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                sniper.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 sniper");
                sniper.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                sniper.manaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                sniper.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerDown_AbilityPowerPercent;
                Debug.Log("Debug9 sniper");
                sniper.upgradeNum = 9;
                break;
                //-------------- 특수 업그레이드 --------------

        }

        Debug.Log("업그레이드 성공");
    }
}
