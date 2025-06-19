using UnityEngine;

[CreateAssetMenu(fileName = "LightSoldierUpgrade", menuName = "Upgrades/LightSoldierUpgrade")]
public class LightSoldierUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // 일반 공격 속도 증가 (쿨타임 감소)
        AttackPower,       // 공격력 증가
        ManaRegen,         // 마나 초당 증가량 증가
        LargerNormalAttack,         // 일반공격 크기 커짐
        Fires4NormalAttackProjectiles,    // 일반공격 4개 날라감
        LongerNormalAttackDuration,     // 일반 공격 지속시간 증가
        IncreasedSkillCount,   // 궁 갯수 증가 
        LargerSkillSize,       // 궁 크기가 커짐
        Gain1ManaPerHit,        // 평타로 맞힌 적 당 마나 1획득
        FasterFallSpeed         // 떨어지는 속도 증가
    }

    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        LightSoldier lightSoldier = character.GetComponent<LightSoldier>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                lightSoldier.normalFireInterval -= 0.2f;              // 공격 쿨타임 0.1초 감소
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AttackPower:
                lightSoldier.attackDamage += 10;                    // ad 10 증가
                Debug.Log("Debug1 Archer");
                lightSoldier.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // 초당 mp 증가량 3증가
                lightSoldier.mpPerSecond += 3;
                Debug.Log("Debug2 Archer");
                lightSoldier.upgradeNum = 2;
                break;
            case UpgradeType.LargerNormalAttack:                    // 일반공격 크기 커짐
                lightSoldier.normalAttackSize += 2f;
                Debug.Log("Debug3 Archer");
                lightSoldier.upgradeNum = 3;
                break;
            case UpgradeType.Fires4NormalAttackProjectiles:                // 일반공격 4개 날라감
                lightSoldier.isFires4NormalAttackProjectiles = true;
                Debug.Log("Debug4 Archer");
                lightSoldier.upgradeNum = 4;
                break;
            case UpgradeType.LongerNormalAttackDuration:                 // 일반 공격 지속시간 증가
                lightSoldier.normalAttackLifetime += 3;
                Debug.Log("Debug5 Archer");
                lightSoldier.upgradeNum = 5;
                break;
            case UpgradeType.IncreasedSkillCount:               // 궁 갯수 증가 
                lightSoldier.skillShotCount += 6;
                Debug.Log("Debug6 Archer");
                lightSoldier.upgradeNum = 6;
                break;
            case UpgradeType.LargerSkillSize:                   // 궁 크기가 커짐
                lightSoldier.skillSize += 1;
                Debug.Log("Debug7 Archer");
                lightSoldier.upgradeNum = 7;
                break;
            case UpgradeType.Gain1ManaPerHit:                    // 평타로 맞힌 적 당 마나 1획득
                lightSoldier.isGain1ManaPerHit = true;
                Debug.Log("Debug8 Archer");
                lightSoldier.upgradeNum = 8;
                break;
            case UpgradeType.FasterFallSpeed:                    // 떨어지는 속도 증가
                lightSoldier.maxFallSpeed += 5;
                Debug.Log("Debug9 Archer");
                lightSoldier.upgradeNum = 9;
                break;
        }
        Debug.Log("업그레이드 성공");
    }
}


