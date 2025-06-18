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
        ManaRegenSpeedUPAbilityPowerUp,             // 마나 회복 속도 증가 + 스킬 대미지 증가

        FallingSpeedMaxUp,                          // 떨어지는 속도 최대치 증가
        SkillRangeUp,                               // 궁극기 범위 증가
        FallingSpeedPerSkillDamageUp,               // 떨어지는 속도가 빠를수록 궁극기 대미지 증가
        RidingDefenseUp,                            // 배에 타고 있으면 방어력이 증가
        SkillNearKnockbackDamageUp,                 // 스킬이 가까운 대상일수록 넉백과 대미지가 증가
        SkillEnemySpeedDown                         // 맞은 대상의 이동속도를 N% 감소
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        Tanker tanker = character.GetComponent<Tanker>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                tanker.attackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 tanker");
                break;
            case UpgradeType.AttackSpeedUp:
                tanker.attackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 tanker");
                tanker.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                tanker.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 tanker");
                tanker.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                tanker.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 tanker");
                tanker.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                tanker.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 tanker");
                tanker.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                tanker.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 tanker");
                tanker.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                tanker.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 tanker");
                tanker.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                tanker.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 tanker");
                tanker.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                tanker.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                tanker.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 tanker");
                tanker.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                tanker.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                tanker.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 tanker");
                tanker.upgradeNum = 9;
                break;

            //-------------- 특수 업그레이드 --------------

            case UpgradeType.FallingSpeedMaxUp:                                                    // 떨어지는 속도 최대치 증가
                tanker.maxFallSpeed += 10;
                Debug.Log("Debug10 tanker");
                tanker.upgradeNum = 10;
                break;
            case UpgradeType.SkillRangeUp:                                                         // 궁극기 범위 증가
                tanker.skillRange += 2;
                Debug.Log("Debug11 tanker");
                tanker.upgradeNum = 11;
                break;
            case UpgradeType.FallingSpeedPerSkillDamageUp:                                         // 떨어지는 속도가 빠를수록 궁극기 대미지 증가
                tanker.isUpgradeFallingSpeedToSkillDamage = true;
                Debug.Log("Debug12 tanker");
                tanker.upgradeNum = 12;
                break;
            case UpgradeType.RidingDefenseUp:                                                      // 배에 타고 있으면 방어력이 증가
                tanker.isUpgradeRidingDefenseUp = true;
                Debug.Log("Debug13 tanker");
                tanker.upgradeNum = 13;
                break;
            case UpgradeType.SkillNearKnockbackDamageUp:                                           // 스킬이 가까운 대상일수록 넉백과 대미지가 증가
                tanker.isCloserMoreDamage = true;
                Debug.Log("Debug14 tanker");
                tanker.upgradeNum = 14;
                break;
            case UpgradeType.SkillEnemySpeedDown:                                                  // 맞은 대상의 이동속도를 N% 감소
                tanker.isSkillEnemySpeedDown = true;
                Debug.Log("Debug14 tanker");
                tanker.upgradeNum = 15;
                break;

        }

        Debug.Log("업그레이드 성공");
    }
}
