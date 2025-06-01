using UnityEngine;

[CreateAssetMenu(fileName = "LeviUpgrade", menuName = "Upgrades/LeviUpgrade")]
public class LeviUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // �Ϲ� ���� �ӵ� ���� (��Ÿ�� ����)
        AttackPower,       // ���ݷ� ����
        ManaRegen,         // ���� �ʴ� ������ ����
        AttackWhileFalling,     // �������鼭�� ����
        LongerProjectileDuration,   // ���� ����ü ���ӽð� ����
        MoreSkillTargets,           // ��ų Ÿ�� ����
        GainPowerFromSkillDamage,   // ��ų �� ������ �� �� ����ؼ� �Ͻ��� ���ݷ� ���
        ManaOnLandingBasedOnTimeAway,   // ��� ������ �ð��� ����Ͽ� ������ ���� �ο�
        AttackSpeedBoostAfterQuickReboard,  // ��ų ���� 3�� �ȿ� ž�� �� �Ͻ����� ���ݼӵ� ���
        MoreUltimateDamageWithJumpPower     // ������ ����ؼ� ��ų ������ ����
    }

    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        Levi levi = character.GetComponent<Levi>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                levi.normalFireInterval -= 0.1f;              // ���� ��Ÿ�� 0.1�� ����
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AttackPower:
                levi.attackDamage += 10;                    // ad 10 ����
                Debug.Log("Debug1 Archer");
                levi.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // �ʴ� mp ������ 3����
                levi.mpPerSecond += 3;
                Debug.Log("Debug2 Archer");
                levi.upgradeNum = 2;
                break;
            case UpgradeType.AttackWhileFalling:                     // �������鼭�� ����
                levi.isAttackWhileFalling = true;
                Debug.Log("Debug3 Archer");
                levi.upgradeNum = 3;
                break;
            case UpgradeType.LongerProjectileDuration:                //  ���� ����ü ���ӽð� ����
                levi.NormalAttackProjectileDuration += 2;
                Debug.Log("Debug4 Archer");
                levi.upgradeNum = 4;
                break;
            case UpgradeType.MoreSkillTargets:                 // ��ų Ÿ�� ����
                levi.skillTargetCount += 2;
                Debug.Log("Debug5 Archer");
                levi.upgradeNum = 5;
                break;
            case UpgradeType.GainPowerFromSkillDamage:               // ��ų �� ������ �� �� ����ؼ� �Ͻ��� ���ݷ� ���
                levi.isGainPowerFromSkillDamage = true;
                Debug.Log("Debug6 Archer");
                levi.upgradeNum = 6;
                break;
            case UpgradeType.ManaOnLandingBasedOnTimeAway:                   // ��� ������ �ð��� ����Ͽ� ������ ���� �ο�
                levi.isManaOnLandingBasedOnTimeAway = true;
                Debug.Log("Debug7 Archer");
                levi.upgradeNum = 7;
                break;
            case UpgradeType.AttackSpeedBoostAfterQuickReboard:                   // ��ų ���� 3�� �ȿ� ž�� �� �Ͻ����� ���ݼӵ� ���
                levi.isAttackSpeedBoostAfterQuickReboard = true;
                Debug.Log("Debug8 Archer");
                levi.upgradeNum = 8;
                break;
            case UpgradeType.MoreUltimateDamageWithJumpPower:                    // �Ϲ� ���� �Լ��� ���ǹ����� �ɷ��ִ� tripleshot true�� �ٲ�
                levi.isMoreUltimateDamageWithJumpPower = true;
                Debug.Log("Debug9 Archer");
                levi.upgradeNum = 9;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}
