using UnityEngine;

[CreateAssetMenu(fileName = "ArcherUpgrade", menuName = "Upgrades/ArcherUpgrade")]
public class ArcherUpgrade : CharacterUpgrade
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
        ManaRegenSpeedUPAbilityPowerUp,             // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
        
        SkillCountUp,                               // ��ų�� 1ȸ �� �����մϴ�.
        AttackProjectileUp,                         // �⺻ ������ ����ü 1���� �� �߻��մϴ�.(���� 3 ���� ȭ�� )
        DieInstantly,                               // ������ ������ ���� Ÿ�� �� ���� Ȯ���� ��� ��ŵ�ϴ�
        SameEnemyDamageUp,                          // ���� ����� ���� �� �� �����ð� ������� �����մϴ�. ���ӽð� : N��
        SkillProjectileCountUp                      // ��ų ����ü ���� ����
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        UpgradeController upgradeController = character.GetComponent<UpgradeController>();
        upgradeController.ApplyUpgrade(this, character);
        Archer archer = character.GetComponent<Archer>();
        switch (type)
        {
            //-------------- �⺻ ���׷��̵� --------------
            case UpgradeType.AttackPowerUp:
                archer.attackPowerUpNum += attackPowerUpPercent;                                    // �⺻ ������ ���
                Debug.Log("Debug0 archer");
                break;
            case UpgradeType.AttackSpeedUp:
                archer.attackSpeedUpNum += attackSpeedUpPercent;                                    // ��Ÿ ����
                Debug.Log("Debug1 archer");
                archer.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // ����ü �̵��ӵ� ����
                archer.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 archer");
                archer.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // ź ũ�� ����
                archer.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 archer");
                archer.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // �� �о�� ȿ�� ����
                archer.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 archer");
                archer.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // ũ�� Ȯ�� ���
                archer.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 archer");
                archer.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // ũ�� ���� ��� ����
                archer.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 archer");
                archer.upgradeNum = 6;          
                break;
            case UpgradeType.AttackRangeUp:                                                         // �� ����/���� ���� �Ÿ� Ȯ��
                archer.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 archer");
                archer.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ���ݷ� ����
                archer.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                archer.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 archer");
                archer.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
                archer.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                archer.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 archer");
                archer.upgradeNum = 9;
                break;

            //-------------- Ư�� ���׷��̵� --------------

            case UpgradeType.SkillCountUp:                                                          // ��ų�� 1ȸ �� �����մϴ�.
                archer.skillCount += 1;
                Debug.Log("Debug10 archer");
                archer.upgradeNum = 10;
                break;
            case UpgradeType.AttackProjectileUp:                                                    // �⺻ ������ ����ü 1���� �� �߻��մϴ�.(���� 3 ���� ȭ�� )
                archer.isUpgradeTwoShot = true;
                Debug.Log("Debug11 archer");
                archer.upgradeNum = 11;
                break;
            case UpgradeType.DieInstantly:                                                          // ������ ������ ���� Ÿ�� �� ���� Ȯ���� ��� ��ŵ�ϴ�
                archer.isUpgradeDieInstantly = true;
                Debug.Log("Debug12 archer");
                archer.upgradeNum = 12;
                break;
            case UpgradeType.SameEnemyDamageUp:                                                     // ���� ����� ���� �� �� �����ð� ������� �����մϴ�. ���ӽð� : N��
                archer.isUpgradeSameEnemyDamage = true;
                Debug.Log("Debug13 archer");
                archer.upgradeNum = 13;
                break;
            case UpgradeType.SkillProjectileCountUp:                                                // ��ų ����ü ���� ����
                archer.skillProjectileCount += 6;
                Debug.Log("Debug14 archer");
                archer.upgradeNum = 14;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}