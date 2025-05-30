using UnityEditor.Searcher;
using UnityEngine;

[CreateAssetMenu(fileName = "TankerUpgrade", menuName = "Upgrades/TankerUpgrade")]
public class TankerUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // 일반 공격 속도 증가 (쿨타임 감소)
        AttackPower,       // 공격력 증가
        ManaRegen,         // 마나 초당 증가량 증가
        FallSpeed,          // 떨어지는 속도 증가
        UltimateExpansion,      // 궁극기 범위 증가
        FallingSpeedToSkillDamage,     // 떨어지는 속도가 빠를수록 궁 데미지 증가
        NomalAttackKnockback,       // 일반공격 넉백 강해짐
        ShipDamageReduction,       // 배에 타있으면 배가 받는 데미지 피해 줄여줌
        HitSkillPerGetMana,        // 밀려난 적의 수만큼 마나 획득
        CloserMoreDamage         // 거리가 가까울 수록 데미지 더 들어감
    }

    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        Tanker tanker = character.GetComponent<Tanker>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                tanker.normalFireInterval -= 0.1f;             
                Debug.Log("Debug0 Tanker");
                break;
            case UpgradeType.AttackPower:
                tanker.attackDamage += 10;                
                Debug.Log("Debug1 Tanker");
                tanker.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                    
                tanker.mpPerSecond += 3;
                Debug.Log("Debug2 Tanker");
                tanker.upgradeNum = 2;
                break;
            case UpgradeType.FallSpeed:                     
                tanker.maxFallSpeed += 10;
                Debug.Log("Debug3 Tanker");
                tanker.upgradeNum = 3;
                break;
            case UpgradeType.UltimateExpansion:
                tanker.skillRange *= 2;
                Debug.Log("Debug9 Tanker");
                tanker.upgradeNum = 4;
                break;
            case UpgradeType.FallingSpeedToSkillDamage:
                tanker.isFallingSpeedToSkillDamage = true;
                Debug.Log("Debug4 Tanker");
                tanker.upgradeNum = 5;
                break;
            case UpgradeType.NomalAttackKnockback:                
                tanker.knockBackpower += 10;
                Debug.Log("Debug5 Tanker");
                tanker.upgradeNum = 5;
                break;
            case UpgradeType.ShipDamageReduction:              
                tanker.isShieldFlyer = true;
                Debug.Log("Debug6 Tanker");
                tanker.upgradeNum = 7;
                break;
            case UpgradeType.HitSkillPerGetMana:                  
                tanker.isHitSkillPerGetMana = true;
                Debug.Log("Debug7 Tanker");
                tanker.upgradeNum = 8;
                break;
            case UpgradeType.CloserMoreDamage:                    
                tanker.isCloserMoreDamage = true;
                Debug.Log("Debug8 Tanker");
                tanker.upgradeNum = 9;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}
