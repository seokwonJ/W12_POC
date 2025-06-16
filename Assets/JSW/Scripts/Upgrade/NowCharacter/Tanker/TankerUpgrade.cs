using UnityEngine;

[CreateAssetMenu(fileName = "TankerUpgrade", menuName = "Upgrades/TankerUpgrade")]
public class TankerUpgrade : CharacterUpgrade
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
        Tanker tanker = character.GetComponent<Tanker>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                tanker.AttackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 tanker");
                break;
            case UpgradeType.AttackSpeedUp:
                tanker.AttackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 tanker");
                tanker.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                tanker.ProjectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 tanker");
                tanker.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                tanker.ProjectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 tanker");
                tanker.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                tanker.KnockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 tanker");
                tanker.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                tanker.CriticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 tanker");
                tanker.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                tanker.CriticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 tanker");
                tanker.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                tanker.AttackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 tanker");
                tanker.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                tanker.ManaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                tanker.AttackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 tanker");
                tanker.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAttackPowerDown:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                tanker.ManaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                tanker.AttackPowerUpNum += ManaRegenSpeedUPAttackPowerDown_AttackPowerPercent;
                Debug.Log("Debug9 tanker");
                tanker.upgradeNum = 9;
                break;
                //-------------- 특수 업그레이드 --------------

        }

        Debug.Log("업그레이드 성공");
    }
}
