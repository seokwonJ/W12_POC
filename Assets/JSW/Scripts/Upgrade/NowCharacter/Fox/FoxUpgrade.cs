using UnityEditor.Searcher;
using UnityEngine;

[CreateAssetMenu(fileName = "FoxUpgrade", menuName = "Upgrades/FoxUpgrade")]
public class FoxUpgrade : CharacterUpgrade
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
        ManaRegenSpeedUPAbilityPowerUp ,            // 마나 회복 속도 증가 + 스킬 대미지 증가

        TenAttackSkillDamageUp,                     // 기본공격을 10회 후 다음 스킬의 대미지가 대폭 증가합니다
        AttackEnemyDefenseDown,                     // 여우의 공격이 적의 방어력을 감소시킵니다
        AttackEnemySpeedDown,                       // 여우의 공격을 맞을때마다 적의 이동속도가느려짐
        AttackMoreFarAway,                          // 원혼구슬 이 더 멀리 나간다
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        Fox fox = character.GetComponent<Fox>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                fox.attackPowerUpNum += attackPowerUpPercent;                                       // 기본 데미지 상승
                Debug.Log("Debug0 fox");
                break;
            case UpgradeType.AttackSpeedUp:
                fox.attackSpeedUpNum += attackSpeedUpPercent;                                       // 평타 간격
                Debug.Log("Debug1 fox");
                fox.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                fox.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 fox");
                fox.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                fox.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 fox");
                fox.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                fox.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 fox");
                fox.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                fox.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 fox");
                fox.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                fox.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 fox");
                fox.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                fox.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 fox");
                fox.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                fox.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                fox.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 fox");
                fox.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                        // 마나 회복 속도 증가 + 스킬 대미지 증가
                fox.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                fox.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 fox");
                fox.upgradeNum = 9;
                break;

            //-------------- 특수 업그레이드 --------------

            case UpgradeType.TenAttackSkillDamageUp:                                                    // 기본공격을 10회 후 다음 스킬의 대미지가 대폭 증가합니다
                fox.isUpgradeTenAttackSkillDamageUp = true;
                Debug.Log("Debug10 archer");
                fox.upgradeNum = 10;
                break;
            case UpgradeType.AttackEnemyDefenseDown:                                                    // 여우의 공격이 적의 방어력을 감소시킵니다
                fox.isUpgradeAttackEnemyDefenseDown = true;
                Debug.Log("Debug11 archer");
                fox.upgradeNum = 11;
                break;
            case UpgradeType.AttackEnemySpeedDown:                                                      // 여우의 공격을 맞을때마다 적의 이동속도가느려짐
                fox.isUpgradeAttackEnemySpeedDown = true;
                Debug.Log("Debug12 archer");
                fox.upgradeNum = 12;
                break;
            case UpgradeType.AttackMoreFarAway:                                                         // 원혼구슬 이 더 멀리 나간다
                fox.attackDuration += 0.5f;
                Debug.Log("Debug13 archer");
                fox.upgradeNum = 13;
                break;

        }

        Debug.Log("업그레이드 성공");
    }
}
