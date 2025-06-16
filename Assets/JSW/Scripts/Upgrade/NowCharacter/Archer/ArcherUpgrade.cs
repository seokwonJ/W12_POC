using UnityEngine;

[CreateAssetMenu(fileName = "ArcherUpgrade", menuName = "Upgrades/ArcherUpgrade")]
public class ArcherUpgrade : CharacterUpgrade
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
        Archer archer = character.GetComponent<Archer>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                archer.AttackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AttackSpeedUp:
                archer.AttackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 Archer");
                archer.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                archer.ProjectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 Archer");
                archer.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                archer.ProjectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 Archer");
                archer.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                archer.KnockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 Archer");
                archer.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                archer.CriticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 Archer");
                archer.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                archer.CriticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 Archer");
                archer.upgradeNum = 6;          
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                archer.AttackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 Archer");
                archer.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                archer.ManaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                archer.AttackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 Archer");
                archer.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAttackPowerDown:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                archer.ManaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                archer.AttackPowerUpNum += ManaRegenSpeedUPAttackPowerDown_AttackPowerPercent;
                Debug.Log("Debug9 Archer");
                archer.upgradeNum = 9;
                break;
            //-------------- 특수 업그레이드 --------------

        }

        Debug.Log("업그레이드 성공");
    }
}