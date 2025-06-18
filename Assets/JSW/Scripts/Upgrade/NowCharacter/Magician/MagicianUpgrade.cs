using UnityEditor.Searcher;
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
        ManaRegenSpeedUPAbilityPowerUp,             // ���� ȸ�� �ӵ� ���� + ��ų ����� ����

        TenAttackSkillAttack,                       // �⺻������ 10ȸ �� ��Ÿ�� ��ų ����ü�� �߻��Ѵ�
        SkillAttackCountUp,                         // ��ų�� 1ȸ �� �����մϴ�
        SkillAttackSizeUp,                          // ��ų ����ü�� ũ�Ⱑ �����մϴ�
        SkillExplosionAttack,                         // ���ӽð��� ������ ������ ������� �ش�
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        Magician magician = character.GetComponent<Magician>();
        switch (type)
        {
            //-------------- �⺻ ���׷��̵� --------------
            case UpgradeType.AttackPowerUp:
                magician.attackPowerUpNum += attackPowerUpPercent;                                    // �⺻ ������ ���
                Debug.Log("Debug0 magician");
                break;
            case UpgradeType.AttackSpeedUp:
                magician.attackSpeedUpNum += attackSpeedUpPercent;                                    // ��Ÿ ����
                Debug.Log("Debug1 magician");
                magician.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // ����ü �̵��ӵ� ����
                magician.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 magician");
                magician.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // ź ũ�� ����
                magician.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 magician");
                magician.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // �� �о�� ȿ�� ����
                magician.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 magician");
                magician.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // ũ�� Ȯ�� ���
                magician.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 magician");
                magician.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // ũ�� ���� ��� ����
                magician.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 magician");
                magician.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // �� ����/���� ���� �Ÿ� Ȯ��
                magician.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 magician");
                magician.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ���ݷ� ����
                magician.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                magician.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 magician");
                magician.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
                magician.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                magician.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 magician");
                magician.upgradeNum = 9;
                break;

            //-------------- Ư�� ���׷��̵� --------------

            case UpgradeType.TenAttackSkillAttack:                                                  // �⺻������ 10ȸ �� ��Ÿ�� ��ų ����ü�� �߻��Ѵ�
                magician.isUpgradeTenAttackSkillAttack = true;
                Debug.Log("Debug10 magician");
                magician.upgradeNum = 10;
                break;
            case UpgradeType.SkillAttackCountUp:                                                    // ��ų�� 1ȸ �� �����մϴ�
                magician.skillCount += 1;
                Debug.Log("Debug11 magician");
                magician.upgradeNum = 11;
                break;
            case UpgradeType.SkillAttackSizeUp:                                                     // ��ų ����ü�� ũ�Ⱑ �����մϴ�
                magician.skillSize += 1;
                Debug.Log("Debug12 magician");
                magician.upgradeNum = 12;
                break;
            case UpgradeType.SkillExplosionAttack:                                                  // ���ӽð��� ������ ������ ������� �ش�
                magician.isUpgradeSkillExplosionAttack = true;
                Debug.Log("Debug13 magician");
                magician.upgradeNum = 13;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}
