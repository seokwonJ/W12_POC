using UnityEngine;

[CreateAssetMenu(fileName = "WorriorUpgrade", menuName = "Upgrades/WorriorUpgrade")]
public class WorriorUpgrade : CharacterUpgrade
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
        Worrior worrior = character.GetComponent<Worrior>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                // archer.normalFireInterval -= value;
                Debug.Log("Debug0 Worrior");
                worrior.upgradeNum = 0;
                break;
            case UpgradeType.AttackPower:
                //archer.arrowSize += value;
                Debug.Log("Debug1 Worrior");
                worrior.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:
                //archer.manaRegen += value;
                Debug.Log("Debug2 Worrior");
                worrior.upgradeNum = 2;
                break;
            case UpgradeType.ArrowSize:
                //archer.arrowSize += value;
                Debug.Log("Debug3 Worrior");
                worrior.upgradeNum = 3;
                break;
            case UpgradeType.ArrowKnockback:
                //archer.manaRegen += value;
                Debug.Log("Debug4 Worrior");
                worrior.upgradeNum = 4;
                break;
            case UpgradeType.SkillUseCount:
                //archer.arrowSize += value;
                Debug.Log("Debug5 Worrior");
                worrior.upgradeNum = 5;
                break;
            case UpgradeType.SkillArrowCount:
                //archer.manaRegen += value;
                Debug.Log("Debug6 Worrior");
                worrior.upgradeNum = 6;
                break;
            case UpgradeType.AttackRange:
                //archer.arrowSize += value;
                Debug.Log("Debug7 Worrior");
                worrior.upgradeNum = 7;
                break;
            case UpgradeType.ArrowSpeed:
                //archer.manaRegen += value;
                Debug.Log("Debug8 Worrior");
                worrior.upgradeNum = 8;
                break;
            case UpgradeType.TripleShot:
                //archer.manaRegen += value;
                Debug.Log("Debug9 Worrior");
                worrior.upgradeNum = 9;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}
