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
        ManaRegenSpeedUPAbilityPowerUp,             // 마나 회복 속도 증가 + 스킬 대미지 증가

        TenAttackSkillAttack,                       // 기본공격을 10회 후 평타로 스킬 투사체를 발사한다
        SkillAttackCountUp,                         // 스킬을 1회 더 시전합니다
        SkillAttackSizeUp,                          // 스킬 투사체의 크기가 증가합니다
        SkillExplosionAttack,                         // 지속시간이 끝나면 터지며 대미지를 준다
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        UpgradeController upgradeController = character.GetComponent<UpgradeController>();
        upgradeController.ApplyUpgrade(this, character);
        Magician magician = character.GetComponent<Magician>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                magician.attackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 magician");
                break;
            case UpgradeType.AttackSpeedUp:
                magician.attackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 magician");
                magician.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                magician.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 magician");
                magician.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                magician.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 magician");
                magician.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                magician.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 magician");
                magician.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                magician.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 magician");
                magician.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                magician.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 magician");
                magician.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                magician.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 magician");
                magician.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                magician.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                magician.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 magician");
                magician.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                magician.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                magician.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 magician");
                magician.upgradeNum = 9;
                break;

            //-------------- 특수 업그레이드 --------------

            case UpgradeType.TenAttackSkillAttack:                                                  // 기본공격을 10회 후 평타로 스킬 투사체를 발사한다
                magician.isUpgradeTenAttackSkillAttack = true;
                Debug.Log("Debug10 magician");
                magician.upgradeNum = 10;
                break;
            case UpgradeType.SkillAttackCountUp:                                                    // 스킬을 1회 더 시전합니다
                magician.skillCount += 1;
                Debug.Log("Debug11 magician");
                magician.upgradeNum = 11;
                break;
            case UpgradeType.SkillAttackSizeUp:                                                     // 스킬 투사체의 크기가 증가합니다
                magician.skillSize += 2;
                Debug.Log("Debug12 magician");
                magician.upgradeNum = 12;
                break;
            case UpgradeType.SkillExplosionAttack:                                                  // 지속시간이 끝나면 터지며 대미지를 준다
                magician.isUpgradeSkillExplosionAttack = true;
                Debug.Log("Debug13 magician");
                magician.upgradeNum = 13;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}
