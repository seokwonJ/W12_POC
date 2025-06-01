using UnityEngine;

[CreateAssetMenu(fileName = "ProtecterUpgrade", menuName = "Upgrades/ProtecterUpgrade")]
public class ProtecterUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // 일반 공격 속도 증가 (쿨타임 감소)
        AttackPower,       // 공격력 증가
        ManaRegen,         // 마나 초당 증가량 증가
        LongerShieldDuration,       // 보호막 지속시간 증가
        ShieldStrengthScalesWithAP, // 주문력 배수당 보호막 내구력 증가
        ShieldBreakExplosion,       // 보호막 깨질 시 주변에 데미지 
        ProjectileCancelsProjectiles,   // 평타에 투사체 닿을시 사라짐
        LargerShieldSize,           // 보호막 크기 커짐
        IncreasedShieldDurability,  // 보호막 내구도 증가
        ReducedJumpPower            // 점프력 낮아짐
    }

    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        Protecter protecter = character.GetComponent<Protecter>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                // archer.normalFireInterval -= value;
                protecter.normalFireInterval -= 0.1f;              // 공격 쿨타임 0.1초 감소
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AttackPower:
                protecter.attackDamage += 10;                    // ad 10 증가
                Debug.Log("Debug1 Archer");
                protecter.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // 초당 mp 증가량 3증가
                protecter.mpPerSecond += 3;
                Debug.Log("Debug2 Archer");
                protecter.upgradeNum = 2;
                break;
            case UpgradeType.LongerShieldDuration:                     // 보호막 지속시간 증가
                protecter.skillDuration += 3f;
                Debug.Log("Debug3 Archer");
                protecter.upgradeNum = 3;
                break;
            case UpgradeType.ShieldStrengthScalesWithAP:                // 주문력 배수당 보호막 내구력 증가
                protecter.skillMultiple += 2;
                Debug.Log("Debug4 Archer");
                protecter.upgradeNum = 4;
                break;
            case UpgradeType.ShieldBreakExplosion:                 // 보호막 깨질 시 주변에 데미지
                protecter.isShieldBreakExplosion = true;
                Debug.Log("Debug5 Archer");
                protecter.upgradeNum = 5;
                break;
            case UpgradeType.ProjectileCancelsProjectiles:               // 평타에 투사체 닿을시 사라짐
                protecter.isShieldCancelsProjectiles = true;
                Debug.Log("Debug6 Archer");
                protecter.upgradeNum = 6;
                break;
            case UpgradeType.LargerShieldSize:                   // 보호막 크기 커짐
                protecter.skillSize += 1;
                Debug.Log("Debug7 Archer");
                protecter.upgradeNum = 7;
                break;
            case UpgradeType.IncreasedShieldDurability:                    // 투사체 속도 15증가
                protecter.skillDurability += 100;
                Debug.Log("Debug8 Archer");
                protecter.upgradeNum = 8;
                break;
            case UpgradeType.ReducedJumpPower:                    // 일반 공격 함수에 조건문으로 걸려있는 tripleshot true로 바꿈
                protecter.jumpForce -= 2;
                Debug.Log("Debug9 Archer");
                protecter.upgradeNum = 9;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}