using UnityEditor.Searcher;
using UnityEngine;


[CreateAssetMenu(fileName = "BlackHoleUpgrade", menuName = "Upgrades/BlackHoleUpgrade")]
public class BlackHoleUpgrade : CharacterUpgrade
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

        SkillDurationUp,                            // 스킬의 지속시간 증가.
        SkillAttackDlaySpeedUp,                     // 블랙홀이 대미지를 주는 주기가 더 빨라진다
        SkillSizeDownExplosion,                     // 블랙홀의 사이즈가 1/2로 줄어드는 대신 블랙홀이 사라지기 전에 폭발해서 대미지를 준다
        SkillDenfenseDown,                          // 블랙홀의 공격이 방어력을 감소 시킵니다
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        BlackHole blackHole = character.GetComponent<BlackHole>();
        switch (type)
        {
            //-------------- 기본 업그레이드 --------------
            case UpgradeType.AttackPowerUp:
                blackHole.attackPowerUpNum += attackPowerUpPercent;                                    // 기본 데미지 상승
                Debug.Log("Debug0 blackHole");
                break;
            case UpgradeType.AttackSpeedUp:
                blackHole.attackSpeedUpNum += attackSpeedUpPercent;                                    // 평타 간격
                Debug.Log("Debug1 blackHole");
                blackHole.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // 투사체 이동속도 증가
                blackHole.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 blackHole");
                blackHole.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // 탄 크기 증가
                blackHole.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 blackHole");
                blackHole.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // 적 밀어내기 효율 증가
                blackHole.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 blackHole");
                blackHole.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // 크리 확률 상승
                blackHole.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 blackHole");
                blackHole.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // 크리 피해 배수 증가
                blackHole.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 blackHole");
                blackHole.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // 적 감지/공격 가능 거리 확대
                blackHole.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 blackHole");
                blackHole.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // 마나 회복 속도 감소 + 공격력 증가
                blackHole.manaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                blackHole.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 blackHole");
                blackHole.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // 마나 회복 속도 증가 + 스킬 대미지 증가
                blackHole.manaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                blackHole.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerDown_AbilityPowerPercent;
                Debug.Log("Debug9 blackHole");
                blackHole.upgradeNum = 9;
                break;

            //-------------- 특수 업그레이드 --------------

            case UpgradeType.SkillDurationUp:                                                       // 스킬의 지속시간 증가.
                blackHole.skillDuration += 2;
                Debug.Log("Debug10 blackHole");
                blackHole.upgradeNum = 10;
                break;
            case UpgradeType.SkillAttackDlaySpeedUp:                                                // 블랙홀이 대미지를 주는 주기가 더 빨라진다
                blackHole.skillPullInterval -= 0.1f;
                Debug.Log("Debug11 blackHole");
                blackHole.upgradeNum = 11;
                break;
            case UpgradeType.SkillSizeDownExplosion:                                                // 블랙홀의 사이즈가 1/2로 줄어드는 대신 블랙홀이 사라지기 전에 폭발해서 대미지를 준다
                blackHole.isUpgradeSkillSizeDownExplosion = true;
                Debug.Log("Debug12 blackHole");
                blackHole.upgradeNum = 12;
                break;
            case UpgradeType.SkillDenfenseDown:                                                     // 블랙홀의 공격이 방어력을 감소 시킵니다
                blackHole.isUpgradeSkillEnemyDenfenseDown = true;
                Debug.Log("Debug13 blackHole");
                blackHole.upgradeNum = 13;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}
