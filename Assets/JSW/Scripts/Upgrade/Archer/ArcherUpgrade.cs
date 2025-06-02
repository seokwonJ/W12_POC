using UnityEngine;

[CreateAssetMenu(fileName = "ArcherUpgrade", menuName = "Upgrades/ArcherUpgrade")]
public class ArcherUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // �Ϲ� ���� �ӵ� ���� (��Ÿ�� ����)
        AttackPower,       // ���ݷ� ����
        ManaRegen,         // ���� �ʴ� ������ ����
        ArrowSize,         // ȭ�� ũ�� ���� (ũ�� ���� ���߱� ���� �Ǵ� ������ ������)
        ArrowKnockback,    // ȭ�� ���� �� �� �˹� ȿ�� ����
        SkillUseCount,     // ��ų ��� Ƚ�� ���� (��: ����Ʈ �߻� Ƚ��)
        SkillArrowCount,   // ��ų ȭ�� �߻緮 ���� (1ȸ ��� �� �� ���� ȭ�� �߻�)
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
                archer.normalFireInterval -= 0.1f;              // ���� ��Ÿ�� 0.1�� ����
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AttackPower:
                archer.attackDamage += 10;                    // ad 10 ����
                Debug.Log("Debug1 Archer");
                archer.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // �ʴ� mp ������ 3����
                archer.mpPerSecond += 3;
                Debug.Log("Debug2 Archer");
                archer.upgradeNum = 2;
                break;
            case UpgradeType.ArrowSize:                     // ArrowSize  1.5�� ����
                archer.arrowSize *= 1.5f;
                Debug.Log("Debug3 Archer");
                archer.upgradeNum = 3;
                break;
            case UpgradeType.ArrowKnockback:                // KnockbackPower + 10 ����
                archer.knockbackPower += 10;
                Debug.Log("Debug4 Archer");
                archer.upgradeNum = 4;
                break;
            case UpgradeType.SkillUseCount:                 // ��ų Ƚ���� +3 ����
                archer.skillCount += 3;
                Debug.Log("Debug5 Archer");
                archer.upgradeNum = 5;
                break;
            case UpgradeType.SkillArrowCount:               // ��ų�� ����ϴ� ȭ�� ���� +6 ����
                archer.skillProjectileCount += 6;
                Debug.Log("Debug6 Archer");
                archer.upgradeNum = 6;          
                break;
            case UpgradeType.AttackRange:                   // ��Ÿ� 50 ����
                archer.enemyDetectRadius += 50;
                Debug.Log("Debug7 Archer");
                archer.upgradeNum = 7;
                break;
            case UpgradeType.ArrowSpeed:                    // ����ü �ӵ� 15����
                archer.projectileSpeed += 15;
                Debug.Log("Debug8 Archer");
                archer.upgradeNum = 8;
                break;
            case UpgradeType.TripleShot:                    // �Ϲ� ���� �Լ��� ���ǹ����� �ɷ��ִ� tripleshot true�� �ٲ�
                archer.isUpgradeTripleShot = true;
                Debug.Log("Debug9 Archer");
                archer.upgradeNum = 9;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}