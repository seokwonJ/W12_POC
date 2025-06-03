using UnityEngine;

[CreateAssetMenu(fileName = "NinjaUpgrade", menuName = "Upgrades/NinjaUpgrade")]
public class NinjaUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {

        AttackSpeed,            // ���� ����
        AttackPower,             // ad ����
        ManaRegen,              // ������ ����
        LowJump,                // ���������� ����
        NomalAttackFive,         // ��Ÿ 5���� �ѹ� ������ ������ ����
        SkillPowerUp,           // ��ų �����ϴ� �� ����
        SkillDurationUp,         // ��ų ���ӽð� ����
        AttackRange,            // ��Ÿ� ����
        FirstLowHpEnemy,       // ü���� ���� ������ ����
        AttackSpeedPerMana,        // ���� ������ Ŭ���� ���� ����
    }
    public UpgradeType type;
    public float value;


    public override void ApplyUpgrade(GameObject character)
    {
        Ninja ninja = character.GetComponent<Ninja>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:               // ���� ����
                ninja.normalFireInterval -= 0.1f;
                Debug.Log("Debug0 Ninja");
                ninja.upgradeNum = 0;
                break;
            case UpgradeType.AttackPower:               // ad ����
                ninja.attackDamage += 10;
                Debug.Log("Debug1 Ninja");
                ninja.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                 // ���������� ����
                ninja.mpPerSecond += 3;
                Debug.Log("Debug2 Ninja");
                ninja.upgradeNum = 2;
                break;
            case UpgradeType.LowJump:                   // ������ ����
                ninja.jumpForce -= 3;
                Debug.Log("Debug3 Ninja");
                ninja.upgradeNum = 3;
                break;
            case UpgradeType.NomalAttackFive:            // ��Ÿ 5���� �ѹ� ������ ������ ����
                ninja.isNomalAttackFive = true;
                Debug.Log("Debug4 Ninja");
                ninja.upgradeNum = 4;
                break;
            case UpgradeType.SkillPowerUp:              // ��ų �����ϴ� �� ����
                ninja.skillPower += 10;
                Debug.Log("Debug5 Ninja");
                ninja.upgradeNum = 5;
                break;
            case UpgradeType.SkillDurationUp:           // ��ų ���ӽð� ����
                ninja.skillPowerDuration += 2;
                Debug.Log("Debug6 Ninja");
                ninja.upgradeNum = 6;
                break;
            case UpgradeType.AttackRange:               // ��Ÿ� ����
                ninja.enemyDetectRadius += 10;
                Debug.Log("Debug7 Ninja");
                ninja.upgradeNum = 7;
                break;
            case UpgradeType.FirstLowHpEnemy:           // ü���� ���� ������ ����
                ninja.isFirstLowHPEnemy = true;
                Debug.Log("Debug8 Ninja");
                ninja.upgradeNum = 8;
                break;
            case UpgradeType.AttackSpeedPerMana:        // ���� ������ Ŭ���� ���� ����
                ninja.isAttackSpeedPerMana = true;
                Debug.Log("Debug9 Ninja");
                ninja.upgradeNum = 9;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}
