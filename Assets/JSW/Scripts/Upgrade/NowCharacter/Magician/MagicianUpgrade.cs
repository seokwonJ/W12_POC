using UnityEngine;

[CreateAssetMenu(fileName = "MagicianUpgrade", menuName = "Upgrades/MagicianUpgrade")]
public class MagicianUpgrade : CharacterUpgrade
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
        Magician magician = character.GetComponent<Magician>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                magician.AttackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 magician");
                break;
            case UpgradeType.AttackSpeedUp:
                magician.AttackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 magician");
                magician.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                magician.ProjectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 magician");
                magician.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                magician.ProjectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 magician");
                magician.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                magician.KnockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 magician");
                magician.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                magician.CriticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 magician");
                magician.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                magician.CriticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 magician");
                magician.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                magician.AttackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 magician");
                magician.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                magician.ManaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                magician.AttackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 magician");
                magician.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAttackPowerDown:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                magician.ManaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                magician.AttackPowerUpNum += ManaRegenSpeedUPAttackPowerDown_AttackPowerPercent;
                Debug.Log("Debug9 magician");
                magician.upgradeNum = 9;
                break;
                //-------------- 특수 업그레이드 --------------

        }

        Debug.Log("업그레이드 성공");
    }
}
