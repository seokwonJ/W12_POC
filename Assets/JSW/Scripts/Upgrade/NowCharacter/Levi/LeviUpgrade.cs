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

        AttackNumPlusSkillDamageDown,               // 1���� ���ݿ� 1�� ������ �߰��մϴ� ��ų ������� ���� �����մϴ�
        SkillEndPlusSkillCountUp,                   // ������ �� �� ���� ��ų ����� 1ȸ �߰��մϴ� �ִ� nȸ���� �����մϴ�.(���׷��̵� �Ҷ� ���� �ִ�ġ ���� )
        AttackDurationUp,                           // �˱Ⱑ �� �ָ� ������ (���� : ������ ���ӽð��� �����Ѵ� )
        AttackPerDamageMinusNumMinus,               // ���� �� �� ���� �����ϴ� ������� �پ���
        SkillingHitAttack                           // ��ų�� �������� �������� ������� �ش�
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

            case UpgradeType.AttackNumPlusSkillDamageDown:                                          // 1���� ���ݿ� 1�� ������ �߰��մϴ� ��ų ������� ���� �����մϴ�
                levi.attackNum += 1;
                levi.abilityPower -= 100;
                Debug.Log("Debug10 archer");
                levi.upgradeNum = 10;
                break;
            case UpgradeType.SkillEndPlusSkillCountUp:                                               // ������ �� �� ���� ��ų ����� 1ȸ �߰��մϴ� �ִ� nȸ���� �����մϴ�.(���׷��̵� �Ҷ� ���� �ִ�ġ ���� )
                levi.isSkillEndPlusSkillCountUp = true;
                Debug.Log("Debug11 archer");
                levi.upgradeNum = 11;
                break;
            case UpgradeType.AttackDurationUp:                                                       // �˱Ⱑ �� �ָ� ������ (���� : ������ ���ӽð��� �����Ѵ� )
                levi.NormalAttackProjectileDuration += 1;
                Debug.Log("Debug12 archer");
                levi.upgradeNum = 12;
                break;
            case UpgradeType.AttackPerDamageMinusNumMinus:                                           // ���� �� �� ���� �����ϴ� ������� �پ���
                levi.attackPerDamageMinus -= 3;
                Debug.Log("Debug13 archer");
                levi.upgradeNum = 13;
                break;
            case UpgradeType.SkillingHitAttack:                                                      // ��ų�� �������� �������� ������� �ش�
                levi.isAttackWhileSkillUpgrade = true;
                Debug.Log("Debug14 archer");
                levi.upgradeNum = 14;
                break;

        }

        Debug.Log("���׷��̵� ����");
    }
}
