using UnityEditor.Searcher;
using UnityEngine;

[CreateAssetMenu(fileName = "SniperUpgrade", menuName = "Upgrades/SniperUpgrade")]
public class SniperUpgrade : CharacterUpgrade
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

        SkillCountUp,                               // ��ų�� ����ü + 1
        ReloadTimeDown,                             // ����źâ : ���� �ð��� N% �پ���
        NoMoreSkill,                                // �� �̻� ��ų�� ��� �� �� ����(���� ������� 0�� �ȴ� )
        NoMorePenetrationAttackUp,                  // �� �̻� ������ ���� ������ ���ݷ��� ��������� �����Ѵ�
        PenetrationPerDamageUp                      // ���� �� ������ ������� �����Ѵ�
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        Sniper sniper = character.GetComponent<Sniper>();
        switch (type)
        {
            //-------------- �⺻ ���׷��̵� --------------
            case UpgradeType.AttackPowerUp:
                sniper.attackPowerUpNum += attackPowerUpPercent;                                    // �⺻ ������ ���
                Debug.Log("Debug0 sniper");
                break;
            case UpgradeType.AttackSpeedUp:
                sniper.attackSpeedUpNum += attackSpeedUpPercent;                                    // ��Ÿ ����
                Debug.Log("Debug1 sniper");
                sniper.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // ����ü �̵��ӵ� ����
                sniper.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 sniper");
                sniper.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // ź ũ�� ����
                sniper.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 sniper");
                sniper.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // �� �о�� ȿ�� ����
                sniper.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 sniper");
                sniper.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // ũ�� Ȯ�� ���
                sniper.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 sniper");
                sniper.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // ũ�� ���� ��� ����
                sniper.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 sniper");
                sniper.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // �� ����/���� ���� �Ÿ� Ȯ��
                sniper.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 sniper");
                sniper.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ���ݷ� ����
                sniper.manaRegenSpeedUpNum -= ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                sniper.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 sniper");
                sniper.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                        // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
                sniper.manaRegenSpeedUpNum += ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent;
                sniper.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent;
                Debug.Log("Debug9 sniper");
                sniper.upgradeNum = 9;
                break;


            //-------------- Ư�� ���׷��̵� --------------

            case UpgradeType.SkillCountUp:                                                          // ��ų�� ����ü + 1
                sniper.skillCount += 1;
                Debug.Log("Debug10 archer");
                sniper.upgradeNum = 10;
                break;
            case UpgradeType.ReloadTimeDown:                                                        // ����źâ : ���� �ð��� N% �پ���
                sniper.realoadTime -= sniper.realoadTime * 0.3f;
                Debug.Log("Debug11 archer");
                sniper.upgradeNum = 11;
                break;
            case UpgradeType.NoMoreSkill:                                                           // �� �̻� ��ų�� ��� �� �� ����(���� ������� 0�� �ȴ� )
                sniper.mpPerSecond = 0;
                Debug.Log("Debug12 archer");
                sniper.upgradeNum = 12;
                break;
            case UpgradeType.NoMorePenetrationAttackUp:                                             // �� �̻� ������ ���� ������ ���ݷ��� ��������� �����Ѵ�
                sniper.isNoMorePenetrationAttackUp = true;
                sniper.attackBase += 30;
                Debug.Log("Debug13 archer");
                sniper.upgradeNum = 13;
                break;
            case UpgradeType.PenetrationPerDamageUp:                                                // ���� �� ������ ������� �����Ѵ�
                sniper.isPenetrationPerDamageUp = true;
                Debug.Log("Debug13 archer");
                sniper.upgradeNum = 13;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}
