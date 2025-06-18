using UnityEditor.Searcher;
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
        ManaRegenSpeedUPAbilityPowerUp,             // ���� ȸ�� �ӵ� ���� + ��ų ����� ����

        SkillProjectileCountUp,                     // ��ų�� ����ü +3
        AttackPerMana,                              // �⺻ ���� ���߽� ������ n% ä���ش�
        ManaMultipleSkillProjectileMultiple,        // �ִ� ������ 2�� ������ ��ų ����ü�� 2��
        NoMoreExplosionAttackDamageUp,              // �� �̻� ������ �����鼭 ���� ������� ���� ���� ���ݷ��� ���� �����Ѵ�
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        Pirate pirate = character.GetComponent<Pirate>();
        switch (type)
        {
            //-------------- �⺻ ���׷��̵� --------------
            case UpgradeType.AttackPowerUp:
                pirate.attackPowerUpNum += attackPowerUpPercent;                                    // �⺻ ������ ���
                Debug.Log("Debug0 pirate");
                break;
            case UpgradeType.AttackSpeedUp:
                pirate.attackSpeedUpNum += attackSpeedUpPercent;                                    // ��Ÿ ����
                Debug.Log("Debug1 pirate");
                pirate.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // ����ü �̵��ӵ� ����
                pirate.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 pirate");
                pirate.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // ź ũ�� ����
                pirate.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 pirate");
                pirate.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // �� �о�� ȿ�� ����
                pirate.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 pirate");
                pirate.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // ũ�� Ȯ�� ���
                pirate.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 pirate");
                pirate.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // ũ�� ���� ��� ����
                pirate.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 pirate");
                pirate.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // �� ����/���� ���� �Ÿ� Ȯ��
                pirate.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 pirate");
                pirate.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ���ݷ� ����
                pirate.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                pirate.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 pirate");
                pirate.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
                pirate.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                pirate.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 pirate");
                pirate.upgradeNum = 9;
                break;
            //-------------- Ư�� ���׷��̵� --------------


            case UpgradeType.SkillProjectileCountUp:                                                // ��ų�� ����ü +3
                pirate.skillShotCount += 3;
                Debug.Log("Debug10 pirate");
                pirate.upgradeNum = 10;
                break;
            case UpgradeType.AttackPerMana:                                                         // �⺻ ���� ���߽� ������ n% ä���ش�
                pirate.isAttackPerMana = true;
                Debug.Log("Debug11 pirate");
                pirate.upgradeNum = 11;
                break;
            case UpgradeType.ManaMultipleSkillProjectileMultiple:                                   // �ִ� ������ 2�� ������ ��ų ����ü�� 2��
                pirate.isManaMultipleSkillProjectileMultiple = true;
                Debug.Log("Debug12 pirate");
                pirate.upgradeNum = 12;
                break;
            case UpgradeType.NoMoreExplosionAttackDamageUp:                                         // �� �̻� ������ �����鼭 ���� ������� ���� ���� ���ݷ��� ���� �����Ѵ�
                pirate.isManaMultipleSkillProjectileMultiple = true;
                pirate.attackBase += 100;
                Debug.Log("Debug13 pirate");
                pirate.upgradeNum = 13;
                break;

        }

        Debug.Log("���׷��̵� ����");
    }
}