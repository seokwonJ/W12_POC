using UnityEngine;

[CreateAssetMenu(fileName = "ArcherUpgrade", menuName = "Upgrades/ArcherUpgrade")]
public class ArcherUpgrade : CharacterUpgrade
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

        Debug.Log("���׷��̵� ����");
    }
}
