using UnityEngine;

[CreateAssetMenu(fileName = "LeviUpgrade", menuName = "Upgrades/LeviUpgrade")]
public class LeviUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // 일반 공격 속도 증가 (쿨타임 감소)
        AttackPower,       // 공격력 증가
        ManaRegen,         // 마나 초당 증가량 증가
        AttackWhileFalling,     // 떨어지면서도 공격
        LongerProjectileDuration,   // 공격 투사체 지속시간 증가
        MoreSkillTargets,           // 스킬 타겟 증가
        GainPowerFromSkillDamage,   // 스킬 시 데미지 준 적 비례해서 일시적 공격력 상승
        ManaOnLandingBasedOnTimeAway,   // 배와 떨어진 시간에 비례하여 착지시 마나 부여
        AttackSpeedBoostAfterQuickReboard,  // 스킬 쓰고 3초 안에 탑승 시 일시적인 공격속도 상승
        MoreUltimateDamageWithJumpPower     // 점프에 비례해서 스킬 데미지 증가
    }

    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        Levi levi = character.GetComponent<Levi>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                levi.normalFireInterval -= 0.1f;              // 공격 쿨타임 0.1초 감소
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AttackPower:
                levi.attackDamage += 10;                    // ad 10 증가
                Debug.Log("Debug1 Archer");
                levi.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // 초당 mp 증가량 3증가
                levi.mpPerSecond += 3;
                Debug.Log("Debug2 Archer");
                levi.upgradeNum = 2;
                break;
            case UpgradeType.AttackWhileFalling:                     // 떨어지면서도 공격
                levi.isAttackWhileFalling = true;
                Debug.Log("Debug3 Archer");
                levi.upgradeNum = 3;
                break;
            case UpgradeType.LongerProjectileDuration:                //  공격 투사체 지속시간 증가
                levi.NormalAttackProjectileDuration += 2;
                Debug.Log("Debug4 Archer");
                levi.upgradeNum = 4;
                break;
            case UpgradeType.MoreSkillTargets:                 // 스킬 타겟 증가
                levi.skillTargetCount += 2;
                Debug.Log("Debug5 Archer");
                levi.upgradeNum = 5;
                break;
            case UpgradeType.GainPowerFromSkillDamage:               // 스킬 시 데미지 준 적 비례해서 일시적 공격력 상승
                levi.isGainPowerFromSkillDamage = true;
                Debug.Log("Debug6 Archer");
                levi.upgradeNum = 6;
                break;
            case UpgradeType.ManaOnLandingBasedOnTimeAway:                   // 배와 떨어진 시간에 비례하여 착지시 마나 부여
                levi.isManaOnLandingBasedOnTimeAway = true;
                Debug.Log("Debug7 Archer");
                levi.upgradeNum = 7;
                break;
            case UpgradeType.AttackSpeedBoostAfterQuickReboard:                   // 스킬 쓰고 3초 안에 탑승 시 일시적인 공격속도 상승
                levi.isAttackSpeedBoostAfterQuickReboard = true;
                Debug.Log("Debug8 Archer");
                levi.upgradeNum = 8;
                break;
            case UpgradeType.MoreUltimateDamageWithJumpPower:                    // 일반 공격 함수에 조건문으로 걸려있는 tripleshot true로 바꿈
                levi.isMoreUltimateDamageWithJumpPower = true;
                Debug.Log("Debug9 Archer");
                levi.upgradeNum = 9;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}
