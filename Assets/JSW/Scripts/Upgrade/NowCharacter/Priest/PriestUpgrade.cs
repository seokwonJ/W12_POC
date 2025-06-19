using UnityEngine;

[CreateAssetMenu(fileName = "PriestUpgrade", menuName = "Upgrades/PriestUpgrade")]
public class PriestUpgrade : CharacterUpgrade
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

        SkillDurationUp,                            // 스킬의 지속시간 증가
        SkillCharacterAttackUp,                     // 스킬 시전 시 아군 전체 공격력 증가
        SkillPlayerSpeedUp,                         // 스킬 시전 시 비행체의 이속을 % 증가시킴
        AttackEnemyDefenseDown,                     // 사제의 공격이 방어력을 감소 시킵니다
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        UpgradeController upgradeController = character.GetComponent<UpgradeController>();
        upgradeController.ApplyUpgrade(this, character);
        Priest priest = character.GetComponent<Priest>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                priest.attackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 priest");
                break;
            case UpgradeType.AttackSpeedUp:
                priest.attackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 priest");
                priest.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                priest.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 priest");
                priest.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                priest.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 priest");
                priest.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                priest.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 priest");
                priest.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                priest.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 priest");
                priest.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                priest.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 priest");
                priest.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                priest.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 priest");
                priest.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                priest.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                priest.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 priest");
                priest.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                priest.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                priest.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 priest");
                priest.upgradeNum = 9;
                break;

            //-------------- 특수 업그레이드 --------------

            case UpgradeType.SkillDurationUp:                                                          // 스킬의 지속시간 증가
                priest.skillDuration += 2;
                Debug.Log("Debug10 priest");
                priest.upgradeNum = 10;
                break;
            case UpgradeType.SkillCharacterAttackUp:                                                   // 스킬 시전 시 아군 전체 공격력 증가
                priest.isUpgradeSkillCharacterAttackUp = true;
                Debug.Log("Debug11 priest");
                priest.upgradeNum = 11;
                break;
            case UpgradeType.SkillPlayerSpeedUp:                                                       // 스킬 시전 시 비행체의 이속을 % 증가시킴
                priest.isUpgradeSkillPlayerSpeedUp = true;
                Debug.Log("Debug12 priest");
                priest.upgradeNum = 12;
                break;
            case UpgradeType.AttackEnemyDefenseDown:                                                   // 사제의 공격이 방어력을 감소 시킵니다
                priest.isUpgradeAttackEnemyDefenseDown = true;
                Debug.Log("Debug13 priest");
                priest.upgradeNum = 13;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}
