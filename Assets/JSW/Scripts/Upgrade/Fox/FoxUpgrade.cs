using UnityEngine;

[CreateAssetMenu(fileName = "FoxUpgrade", menuName = "Upgrades/FoxUpgrade")]
public class FoxUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // �Ϲ� ���� �ӵ� ���� (��Ÿ�� ����)
        AbilityPower,       // �ֹ��� ����
        ManaRegen,         // ���� �ʴ� ������ ����
        MoreUltimateOrbs,         // �ñر� ���� ����
        ReturnDamageScalesWithHitCount,    // �� �� ���� �� ������ ���ƿ� �� ������ ����
        IncreasedSpiritOrbRange,     // ��ȥ���� �����Ÿ� ����
        EmpoweredAttackEvery3Hits,   // ��Ÿ 3����  ���� ��Ÿ
        MoreDamageBasedOnOnboardAllies,       // ���� �迡 Ÿ�ִ� ���ῡ ����ؼ� ��Ÿ ������ ����
        OrbPausesBeforeReturning,        // ��ȥ������ ������ ��� ����ٰ� ���ƿ�
        AutoReturnAfterSeconds         // �迡�� �������� 7�� ���� ��Ÿ�� �ڵ� ����
    }

    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        Fox fox = character.GetComponent<Fox>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                fox.normalFireInterval -= 0.1f;              // ���� ��Ÿ�� 0.1�� ����
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AbilityPower:
                fox.abilityPower += 10;                    // ap 10 ����
                Debug.Log("Debug1 Archer");
                fox.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // �ʴ� mp ������ 3����
                fox.mpPerSecond += 3;
                Debug.Log("Debug2 Archer");
                fox.upgradeNum = 2;
                break;
            case UpgradeType.MoreUltimateOrbs:                    // �ñر� ���� ����
                fox.skillCount += 6;
                Debug.Log("Debug3 Archer");
                fox.upgradeNum = 3;
                break;
            case UpgradeType.ReturnDamageScalesWithHitCount:                // �� �� ���� �� ������ ���ƿ� �� ������ ����
                fox.isReturnDamageScalesWithHitCount = true;
                Debug.Log("Debug4 Archer");
                fox.upgradeNum = 4;
                break;
            case UpgradeType.IncreasedSpiritOrbRange:                 // ��ȥ���� �����Ÿ� ����
                fox.skillTime += 0.3f;
                Debug.Log("Debug5 Archer");
                fox.upgradeNum = 5;
                break;
            case UpgradeType.EmpoweredAttackEvery3Hits:               //��Ÿ 3����  ���� ��Ÿ
                fox.isEmpoweredAttackEvery3Hits = true;
                Debug.Log("Debug6 Archer");
                fox.upgradeNum = 6;
                break;
            case UpgradeType.MoreDamageBasedOnOnboardAllies:                   // ���� �迡 Ÿ�ִ� ���ῡ ����ؼ� ��Ÿ ������ ����
                fox.skillSize += 1;
                Debug.Log("Debug7 Archer");
                fox.upgradeNum = 7;
                break;
            case UpgradeType.OrbPausesBeforeReturning:                    // ��ȥ������ ������ ��� ����ٰ� ���ƿ�
                fox.isOrbPausesBeforeReturning = true;
                Debug.Log("Debug8 Archer");
                fox.upgradeNum = 8;
                break;
            case UpgradeType.AutoReturnAfterSeconds:                    // �������� �ӵ� ����
                fox.isAutoReturnAfterSeconds = true;
                Debug.Log("Debug9 Archer");
                fox.upgradeNum = 9;
                break;
        }
        Debug.Log("���׷��̵� ����");
    }
}
