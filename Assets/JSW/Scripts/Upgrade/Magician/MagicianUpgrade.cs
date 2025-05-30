using UnityEngine;

[CreateAssetMenu(fileName = "MagicianUpgrade", menuName = "Upgrades/MagicianUpgrade")]
public class MagicianUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,                        // ���� ����
        AbilityPower,                       // ap ����
        ManaRegen,                           // ���������� ����
        NormalAttackSize,                   // �Ϲݰ���ũ�� ����         
        NormalProjectileSpeed,              // �Ϲݰ��� ����ü �ӵ� ����
        AddAbilityPowerToNormalAttack,      // �Ϲݰ��� �������� ap �߰�
        SkillSize,                       // ��ų ũ�� ����
        SkillDamage,                     // ��ų ������ ����
        ProjectileSizePerMana,           // ���� Ŭ���� ���� ũ�� ����
        AutoTeleportToShipAfterFalls        // �迡�� 10�ʵ��� �ȵ��ƿ��� �ڵ����� ���ƿ�
    }
    public UpgradeType type;
    public float value;


    public override void ApplyUpgrade(GameObject character)
    {
        Magician magician = character.GetComponent<Magician>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                magician.normalFireInterval -= 0.2f;
                Debug.Log("Debug0 Magician");
                magician.upgradeNum = 0;
                break;
            case UpgradeType.AbilityPower:
                magician.abilityPower += 10;
                Debug.Log("Debug1 Magician");
                magician.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:
                magician.mpPerSecond += 3;
                Debug.Log("Debug2 Magician");
                magician.upgradeNum = 2;
                break;
            case UpgradeType.NormalAttackSize:
                magician.nomalAttackSize += 1;
                Debug.Log("Debug3 Magician");
                magician.upgradeNum = 3;
                break;
            case UpgradeType.NormalProjectileSpeed:
                magician.projectileSpeed += 10;
                Debug.Log("Debug4 Magician");
                magician.upgradeNum = 4;
                break;
            case UpgradeType.AddAbilityPowerToNormalAttack:
                magician.isAddAbilityPower = true;
                Debug.Log("Debug5 Magician");
                magician.upgradeNum = 5;
                break;
            case UpgradeType.SkillSize:
                magician.skillSize += 1;
                Debug.Log("Debug6 Magician");
                magician.upgradeNum = 6;
                break;
            case UpgradeType.SkillDamage:
                magician.skillDamage += 20;
                Debug.Log("Debug7 Magician");
                magician.upgradeNum = 7;
                break;
            case UpgradeType.ProjectileSizePerMana:
                magician.isnomalAttackSizePerMana = true;
                Debug.Log("Debug8 Magician");
                magician.upgradeNum = 8;
                break;
            case UpgradeType.AutoTeleportToShipAfterFalls:
                //archer.manaRegen += value;
                Debug.Log("Debug9 Magician");
                magician.upgradeNum = 9;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}
