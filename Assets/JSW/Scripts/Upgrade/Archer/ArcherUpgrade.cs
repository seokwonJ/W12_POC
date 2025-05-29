using UnityEngine;

[CreateAssetMenu(fileName = "ArcherUpgrade", menuName = "Upgrades/ArcherUpgrade")]
public class ArcherUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // 일반 공격 속도 증가 (쿨타임 감소)
        AttackPower,       // 일반 공격의 공격력 증가
        ManaRegen,         // 마나 초당 증가량 증가
        ArrowSize,         // 화살 크기 증가 (크게 만들어서 맞추기 쉬움 또는 데미지 증가용)
        ArrowKnockback,    // 화살 명중 시 적 넉백 효과 증가
        SkillUseCount,     // 궁극기 사용 횟수 증가 (예: 버스트 발사 횟수)
        SkillArrowCount,   // 궁극기 화살 발사량 증가 (1회 사용 시 더 많은 화살 발사)
        AttackRange,       // 일반 공격 사거리 증가
        ArrowSpeed,        // 화살 비행 속도 증가
        TripleShot         // 기본 공격 시 한 번에 화살 3발 발사 }
    }
    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        Archer archer = character.GetComponent<Archer>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                // archer.normalFireInterval -= value;
                archer.upgradeNum = 0;
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AttackPower:
                //archer.arrowSize += value;
                Debug.Log("Debug1 Archer");
                archer.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:
                //archer.manaRegen += value;
                Debug.Log("Debug2 Archer");
                archer.upgradeNum = 2;
                break;
            case UpgradeType.ArrowSize:
                //archer.arrowSize += value;
                Debug.Log("Debug3 Archer");
                archer.upgradeNum = 3;
                break;
            case UpgradeType.ArrowKnockback:
                //archer.manaRegen += value;
                Debug.Log("Debug4 Archer");
                archer.upgradeNum = 4;
                break;
            case UpgradeType.SkillUseCount:
                //archer.arrowSize += value;
                Debug.Log("Debug5 Archer");
                archer.upgradeNum = 5;
                break;
            case UpgradeType.SkillArrowCount:
                //archer.manaRegen += value;
                Debug.Log("Debug6 Archer");
                archer.upgradeNum = 6;
                break;
            case UpgradeType.AttackRange:
                //archer.arrowSize += value;
                Debug.Log("Debug7 Archer");
                archer.upgradeNum = 7;
                break;
            case UpgradeType.ArrowSpeed:
                //archer.manaRegen += value;
                Debug.Log("Debug8 Archer");
                archer.upgradeNum = 8;
                break;
            case UpgradeType.TripleShot:
                //archer.manaRegen += value;
                Debug.Log("Debug9 Archer");
                archer.upgradeNum = 9;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}
