using UnityEngine;

[CreateAssetMenu(fileName = "ArcherUpgrade", menuName = "Upgrades/ArcherUpgrade")]
public class ArcherUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // 일반 공격 속도 증가 (쿨타임 감소)
        AttackPower,       // 공격력 증가
        ManaRegen,         // 마나 초당 증가량 증가
        ArrowSize,         // 화살 크기 증가 (크게 만들어서 맞추기 쉬움 또는 데미지 증가용)
        ArrowKnockback,    // 화살 명중 시 적 넉백 효과 증가
        SkillUseCount,     // 스킬 사용 횟수 증가 (예: 버스트 발사 횟수)
        SkillArrowCount,   // 스킬 화살 발사량 증가 (1회 사용 시 더 많은 화살 발사)
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
                archer.normalFireInterval -= 0.1f;              // 공격 쿨타임 0.1초 감소
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AttackPower:
                archer.attackDamage += 10;                    // ad 10 증가
                Debug.Log("Debug1 Archer");
                archer.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // 초당 mp 증가량 3증가
                archer.mpPerSecond += 3;
                Debug.Log("Debug2 Archer");
                archer.upgradeNum = 2;
                break;
            case UpgradeType.ArrowSize:                     // ArrowSize  1.5배 증가
                archer.arrowSize *= 1.5f;
                Debug.Log("Debug3 Archer");
                archer.upgradeNum = 3;
                break;
            case UpgradeType.ArrowKnockback:                // KnockbackPower + 10 증가
                archer.knockbackPower += 10;
                Debug.Log("Debug4 Archer");
                archer.upgradeNum = 4;
                break;
            case UpgradeType.SkillUseCount:                 // 스킬 횟수가 +3 증가
                archer.skillCount += 3;
                Debug.Log("Debug5 Archer");
                archer.upgradeNum = 5;
                break;
            case UpgradeType.SkillArrowCount:               // 스킬에 사용하는 화살 갯수 +6 증가
                archer.skillProjectileCount += 6;
                Debug.Log("Debug6 Archer");
                archer.upgradeNum = 6;          
                break;
            case UpgradeType.AttackRange:                   // 사거리 50 증가
                archer.enemyDetectRadius += 50;
                Debug.Log("Debug7 Archer");
                archer.upgradeNum = 7;
                break;
            case UpgradeType.ArrowSpeed:                    // 투사체 속도 15증가
                archer.projectileSpeed += 15;
                Debug.Log("Debug8 Archer");
                archer.upgradeNum = 8;
                break;
            case UpgradeType.TripleShot:                    // 일반 공격 함수에 조건문으로 걸려있는 tripleshot true로 바꿈
                archer.isUpgradeTripleShot = true;
                Debug.Log("Debug9 Archer");
                archer.upgradeNum = 9;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}