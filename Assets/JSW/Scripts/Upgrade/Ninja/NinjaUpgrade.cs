using UnityEngine;

[CreateAssetMenu(fileName = "NinjaUpgrade", menuName = "Upgrades/NinjaUpgrade")]
public class NinjaUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {

        AttackSpeed,       // �Ϲ� ���� �ӵ� ���� (��Ÿ�� ����)
        AttackPower,       // �Ϲ� ������ ���ݷ� ����
        ManaRegen,         // ���� �ʴ� ������ ����
        ArrowSize,         // ȭ�� ũ�� ���� (ũ�� ���� ���߱� ���� �Ǵ� ������ ������)
        ArrowKnockback,    // ȭ�� ���� �� �� �˹� ȿ�� ����
        SkillUseCount,     // �ñر� ��� Ƚ�� ���� (��: ����Ʈ �߻� Ƚ��)
        SkillArrowCount,   // �ñر� ȭ�� �߻緮 ���� (1ȸ ��� �� �� ���� ȭ�� �߻�)
        AttackRange,       // �Ϲ� ���� ��Ÿ� ����
        ArrowSpeed,        // ȭ�� ���� �ӵ� ����
        TripleShot         // �⺻ ���� �� �� ���� ȭ�� 3�� �߻� }
    }
    public UpgradeType type;
    public float value;


    public override void ApplyUpgrade(GameObject character)
    {
        Ninja ninja = character.GetComponent<Ninja>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                // archer.normalFireInterval -= value;
                Debug.Log("Debug0 Ninja");
                ninja.upgradeNum = 0;
                break;
            case UpgradeType.AttackPower:
                //archer.arrowSize += value;
                Debug.Log("Debug1 Ninja");
                ninja.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:
                //archer.manaRegen += value;
                Debug.Log("Debug2 Magician");
                ninja.upgradeNum = 2;
                break;
            case UpgradeType.ArrowSize:
                //archer.arrowSize += value;
                Debug.Log("Debug3 Magician");
                ninja.upgradeNum = 3;
                break;
            case UpgradeType.ArrowKnockback:
                //archer.manaRegen += value;
                Debug.Log("Debug4 Magician");
                ninja.upgradeNum = 4;
                break;
            case UpgradeType.SkillUseCount:
                //archer.arrowSize += value;
                Debug.Log("Debug5 Magician");
                ninja.upgradeNum = 5;
                break;
            case UpgradeType.SkillArrowCount:
                //archer.manaRegen += value;
                Debug.Log("Debug6 Magician");
                ninja.upgradeNum = 6;
                break;
            case UpgradeType.AttackRange:
                //archer.arrowSize += value;
                Debug.Log("Debug7 Magician");
                ninja.upgradeNum = 7;
                break;
            case UpgradeType.ArrowSpeed:
                //archer.manaRegen += value;
                Debug.Log("Debug8 Magician");
                ninja.upgradeNum = 8;
                break;
            case UpgradeType.TripleShot:
                //archer.manaRegen += value;
                Debug.Log("Debug9 Magician");
                ninja.upgradeNum = 9;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}
