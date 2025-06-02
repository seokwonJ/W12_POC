using UnityEngine;

[CreateAssetMenu(fileName = "PirateUpgrade", menuName = "Upgrades/PirateUpgrade")]
public class PirateUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // 일반 공격 속도 증가 (쿨타임 감소)
        AttackPower,       // 공격력 증가
        ManaRegen,         // 마나 초당 증가량 증가
        IncreasedCannonBlastRadius,         // 대포 폭발 범위 증가
        FasterCannonProjectile,    // 대포 투사체 속도 증가
        MoreSkillCannonShots,     // 궁 대포 투사체 개수 증가
        ReducedJumpPower,   // 점프력 낮아짐
        BackwardCannonShot,       // 일반 대포 쏠 때 정반대 방향으로도 한 발 쏨
        MoreSkillShots,        // 스킬 한번 더 씀
        FirstHitDealsBonusDamage         // 최초 대포 투사체 맞는 적은 큰 데미지 받음
    }
    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        Pirate pirate = character.GetComponent<Pirate>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                pirate.normalFireInterval -= 0.2f;              // 공격 쿨타임 0.1초 감소
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AttackPower:
                pirate.attackDamage += 10;                    // ad 10 증가
                Debug.Log("Debug1 Archer");
                pirate.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // 초당 mp 증가량 3증가
                pirate.mpPerSecond += 3;
                Debug.Log("Debug2 Archer");
                pirate.upgradeNum = 2;
                break;
            case UpgradeType.IncreasedCannonBlastRadius:                    // 대포 폭발 범위 증가
                pirate.nomalAttackSize += 0.5f;
                Debug.Log("Debug3 Archer");
                pirate.upgradeNum = 3;
                break;
            case UpgradeType.FasterCannonProjectile:                // 대포 투사체 속도 증가
                pirate.projectileSpeed += 10;
                Debug.Log("Debug4 Archer");
                pirate.upgradeNum = 4;
                break;
            case UpgradeType.MoreSkillCannonShots:                 // 궁 대포 투사체 개수 증가
                pirate.skillShotCount += 3;
                Debug.Log("Debug5 Archer");
                pirate.upgradeNum = 5;
                break;
            case UpgradeType.ReducedJumpPower:               // 점프력 낮아짐
                pirate.jumpForce -= 2;
                Debug.Log("Debug6 Archer");
                pirate.upgradeNum = 6;
                break;
            case UpgradeType.BackwardCannonShot:                   // 일반 대포 쏠 때 정반대 방향으로도 한 발 쏨
                pirate.isBackwardCannonShot = true;
                Debug.Log("Debug7 Archer");
                pirate.upgradeNum = 7;
                break;
            case UpgradeType.MoreSkillShots:                    // 스킬 한번 더 씀
                pirate.skillCount += 1;
                Debug.Log("Debug8 Archer");
                pirate.upgradeNum = 8;
                break;
            case UpgradeType.FirstHitDealsBonusDamage:                     // 최초 대포 투사체 맞는 적은 큰 데미지 받음
                pirate.isFirstHitDealsBonusDamage = true;
                Debug.Log("Debug9 Archer");
                pirate.upgradeNum = 9;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}