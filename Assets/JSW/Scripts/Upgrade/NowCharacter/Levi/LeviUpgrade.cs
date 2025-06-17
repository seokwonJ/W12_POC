using UnityEditor.Searcher;
using UnityEngine;

[CreateAssetMenu(fileName = "LeviUpgrade", menuName = "Upgrades/LeviUpgrade")]
public class LeviUpgrade : CharacterUpgrade
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

        SkillCountUp,                               // 1���� ���ݿ� 1�� ������ �߰��մϴ� ��ų ������� ���� �����մϴ�
        AttackProjectileUp,                         // ������ �� �� ���� ��ų ����� 1ȸ �߰��մϴ� �ִ� nȸ���� �����մϴ�.(���׷��̵� �Ҷ� ���� �ִ�ġ ���� )
        DieInstantly,                               // �˱Ⱑ �� �ָ� ������ (���� : ������ ���ӽð��� �����Ѵ� )
        SameEnemyDamageUp,                          // ���� �� �� ���� �����ϴ� ������� �پ���
        SkillProjectileCountUp                      // ��ų�� �������� �������� ������� �ش�
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        Levi levi = character.GetComponent<Levi>();
        switch (type)
        {
            //-------------- �⺻ ���׷��̵� --------------
            case UpgradeType.AttackPowerUp:
                levi.attackPowerUpNum += attackPowerUpPercent;                                    // �⺻ ������ ���
                Debug.Log("Debug0 levi");
                break;
            case UpgradeType.AttackSpeedUp:
                levi.attackSpeedUpNum += attackSpeedUpPercent;                                    // ��Ÿ ����
                Debug.Log("Debug1 levi");
                levi.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // ����ü �̵��ӵ� ����
                levi.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 levi");
                levi.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // ź ũ�� ����
                levi.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 levi");
                levi.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // �� �о�� ȿ�� ����
                levi.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 levi");
                levi.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // ũ�� Ȯ�� ���
                levi.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 levi");
                levi.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // ũ�� ���� ��� ����
                levi.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 levi");
                levi.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // �� ����/���� ���� �Ÿ� Ȯ��
                levi.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 levi");
                levi.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ���ݷ� ����
                levi.manaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                levi.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 levi");
                levi.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
                levi.manaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                levi.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerDown_AbilityPowerPercent;
                Debug.Log("Debug9 levi");
                levi.upgradeNum = 9;
                break;

            //-------------- Ư�� ���׷��̵� --------------

            case UpgradeType.SkillCountUp:                                                          // ��ų�� 1ȸ �� �����մϴ�.
                levi.skillCount += 1;
                Debug.Log("Debug10 archer");
                levi.upgradeNum = 10;
                break;
            case UpgradeType.AttackProjectileUp:                                                    // �⺻ ������ ����ü 1���� �� �߻��մϴ�.(���� 3 ���� ȭ�� )
                archer.isUpgradeTwoShot = true;
                Debug.Log("Debug11 archer");
                levi.upgradeNum = 11;
                break;
            case UpgradeType.DieInstantly:                                                          // ������ ������ ���� Ÿ�� �� ���� Ȯ���� ��� ��ŵ�ϴ�
                archer.isUpgradeDieInstantly = true;
                Debug.Log("Debug12 archer");
                levi.upgradeNum = 12;
                break;
            case UpgradeType.SameEnemyDamageUp:                                                     // ���� ����� ���� �� �� �����ð� ������� �����մϴ�. ���ӽð� : N��
                archer.isUpgradeSameEnemyDamage = true;
                Debug.Log("Debug13 archer");
                levi.upgradeNum = 13;
                break;
            case UpgradeType.SkillProjectileCountUp:                                                // ��ų ����ü ���� ����
                archer.skillProjectileCount += 6;
                Debug.Log("Debug14 archer");
                levi.upgradeNum = 14;
                break;

        }

        Debug.Log("���׷��̵� ����");
    }
}
