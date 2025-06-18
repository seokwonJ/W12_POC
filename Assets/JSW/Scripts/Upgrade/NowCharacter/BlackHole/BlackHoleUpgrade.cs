using UnityEditor.Searcher;
using UnityEngine;


[CreateAssetMenu(fileName = "BlackHoleUpgrade", menuName = "Upgrades/BlackHoleUpgrade")]
public class BlackHoleUpgrade : CharacterUpgrade
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

        SkillDurationUp,                            // ��ų�� ���ӽð� ����.
        SkillAttackDlaySpeedUp,                     // ��Ȧ�� ������� �ִ� �ֱⰡ �� ��������
        SkillSizeDownExplosion,                     // ��Ȧ�� ����� 1/2�� �پ��� ��� ��Ȧ�� ������� ���� �����ؼ� ������� �ش�
        SkillDenfenseDown,                          // ��Ȧ�� ������ ������ ���� ��ŵ�ϴ�
    }

    public UpgradeType type;

    public override void ApplyUpgrade(GameObject character)
    {
        BlackHole blackHole = character.GetComponent<BlackHole>();
        switch (type)
        {
            //-------------- �⺻ ���׷��̵� --------------
            case UpgradeType.AttackPowerUp:
                blackHole.attackPowerUpNum += attackPowerUpPercent;                                    // �⺻ ������ ���
                Debug.Log("Debug0 blackHole");
                break;
            case UpgradeType.AttackSpeedUp:
                blackHole.attackSpeedUpNum += attackSpeedUpPercent;                                    // ��Ÿ ����
                Debug.Log("Debug1 blackHole");
                blackHole.upgradeNum = 1;
                break;
            case UpgradeType.ProjectileSpeedUp:                                                     // ����ü �̵��ӵ� ����
                blackHole.projectileSpeedUpNum += ProjectileSpeedUpPercent;
                Debug.Log("Debug2 blackHole");
                blackHole.upgradeNum = 2;
                break;
            case UpgradeType.ProjectileSizeUp:                                                      // ź ũ�� ����
                blackHole.projectileSizeUpNum += ProjectileSizeUpPercent;
                Debug.Log("Debug3 blackHole");
                blackHole.upgradeNum = 3;
                break;
            case UpgradeType.KnockbackPowerUp:                                                      // �� �о�� ȿ�� ����
                blackHole.knockbackPowerUpNum += KnockbackPowerUpPercent;
                Debug.Log("Debug4 blackHole");
                blackHole.upgradeNum = 4;
                break;
            case UpgradeType.CriticalProbabilityUp:                                                 // ũ�� Ȯ�� ���
                blackHole.criticalProbabilityUpNum += CriticalProbabilityUpPercent;
                Debug.Log("Debug5 blackHole");
                blackHole.upgradeNum = 5;
                break;
            case UpgradeType.CriticalDamageUp:                                                      // ũ�� ���� ��� ����
                blackHole.criticalDamageUpNum += CriticalDamageUpPercent;
                Debug.Log("Debug6 blackHole");
                blackHole.upgradeNum = 6;
                break;
            case UpgradeType.AttackRangeUp:                                                         // �� ����/���� ���� �Ÿ� Ȯ��
                blackHole.attackRangeUpNum += AttackRangeUpPercent;
                Debug.Log("Debug7 blackHole");
                blackHole.upgradeNum = 7;
                break;
            case UpgradeType.ManaRegenSpeedDownAttackPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ���ݷ� ����
                blackHole.manaRegenSpeedUpNum += ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
                blackHole.attackPowerUpNum += ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
                Debug.Log("Debug8 blackHole");
                blackHole.upgradeNum = 8;
                break;
            case UpgradeType.ManaRegenSpeedUPAbilityPowerUp:                                       // ���� ȸ�� �ӵ� ���� + ��ų ����� ����
                blackHole.manaRegenSpeedUpNum += ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
                blackHole.abilityPowerUpNum += ManaRegenSpeedUPAbilityPowerDown_AbilityPowerPercent;
                Debug.Log("Debug9 blackHole");
                blackHole.upgradeNum = 9;
                break;

            //-------------- Ư�� ���׷��̵� --------------

            case UpgradeType.SkillDurationUp:                                                       // ��ų�� ���ӽð� ����.
                blackHole.skillDuration += 2;
                Debug.Log("Debug10 blackHole");
                blackHole.upgradeNum = 10;
                break;
            case UpgradeType.SkillAttackDlaySpeedUp:                                                // ��Ȧ�� ������� �ִ� �ֱⰡ �� ��������
                blackHole.skillPullInterval -= 0.1f;
                Debug.Log("Debug11 blackHole");
                blackHole.upgradeNum = 11;
                break;
            case UpgradeType.SkillSizeDownExplosion:                                                // ��Ȧ�� ����� 1/2�� �پ��� ��� ��Ȧ�� ������� ���� �����ؼ� ������� �ش�
                blackHole.isUpgradeSkillSizeDownExplosion = true;
                Debug.Log("Debug12 blackHole");
                blackHole.upgradeNum = 12;
                break;
            case UpgradeType.SkillDenfenseDown:                                                     // ��Ȧ�� ������ ������ ���� ��ŵ�ϴ�
                blackHole.isUpgradeSkillEnemyDenfenseDown = true;
                Debug.Log("Debug13 blackHole");
                blackHole.upgradeNum = 13;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}
