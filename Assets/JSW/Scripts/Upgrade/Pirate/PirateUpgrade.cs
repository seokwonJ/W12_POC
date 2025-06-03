using UnityEngine;

[CreateAssetMenu(fileName = "PirateUpgrade", menuName = "Upgrades/PirateUpgrade")]
public class PirateUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // �Ϲ� ���� �ӵ� ���� (��Ÿ�� ����)
        AttackPower,       // ���ݷ� ����
        ManaRegen,         // ���� �ʴ� ������ ����
        IncreasedCannonBlastRadius,         // ���� ���� ���� ����
        FasterCannonProjectile,    // ���� ����ü �ӵ� ����
        MoreSkillCannonShots,     // �� ���� ����ü ���� ����
        ReducedJumpPower,   // ������ ������
        BackwardCannonShot,       // �Ϲ� ���� �� �� ���ݴ� �������ε� �� �� ��
        MoreSkillShots,        // ��ų �ѹ� �� ��
        FirstHitDealsBonusDamage         // ���� ���� ����ü �´� ���� ū ������ ����
    }
    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        Pirate pirate = character.GetComponent<Pirate>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                pirate.normalFireInterval -= 0.2f;              // ���� ��Ÿ�� 0.1�� ����
                Debug.Log("Debug0 Archer");
                break;
            case UpgradeType.AttackPower:
                pirate.attackDamage += 10;                    // ad 10 ����
                Debug.Log("Debug1 Archer");
                pirate.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // �ʴ� mp ������ 3����
                pirate.mpPerSecond += 3;
                Debug.Log("Debug2 Archer");
                pirate.upgradeNum = 2;
                break;
            case UpgradeType.IncreasedCannonBlastRadius:                    // ���� ���� ���� ����
                pirate.nomalAttackSize += 0.5f;
                Debug.Log("Debug3 Archer");
                pirate.upgradeNum = 3;
                break;
            case UpgradeType.FasterCannonProjectile:                // ���� ����ü �ӵ� ����
                pirate.projectileSpeed += 10;
                Debug.Log("Debug4 Archer");
                pirate.upgradeNum = 4;
                break;
            case UpgradeType.MoreSkillCannonShots:                 // �� ���� ����ü ���� ����
                pirate.skillShotCount += 3;
                Debug.Log("Debug5 Archer");
                pirate.upgradeNum = 5;
                break;
            case UpgradeType.ReducedJumpPower:               // ������ ������
                pirate.jumpForce -= 2;
                Debug.Log("Debug6 Archer");
                pirate.upgradeNum = 6;
                break;
            case UpgradeType.BackwardCannonShot:                   // �Ϲ� ���� �� �� ���ݴ� �������ε� �� �� ��
                pirate.isBackwardCannonShot = true;
                Debug.Log("Debug7 Archer");
                pirate.upgradeNum = 7;
                break;
            case UpgradeType.MoreSkillShots:                    // ��ų �ѹ� �� ��
                pirate.skillCount += 1;
                Debug.Log("Debug8 Archer");
                pirate.upgradeNum = 8;
                break;
            case UpgradeType.FirstHitDealsBonusDamage:                     // ���� ���� ����ü �´� ���� ū ������ ����
                pirate.isFirstHitDealsBonusDamage = true;
                Debug.Log("Debug9 Archer");
                pirate.upgradeNum = 9;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}