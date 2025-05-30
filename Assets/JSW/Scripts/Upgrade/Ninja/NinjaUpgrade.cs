using UnityEngine;

[CreateAssetMenu(fileName = "NinjaUpgrade", menuName = "Upgrades/NinjaUpgrade")]
public class NinjaUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {

        AttackSpeed,            // 공속 증가
        AttackPower,             // ad 증가
        ManaRegen,              // 점프량 감소
        LowJump,                // 마나증가량 증가
        NomalAttackFive,         // 평타 5번당 한번 데미지 증가된 공격
        SkillPowerUp,           // 궁극기 증가하는 힘 증가
        SkillDurationUp,         // 궁극기 지속시간 증가
        AttackRange,            // 사거리 증가
        FirstLowHpEnemy,       // 체력이 적은 적부터 공격
        AttackSpeedPerMana,        // 현재 마나가 클수록 공속 증가
    }
    public UpgradeType type;
    public float value;


    public override void ApplyUpgrade(GameObject character)
    {
        Ninja ninja = character.GetComponent<Ninja>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                ninja.normalFireInterval -= 0.2f;
                Debug.Log("Debug0 Ninja");
                ninja.upgradeNum = 0;
                break;
            case UpgradeType.AttackPower:
                ninja.attackDamage += 10;
                Debug.Log("Debug1 Ninja");
                ninja.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:
                ninja.mpPerSecond += 3;
                Debug.Log("Debug2 Ninja");
                ninja.upgradeNum = 2;
                break;
            case UpgradeType.LowJump:
                ninja.jumpForce -= 3;
                Debug.Log("Debug3 Ninja");
                ninja.upgradeNum = 3;
                break;
            case UpgradeType.NomalAttackFive:
                ninja.isNomalAttackFive = true;
                Debug.Log("Debug4 Ninja");
                ninja.upgradeNum = 4;
                break;
            case UpgradeType.SkillPowerUp:
                ninja.skillPower += 10;
                Debug.Log("Debug5 Ninja");
                ninja.upgradeNum = 5;
                break;
            case UpgradeType.SkillDurationUp:
                ninja.skillPowerDuration += 2;
                Debug.Log("Debug6 Ninja");
                ninja.upgradeNum = 6;
                break;
            case UpgradeType.AttackRange:
                ninja.enemyDetectRadius += 50;
                Debug.Log("Debug7 Ninja");
                ninja.upgradeNum = 7;
                break;
            case UpgradeType.FirstLowHpEnemy:
                //archer.manaRegen += value;
                Debug.Log("Debug8 Ninja");
                ninja.upgradeNum = 8;
                break;
            case UpgradeType.AttackSpeedPerMana:
                //archer.manaRegen += value;
                Debug.Log("Debug9 Ninja");
                ninja.upgradeNum = 9;
                break;
        }

        Debug.Log("업그레이드 성공");
    }
}
