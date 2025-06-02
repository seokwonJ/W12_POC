using UnityEngine;

[CreateAssetMenu(fileName = "ProtecterUpgrade", menuName = "Upgrades/ProtecterUpgrade")]
public class ProtecterUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // �Ϲ� ���� �ӵ� ���� (��Ÿ�� ����)
        AttackPower,       // ���ݷ� ����
        ManaRegen,         // ���� �ʴ� ������ ����
        LongerShieldDuration,       // ��ȣ�� ���ӽð� ����
        ShieldStrengthScalesWithAP, // �ֹ��� ����� ��ȣ�� ������ ����
        ShieldBreakExplosion,       // ��ȣ�� ���� �� �ֺ��� ������ 
        ProjectileCancelsProjectiles,   // ��Ÿ�� ����ü ������ �����
        LargerShieldSize,           // ��ȣ�� ũ�� Ŀ��
        IncreasedShieldDurability,  // ��ȣ�� ������ ����
        ReducedJumpPower            // ������ ������
    }

    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        Protecter protecter = character.GetComponent<Protecter>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                // archer.normalFireInterval -= value;
                protecter.normalFireInterval -= 0.1f;              // ���� ��Ÿ�� 0.1�� ����
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AttackPower:
                protecter.attackDamage += 10;                    // ad 10 ����
                Debug.Log("Debug1 Archer");
                protecter.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // �ʴ� mp ������ 3����
                protecter.mpPerSecond += 3;
                Debug.Log("Debug2 Archer");
                protecter.upgradeNum = 2;
                break;
            case UpgradeType.LongerShieldDuration:                     // ��ȣ�� ���ӽð� ����
                protecter.skillDuration += 3f;
                Debug.Log("Debug3 Archer");
                protecter.upgradeNum = 3;
                break;
            case UpgradeType.ShieldStrengthScalesWithAP:                // �ֹ��� ����� ��ȣ�� ������ ����
                protecter.skillMultiple += 2;
                Debug.Log("Debug4 Archer");
                protecter.upgradeNum = 4;
                break;
            case UpgradeType.ShieldBreakExplosion:                 // ��ȣ�� ���� �� �ֺ��� ������
                protecter.isShieldBreakExplosion = true;
                Debug.Log("Debug5 Archer");
                protecter.upgradeNum = 5;
                break;
            case UpgradeType.ProjectileCancelsProjectiles:               // ��Ÿ�� ����ü ������ �����
                protecter.isShieldCancelsProjectiles = true;
                Debug.Log("Debug6 Archer");
                protecter.upgradeNum = 6;
                break;
            case UpgradeType.LargerShieldSize:                   // ��ȣ�� ũ�� Ŀ��
                protecter.skillSize += 1;
                Debug.Log("Debug7 Archer");
                protecter.upgradeNum = 7;
                break;
            case UpgradeType.IncreasedShieldDurability:                    // ����ü �ӵ� 15����
                protecter.skillDurability += 100;
                Debug.Log("Debug8 Archer");
                protecter.upgradeNum = 8;
                break;
            case UpgradeType.ReducedJumpPower:                    // �Ϲ� ���� �Լ��� ���ǹ����� �ɷ��ִ� tripleshot true�� �ٲ�
                protecter.jumpForce -= 2;
                Debug.Log("Debug9 Archer");
                protecter.upgradeNum = 9;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}