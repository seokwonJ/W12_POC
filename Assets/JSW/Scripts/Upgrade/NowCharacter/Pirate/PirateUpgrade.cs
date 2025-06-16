using UnityEngine;

[CreateAssetMenu(fileName = "PirateUpgrade", menuName = "Upgrades/PirateUpgrade")]
public class PirateUpgrade : CharacterUpgrade
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
        ManaRegenSpeedUPAttackPowerDown             // 마나 회복 속도 증가 + 스킬 대미지 증가
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        Pirate pirate = character.GetComponent<Pirate>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                pirate.AttackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 pirate");
                break;
            case UpgradeType.AttackSpeedUp:
                pirate.AttackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 pirate");
                pirate.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                pirate.ProjectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 pirate");
                pirate.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                pirate.ProjectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 pirate");
                pirate.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                pirate.KnockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 pirate");
                pirate.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                pirate.CriticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 pirate");
                pirate.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                pirate.CriticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 pirate");
                pirate.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                pirate.AttackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 pirate");
                pirate.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                pirate.ManaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                pirate.AttackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 pirate");
                pirate.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAttackPowerDown:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                pirate.ManaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                pirate.AttackPowerUpNum += ManaRegenSpeedUPAttackPowerDown_AttackPowerPercent;
                Debug.Log("Debug9 pirate");
                pirate.upgradeNum = 9;
                break;
                //-------------- 특수 업그레이드 --------------

        }

        Debug.Log("업그레이드 성공");
    }
}