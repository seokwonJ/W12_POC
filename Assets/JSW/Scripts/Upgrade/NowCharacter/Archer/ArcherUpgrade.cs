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
        ManaRegenSpeedUPAbilityPowerUp,             // 마나 회복 속도 증가 + 스킬 대미지 증가
        
        SkillCountUp,                               // 스킬을 1회 더 시전합니다.
        AttackProjectileUp,                         // 기본 공격이 투사체 1개를 더 발사합니다.(기존 3 갈래 화살 )
        DieInstantly,                               // 보스를 제외한 몬스터 타격 시 일정 확률로 즉사 시킵니다
        SameEnemyDamageUp,                          // 같은 대상을 공격 할 시 일정시간 대미지가 증가합니다. 지속시간 : N초
        SkillProjectileCountUp                      // 스킬 투사체 갯수 증가
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        UpgradeController upgradeController = character.GetComponent<UpgradeController>();
        upgradeController.ApplyUpgrade(this, character);
        Archer archer = character.GetComponent<Archer>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                archer.attackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 archer");
                break;
            case UpgradeType.AttackSpeedUp:
                archer.attackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 archer");
                archer.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                archer.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 archer");
                archer.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                archer.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 archer");
                archer.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                archer.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 archer");
                archer.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                archer.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 archer");
                archer.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                archer.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 archer");
                archer.upgradeNum = 6;          
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                archer.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 archer");
                archer.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                archer.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                archer.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 archer");
                archer.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                archer.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                archer.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 archer");
                archer.upgradeNum = 9;
                break;

            //-------------- 특수 업그레이드 --------------

            case UpgradeType.SkillCountUp:                                                          // 스킬을 1회 더 시전합니다.
                archer.skillCount += 1;
                Debug.Log("Debug10 archer");
                archer.upgradeNum = 10;
                break;
            case UpgradeType.AttackProjectileUp:                                                    // 기본 공격이 투사체 1개를 더 발사합니다.(기존 3 갈래 화살 )
                archer.isUpgradeTwoShot = true;
                Debug.Log("Debug11 archer");
                archer.upgradeNum = 11;
                break;
            case UpgradeType.DieInstantly:                                                          // 보스를 제외한 몬스터 타격 시 일정 확률로 즉사 시킵니다
                archer.isUpgradeDieInstantly = true;
                Debug.Log("Debug12 archer");
                archer.upgradeNum = 12;
                break;
            case UpgradeType.SameEnemyDamageUp:                                                     // 같은 대상을 공격 할 시 일정시간 대미지가 증가합니다. 지속시간 : N초
                archer.isUpgradeSameEnemyDamage = true;
                Debug.Log("Debug13 archer");
                archer.upgradeNum = 13;
                break;
            case UpgradeType.SkillProjectileCountUp:                                                // 스킬 투사체 갯수 증가
                archer.skillProjectileCount += 6;
                Debug.Log("Debug14 archer");
                archer.upgradeNum = 14;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}