using UnityEditor.Searcher;
using UnityEngine;

[CreateAssetMenu(fileName = "FoxUpgrade", menuName = "Upgrades/FoxUpgrade")]
public class FoxUpgrade : CharacterUpgrade
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
        ManaRegenSpeedUPAbilityPowerUp ,            // ���� ȸ�� �ӵ� ���� + ��ų ����� ����

        TenAttackSkillDamageUp,                     // �⺻������ 10ȸ �� ���� ��ų�� ������� ���� �����մϴ�
        AttackEnemyDefenseDown,                     // ������ ������ ���� ������ ���ҽ�ŵ�ϴ�
        AttackEnemySpeedDown,                       // ������ ������ ���������� ���� �̵��ӵ���������
        AttackMoreFarAway,                          // ��ȥ���� �� �� �ָ� ������
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        Fox fox = character.GetComponent<Fox>();
        switch (type)
        {
            //-------------- �⺻ ���׷��̵� --------------
            case UpgradeType.AttackPowerUp:
                fox.attackPowerUpNum += attackPowerUpPercent;                                       // �⺻ ������ ���
                Debug.Log("Debug0 fox");
                break;
            case UpgradeType.AttackSpeedUp:
                fox.attackSpeedUpNum += attackSpeedUpPercent;                                       // ��Ÿ ����
                Debug.Log("Debug1 fox");
                fox.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // ����ü �̵��ӵ� ����
                fox.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 fox");
                fox.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // ź ũ�� ����
                fox.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 fox");
                fox.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // �� �о�� ȿ�� ����
                fox.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 fox");
                fox.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // ũ�� Ȯ�� ���
                fox.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 fox");
                fox.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // ũ�� ���� ��� ����
                fox.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 fox");
                fox.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // �� ����/���� ���� �Ÿ� Ȯ��
                fox.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 fox");
                fox.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ���ݷ� ����
                fox.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                fox.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 fox");
                fox.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                        // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
                fox.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                fox.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 fox");
                fox.upgradeNum = 9;
                break;

            //-------------- Ư�� ���׷��̵� --------------

            case UpgradeType.TenAttackSkillDamageUp:                                                    // �⺻������ 10ȸ �� ���� ��ų�� ������� ���� �����մϴ�
                fox.isUpgradeTenAttackSkillDamageUp = true;
                Debug.Log("Debug10 archer");
                fox.upgradeNum = 10;
                break;
            case UpgradeType.AttackEnemyDefenseDown:                                                    // ������ ������ ���� ������ ���ҽ�ŵ�ϴ�
                fox.isUpgradeAttackEnemyDefenseDown = true;
                Debug.Log("Debug11 archer");
                fox.upgradeNum = 11;
                break;
            case UpgradeType.AttackEnemySpeedDown:                                                      // ������ ������ ���������� ���� �̵��ӵ���������
                fox.isUpgradeAttackEnemySpeedDown = true;
                Debug.Log("Debug12 archer");
                fox.upgradeNum = 12;
                break;
            case UpgradeType.AttackMoreFarAway:                                                         // ��ȥ���� �� �� �ָ� ������
                fox.attackDuration += 0.5f;
                Debug.Log("Debug13 archer");
                fox.upgradeNum = 13;
                break;

        }

        Debug.Log("���׷��̵� ����");
    }
}
