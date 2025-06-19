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
        ManaRegenSpeedUPAbilityPowerUp,             // 마나 회복 속도 증가 + 스킬 대미지 증가

        SkillProjectileCountUp,                     // 스킬의 투사체 +3
        AttackPerMana,                              // 기본 공격 적중시 마나를 n% 채워준다
        ManaMultipleSkillProjectileMultiple,        // 최대 마나량 2배 해적의 스킬 투사체가 2배
        NoMoreExplosionAttackDamageUp,              // 더 이상 대포가 터지면서 범위 대미지를 주지 않음 공격력이 대폭 증가한다
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        UpgradeController upgradeController = character.GetComponent<UpgradeController>();
        upgradeController.ApplyUpgrade(this, character);
        Pirate pirate = character.GetComponent<Pirate>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                pirate.attackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 pirate");
                break;
            case UpgradeType.AttackSpeedUp:
                pirate.attackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 pirate");
                pirate.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                pirate.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 pirate");
                pirate.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                pirate.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 pirate");
                pirate.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                pirate.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 pirate");
                pirate.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                pirate.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 pirate");
                pirate.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                pirate.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 pirate");
                pirate.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                pirate.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 pirate");
                pirate.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                pirate.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                pirate.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 pirate");
                pirate.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                pirate.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                pirate.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 pirate");
                pirate.upgradeNum = 9;
                break;
            //-------------- 특수 업그레이드 --------------


            case UpgradeType.SkillProjectileCountUp:                                                // 스킬의 투사체 +3
                pirate.skillShotCount += 3;
                Debug.Log("Debug10 pirate");
                pirate.upgradeNum = 10;
                break;
            case UpgradeType.AttackPerMana:                                                         // 기본 공격 적중시 마나를 n% 채워준다
                pirate.isAttackPerMana = true;
                Debug.Log("Debug11 pirate");
                pirate.upgradeNum = 11;
                break;
            case UpgradeType.ManaMultipleSkillProjectileMultiple:                                   // 최대 마나량 2배 해적의 스킬 투사체가 2배
                pirate.isManaMultipleSkillProjectileMultiple = true;
                Debug.Log("Debug12 pirate");
                pirate.upgradeNum = 12;
                break;
            case UpgradeType.NoMoreExplosionAttackDamageUp:                                         // 더 이상 대포가 터지면서 범위 대미지를 주지 않음 공격력이 대폭 증가한다
                pirate.isManaMultipleSkillProjectileMultiple = true;
                pirate.attackBase += 100;
                Debug.Log("Debug13 pirate");
                pirate.upgradeNum = 13;
                break;

        }

        Debug.Log("업그레이드 성공");
    }
}