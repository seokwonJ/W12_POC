using UnityEngine;

[CreateAssetMenu(fileName = "MagicianUpgrade", menuName = "Upgrades/MagicianUpgrade")]
public class MagicianUpgrade : CharacterUpgrade
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
        Magician magician = character.GetComponent<Magician>();
        switch (type)
        {
            //-------------- �⺻ ���׷��̵� --------------
            case UpgradeType.AttackPowerUp:
                magician.AttackPowerUpNum += attackPowerUpPercent;                                    // �⺻ ������ ���
                Debug.Log("Debug0 magician");
                break;
            case UpgradeType.AttackSpeedUp:
                magician.AttackSpeedUpNum += attackSpeedUpPercent;                                    // ��Ÿ ����
                Debug.Log("Debug1 magician");
                magician.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // ����ü �̵��ӵ� ����
                magician.ProjectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 magician");
                magician.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // ź ũ�� ����
                magician.ProjectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 magician");
                magician.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // �� �о�� ȿ�� ����
                magician.KnockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 magician");
                magician.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // ũ�� Ȯ�� ���
                magician.CriticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 magician");
                magician.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // ũ�� ���� ��� ����
                magician.CriticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 magician");
                magician.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // �� ����/���� ���� �Ÿ� Ȯ��
                magician.AttackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 magician");
                magician.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ���ݷ� ����
                magician.ManaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                magician.AttackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 magician");
                magician.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAttackPowerDown:                                       // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
                magician.ManaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                magician.AttackPowerUpNum += ManaRegenSpeedUPAttackPowerDown_AttackPowerPercent;
                Debug.Log("Debug9 magician");
                magician.upgradeNum = 9;
                break;
                //-------------- Ư�� ���׷��̵� --------------

        }

        Debug.Log("���׷��̵� ����");
    }
}
