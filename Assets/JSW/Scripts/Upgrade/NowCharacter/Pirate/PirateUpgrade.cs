using UnityEngine;

[CreateAssetMenu(fileName = "PirateUpgrade", menuName = "Upgrades/PirateUpgrade")]
public class PirateUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackPowerUp,                              // �⺻ ������ ���
        AttackSpeedUp,                              // ��Ÿ ����
        ProjectileSpeedUp,                          // ����ü �̵��ӵ� ����
        ProjectileSizeUp,                           // ź ũ�� ����
        KnockbackPowerUp,                           // �� �о�� ȿ�� ����
        CriticalProbabilityUp,                      // ũ�� Ȯ�� ���
        CriticalDamageUp,                           // ũ�� ���� ��� ����
        AttackRangeUp,                              // �� ����/���� ���� �Ÿ� Ȯ��
        ManaRegenSpeedDownAttackPowerUp,            // ���� ȸ�� �ӵ� ���� + ���ݷ� ����
        ManaRegenSpeedUPAttackPowerDown             // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        Pirate pirate = character.GetComponent<Pirate>();
        switch (type)
        {
            //-------------- �⺻ ���׷��̵� --------------
            case UpgradeType.AttackPowerUp:
                pirate.AttackPowerUpNum += attackPowerUpPercent;                                    // �⺻ ������ ���
                Debug.Log("Debug0 pirate");
                break;
            case UpgradeType.AttackSpeedUp:
                pirate.AttackSpeedUpNum += attackSpeedUpPercent;                                    // ��Ÿ ����
                Debug.Log("Debug1 pirate");
                pirate.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // ����ü �̵��ӵ� ����
                pirate.ProjectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 pirate");
                pirate.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // ź ũ�� ����
                pirate.ProjectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 pirate");
                pirate.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // �� �о�� ȿ�� ����
                pirate.KnockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 pirate");
                pirate.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // ũ�� Ȯ�� ���
                pirate.CriticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 pirate");
                pirate.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // ũ�� ���� ��� ����
                pirate.CriticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 pirate");
                pirate.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // �� ����/���� ���� �Ÿ� Ȯ��
                pirate.AttackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 pirate");
                pirate.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ���ݷ� ����
                pirate.ManaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                pirate.AttackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 pirate");
                pirate.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAttackPowerDown:                                       // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
                pirate.ManaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                pirate.AttackPowerUpNum += ManaRegenSpeedUPAttackPowerDown_AttackPowerPercent;
                Debug.Log("Debug9 pirate");
                pirate.upgradeNum = 9;
                break;
                //-------------- Ư�� ���׷��̵� --------------

        }

        Debug.Log("���׷��̵� ����");
    }
}