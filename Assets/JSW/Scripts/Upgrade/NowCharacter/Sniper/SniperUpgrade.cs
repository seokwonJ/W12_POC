using UnityEditor.Searcher;
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
        ManaRegenSpeedUPAbilityPowerUp,             // 마나 회복 속도 증가 + 스킬 대미지 증가

        SkillCountUp,                               // 스킬의 투사체 + 1
        ReloadTimeDown,                             // 무한탄창 : 장전 시간이 N% 줄어든다
        NoMoreSkill,                                // 더 이상 스킬을 사용 할 수 없다(마나 재생량이 0이 된다 )
        NoMorePenetrationAttackUp,                  // 더 이상 관통이 되지 않지만 공격력이 비약적으로 증가한다
        PenetrationPerDamageUp                      // 관통 될 때마다 대미지가 증가한다
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
                sniper.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                sniper.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 sniper");
                sniper.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                        // 마나 회복 속도 증가 + 스킬 대미지 증가
                sniper.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                sniper.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 sniper");
                sniper.upgradeNum = 9;
                break;


            //-------------- 특수 업그레이드 --------------

            case UpgradeType.SkillCountUp:                                                          // 스킬의 투사체 + 1
                sniper.skillCount += 1;
                Debug.Log("Debug10 archer");
                sniper.upgradeNum = 10;
                break;
            case UpgradeType.ReloadTimeDown:                                                        // 무한탄창 : 장전 시간이 N% 줄어든다
                sniper.realoadTime -= sniper.realoadTime * 0.3f;
                Debug.Log("Debug11 archer");
                sniper.upgradeNum = 11;
                break;
            case UpgradeType.NoMoreSkill:                                                           // 더 이상 스킬을 사용 할 수 없다(마나 재생량이 0이 된다 )
                sniper.mpPerSecond = 0;
                Debug.Log("Debug12 archer");
                sniper.upgradeNum = 12;
                break;
            case UpgradeType.NoMorePenetrationAttackUp:                                             // 더 이상 관통이 되지 않지만 공격력이 비약적으로 증가한다
                sniper.isNoMorePenetrationAttackUp = true;
                sniper.attackBase += 30;
                Debug.Log("Debug13 archer");
                sniper.upgradeNum = 13;
                break;
            case UpgradeType.PenetrationPerDamageUp:                                                // 관통 될 때마다 대미지가 증가한다
                sniper.isPenetrationPerDamageUp = true;
                Debug.Log("Debug13 archer");
                sniper.upgradeNum = 13;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}
