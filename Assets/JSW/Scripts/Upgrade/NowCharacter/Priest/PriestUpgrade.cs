using UnityEngine;

[CreateAssetMenu(fileName = "PriestUpgrade", menuName = "Upgrades/PriestUpgrade")]
public class PriestUpgrade : CharacterUpgrade
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
        ManaRegenSpeedUPAbilityPowerUp             // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        Priest priest = character.GetComponent<Priest>();
        switch (type)
        {
            //-------------- �⺻ ���׷��̵� --------------
            case UpgradeType.AttackPowerUp:
                priest.attackPowerUpNum += attackPowerUpPercent;                                    // �⺻ ������ ���
                Debug.Log("Debug0 priest");
                break;
            case UpgradeType.AttackSpeedUp:
                priest.attackSpeedUpNum += attackSpeedUpPercent;                                    // ��Ÿ ����
                Debug.Log("Debug1 priest");
                priest.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // ����ü �̵��ӵ� ����
                priest.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 priest");
                priest.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // ź ũ�� ����
                priest.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 priest");
                priest.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // �� �о�� ȿ�� ����
                priest.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 priest");
                priest.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // ũ�� Ȯ�� ���
                priest.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 priest");
                priest.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // ũ�� ���� ��� ����
                priest.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 priest");
                priest.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // �� ����/���� ���� �Ÿ� Ȯ��
                priest.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 priest");
                priest.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ���ݷ� ����
                priest.manaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                priest.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 priest");
                priest.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
                priest.manaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                priest.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerDown_AbilityPowerPercent;
                Debug.Log("Debug9 priest");
                priest.upgradeNum = 9;
                break;
                //-------------- Ư�� ���׷��̵� --------------

        }

        Debug.Log("���׷��̵� ����");
    }
}
