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
        ManaRegenSpeedUPAbilityPowerUp,             // 마나 회복 속도 증가 + 스킬 대미지 증가

        AttackNumPlusSkillDamageDown,               // 1번의 공격에 1번 공격을 추가합니다 스킬 대미지가 대폭 감소합니다
        SkillEndPlusSkillCountUp,                   // 착지를 할 때 마다 스킬 대상을 1회 추가합니다 최대 n회까지 증가합니다.(업그레이드 할때 마다 최대치 증가 )
        AttackDurationUp,                           // 검기가 더 멀리 나간다 (실제 : 공격의 지속시간이 증가한다 )
        AttackPerDamageMinusNumMinus,               // 관통 될 때 마다 감소하는 대미지가 줄어든다
        SkillingHitAttack                           // 스킬로 지나가는 동선에도 대미지를 준다
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

            case UpgradeType.AttackNumPlusSkillDamageDown:                                          // 1번의 공격에 1번 공격을 추가합니다 스킬 대미지가 대폭 감소합니다
                levi.attackNum += 1;
                levi.abilityPower -= 100;
                Debug.Log("Debug10 archer");
                levi.upgradeNum = 10;
                break;
            case UpgradeType.SkillEndPlusSkillCountUp:                                               // 착지를 할 때 마다 스킬 대상을 1회 추가합니다 최대 n회까지 증가합니다.(업그레이드 할때 마다 최대치 증가 )
                levi.isSkillEndPlusSkillCountUp = true;
                Debug.Log("Debug11 archer");
                levi.upgradeNum = 11;
                break;
            case UpgradeType.AttackDurationUp:                                                       // 검기가 더 멀리 나간다 (실제 : 공격의 지속시간이 증가한다 )
                levi.NormalAttackProjectileDuration += 1;
                Debug.Log("Debug12 archer");
                levi.upgradeNum = 12;
                break;
            case UpgradeType.AttackPerDamageMinusNumMinus:                                           // 관통 될 때 마다 감소하는 대미지가 줄어든다
                levi.attackPerDamageMinus -= 3;
                Debug.Log("Debug13 archer");
                levi.upgradeNum = 13;
                break;
            case UpgradeType.SkillingHitAttack:                                                      // 스킬로 지나가는 동선에도 대미지를 준다
                levi.isAttackWhileSkillUpgrade = true;
                Debug.Log("Debug14 archer");
                levi.upgradeNum = 14;
                break;

        }

        Debug.Log("업그레이드 성공");
    }
}
