using UnityEngine;

[CreateAssetMenu(fileName = "FoxUpgrade", menuName = "Upgrades/FoxUpgrade")]
public class FoxUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // 일반 공격 속도 증가 (쿨타임 감소)
        AbilityPower,       // 주문력 증가
        ManaRegen,         // 마나 초당 증가량 증가
        MoreUltimateOrbs,         // 궁극기 구슬 증가
        ReturnDamageScalesWithHitCount,    // 갈 때 맞은 적 개수당 돌아올 때 데미지 증가
        IncreasedSpiritOrbRange,     // 원혼구슬 사정거리 증가
        EmpoweredAttackEvery3Hits,   // 평타 3번당  강한 평타
        MoreDamageBasedOnOnboardAllies,       // 현재 배에 타있는 동료에 비례해서 평타 데미지 증가
        OrbPausesBeforeReturning,        // 원혼구슬이 끝에서 잠시 멈췄다가 돌아옴
        AutoReturnAfterSeconds         // 배에서 떨어지고 7초 동안 못타면 자동 복귀
    }

    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        Fox fox = character.GetComponent<Fox>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                fox.normalFireInterval -= 0.1f;              // 공격 쿨타임 0.1초 감소
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AbilityPower:
                fox.abilityPower += 10;                    // ap 10 증가
                Debug.Log("Debug1 Archer");
                fox.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // 초당 mp 증가량 3증가
                fox.mpPerSecond += 3;
                Debug.Log("Debug2 Archer");
                fox.upgradeNum = 2;
                break;
            case UpgradeType.MoreUltimateOrbs:                    // 궁극기 구슬 증가
                fox.skillCount += 6;
                Debug.Log("Debug3 Archer");
                fox.upgradeNum = 3;
                break;
            case UpgradeType.ReturnDamageScalesWithHitCount:                // 갈 때 맞은 적 개수당 돌아올 때 데미지 증가
                fox.isReturnDamageScalesWithHitCount = true;
                Debug.Log("Debug4 Archer");
                fox.upgradeNum = 4;
                break;
            case UpgradeType.IncreasedSpiritOrbRange:                 // 원혼구슬 사정거리 증가
                fox.skillTime += 0.3f;
                Debug.Log("Debug5 Archer");
                fox.upgradeNum = 5;
                break;
            case UpgradeType.EmpoweredAttackEvery3Hits:               //평타 3번당  강한 평타
                fox.isEmpoweredAttackEvery3Hits = true;
                Debug.Log("Debug6 Archer");
                fox.upgradeNum = 6;
                break;
            case UpgradeType.MoreDamageBasedOnOnboardAllies:                   // 현재 배에 타있는 동료에 비례해서 평타 데미지 증가
                fox.skillSize += 1;
                Debug.Log("Debug7 Archer");
                fox.upgradeNum = 7;
                break;
            case UpgradeType.OrbPausesBeforeReturning:                    // 원혼구슬이 끝에서 잠시 멈췄다가 돌아옴
                fox.isOrbPausesBeforeReturning = true;
                Debug.Log("Debug8 Archer");
                fox.upgradeNum = 8;
                break;
            case UpgradeType.AutoReturnAfterSeconds:                    // 떨어지는 속도 증가
                fox.isAutoReturnAfterSeconds = true;
                Debug.Log("Debug9 Archer");
                fox.upgradeNum = 9;
                break;
        }
        Debug.Log("업그레이드 성공");
    }
}
