using UnityEditor.Searcher;
using UnityEngine;

[CreateAssetMenu(fileName = "NinjaUpgrade", menuName = "Upgrades/NinjaUpgrade")]
public class NinjaUpgrade : CharacterUpgrade
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

        SkillDurationUp,                            // ��ų�� ���ӽð��� �����մϴ�.
        SkillDamageUp,                              // ��ų�� ����ϴ� ���ݷ� �������� �����մϴ�.
        SkillCriticalDamageUp,                      // ��ų ���� ġ��Ÿ �������� �����մϴ�.
        AttackFiveDamageUp,                         // ��Ÿ 5��° �⺻������ �������� ��ȭ�˴ϴ�.
        LongAttackDamageUp,                         // �Ÿ��� �� ���� �⺻������ �������� �����մϴ� 
        ManaPerDamageUp                             // �Ҹ��� �������� ���� �������� �����մϴ�.
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        Ninja ninja = character.GetComponent<Ninja>();
        switch (type)
        {
            //-------------- �⺻ ���׷��̵� --------------
            case UpgradeType.AttackPowerUp:
                ninja.attackPowerUpNum += attackPowerUpPercent;                                    // �⺻ ������ ���
                Debug.Log("Debug0 ninja");
                break;
            case UpgradeType.AttackSpeedUp:
                ninja.attackSpeedUpNum += attackSpeedUpPercent;                                    // ��Ÿ ����
                Debug.Log("Debug1 ninja");
                ninja.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // ����ü �̵��ӵ� ����
                ninja.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 ninja");
                ninja.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // ź ũ�� ����
                ninja.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 ninja");
                ninja.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // �� �о�� ȿ�� ����
                ninja.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 ninja");
                ninja.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // ũ�� Ȯ�� ���
                ninja.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 ninja");
                ninja.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // ũ�� ���� ��� ����
                ninja.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 ninja");
                ninja.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // �� ����/���� ���� �Ÿ� Ȯ��
                ninja.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 ninja");
                ninja.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ���ݷ� ����
                ninja.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                ninja.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 ninja");
                ninja.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                        // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
                ninja.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                ninja.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 ninja");
                ninja.upgradeNum = 9;
                break;

            //-------------- Ư�� ���׷��̵� --------------

            case UpgradeType.SkillDurationUp:                                                        // ��ų�� ���ӽð��� �����մϴ�.
                ninja.skillPowerDuration += 3;
                Debug.Log("Debug10 ninja");
                ninja.upgradeNum = 10;
                break; 
            case UpgradeType.SkillDamageUp:                                                         // ��ų�� ����ϴ� ���ݷ� �������� �����մϴ�.
                ninja.skillDamageUp = 10;
                Debug.Log("Debug11 ninja");
                ninja.upgradeNum = 11;
                break;
            case UpgradeType.SkillCriticalDamageUp:                                                 // ��ų ���� ġ��Ÿ �������� �����մϴ�.
                ninja.isSkillCriticalDamageUp = true;
                Debug.Log("Debug12 ninja");
                ninja.upgradeNum = 12;
                break;
            case UpgradeType.AttackFiveDamageUp:                                                    // ��Ÿ 5��° �⺻������ �������� ��ȭ�˴ϴ�.
                ninja.isNomalAttackFive = true;
                ninja.upgradeNum = 13;
                break;
            case UpgradeType.LongAttackDamageUp:                                                    // �Ÿ��� �� ���� �⺻������ �������� �����մϴ� 
                ninja.isLongAttackDamageUp = true;
                Debug.Log("Debug14 ninja");
                ninja.upgradeNum = 14;
                break;
            case UpgradeType.ManaPerDamageUp:                                                       // �Ҹ��� �������� ���� �������� �����մϴ�.
                ninja.isManaPerDamageUp = true;
                Debug.Log("Debug14 ninja");
                ninja.upgradeNum = 15;
                break;

        }

        Debug.Log("���׷��̵� ����");
    }
}
