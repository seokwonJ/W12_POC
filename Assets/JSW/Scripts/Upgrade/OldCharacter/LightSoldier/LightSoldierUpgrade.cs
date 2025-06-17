using UnityEngine;

[CreateAssetMenu(fileName = "LightSoldierUpgrade", menuName = "Upgrades/LightSoldierUpgrade")]
public class LightSoldierUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // �Ϲ� ���� �ӵ� ���� (��Ÿ�� ����)
        AttackPower,       // ���ݷ� ����
        ManaRegen,         // ���� �ʴ� ������ ����
        LargerNormalAttack,         // �Ϲݰ��� ũ�� Ŀ��
        Fires4NormalAttackProjectiles,    // �Ϲݰ��� 4�� ����
        LongerNormalAttackDuration,     // �Ϲ� ���� ���ӽð� ����
        IncreasedSkillCount,   // �� ���� ���� 
        LargerSkillSize,       // �� ũ�Ⱑ Ŀ��
        Gain1ManaPerHit,        // ��Ÿ�� ���� �� �� ���� 1ȹ��
        FasterFallSpeed         // �������� �ӵ� ����
    }

    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        LightSoldier lightSoldier = character.GetComponent<LightSoldier>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                lightSoldier.normalFireInterval -= 0.2f;              // ���� ��Ÿ�� 0.1�� ����
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AttackPower:
                lightSoldier.attackDamage += 10;                    // ad 10 ����
                Debug.Log("Debug1 Archer");
                lightSoldier.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // �ʴ� mp ������ 3����
                lightSoldier.mpPerSecond += 3;
                Debug.Log("Debug2 Archer");
                lightSoldier.upgradeNum = 2;
                break;
            case UpgradeType.LargerNormalAttack:                    // �Ϲݰ��� ũ�� Ŀ��
                lightSoldier.normalAttackSize += 2f;
                Debug.Log("Debug3 Archer");
                lightSoldier.upgradeNum = 3;
                break;
            case UpgradeType.Fires4NormalAttackProjectiles:                // �Ϲݰ��� 4�� ����
                lightSoldier.isFires4NormalAttackProjectiles = true;
                Debug.Log("Debug4 Archer");
                lightSoldier.upgradeNum = 4;
                break;
            case UpgradeType.LongerNormalAttackDuration:                 // �Ϲ� ���� ���ӽð� ����
                lightSoldier.normalAttackLifetime += 3;
                Debug.Log("Debug5 Archer");
                lightSoldier.upgradeNum = 5;
                break;
            case UpgradeType.IncreasedSkillCount:               // �� ���� ���� 
                lightSoldier.skillShotCount += 6;
                Debug.Log("Debug6 Archer");
                lightSoldier.upgradeNum = 6;
                break;
            case UpgradeType.LargerSkillSize:                   // �� ũ�Ⱑ Ŀ��
                lightSoldier.skillSize += 1;
                Debug.Log("Debug7 Archer");
                lightSoldier.upgradeNum = 7;
                break;
            case UpgradeType.Gain1ManaPerHit:                    // ��Ÿ�� ���� �� �� ���� 1ȹ��
                lightSoldier.isGain1ManaPerHit = true;
                Debug.Log("Debug8 Archer");
                lightSoldier.upgradeNum = 8;
                break;
            case UpgradeType.FasterFallSpeed:                    // �������� �ӵ� ����
                lightSoldier.maxFallSpeed += 5;
                Debug.Log("Debug9 Archer");
                lightSoldier.upgradeNum = 9;
                break;
        }
        Debug.Log("���׷��̵� ����");
    }
}


