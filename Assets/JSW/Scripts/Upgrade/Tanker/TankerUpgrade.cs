using UnityEditor.Searcher;
using UnityEngine;

[CreateAssetMenu(fileName = "TankerUpgrade", menuName = "Upgrades/TankerUpgrade")]
public class TankerUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // �Ϲ� ���� �ӵ� ���� (��Ÿ�� ����)
        AttackPower,       // ���ݷ� ����
        ManaRegen,         // ���� �ʴ� ������ ����
        FallSpeed,          // �������� �ӵ� ����
        SkillExpansion,      // ��ų ���� ����
        FallingSpeedToSkillDamage,     // �������� �ӵ��� �������� ��ų ������ ����
        NomalAttackKnockback,       // �Ϲݰ��� �˹� ������
        ShipDamageReduction,       // �迡 Ÿ������ �谡 �޴� ������ ���� �ٿ���
        HitSkillPerGetMana,        // �з��� ���� ����ŭ ���� ȹ��
        CloserMoreDamage         // �Ÿ��� ����� ���� ������ �� ��
    }

    public UpgradeType type;
    public float value;

    public override void ApplyUpgrade(GameObject character)
    {
        Tanker tanker = character.GetComponent<Tanker>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:               // �Ϲ� ���� �ӵ� ���� (��Ÿ�� ����)
                tanker.normalFireInterval -= 0.5f;             
                Debug.Log("Debug0 Tanker");
                break;
            case UpgradeType.AttackPower:               // ���ݷ� ����
                tanker.attackDamage += 10;                
                Debug.Log("Debug1 Tanker");
                tanker.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                 // ���� �ʴ� ������ ����  
                tanker.mpPerSecond += 3;
                Debug.Log("Debug2 Tanker");
                tanker.upgradeNum = 2;
                break;
            case UpgradeType.FallSpeed:                  // �������� �ӵ� ����
                tanker.maxFallSpeed += 10;
                Debug.Log("Debug3 Tanker");
                tanker.upgradeNum = 3;
                break;
            case UpgradeType.SkillExpansion:            // ��ų ���� ����
                tanker.skillRange += 3;
                Debug.Log("Debug9 Tanker");
                tanker.upgradeNum = 4;
                break;
            case UpgradeType.FallingSpeedToSkillDamage:     // �������� �ӵ��� �������� ��ų ������ ����
                tanker.isFallingSpeedToSkillDamage = true;
                Debug.Log("Debug4 Tanker");
                tanker.upgradeNum = 5;
                break;
            case UpgradeType.NomalAttackKnockback:          // �Ϲݰ��� �˹� ������      
                tanker.knockBackpower += 10;
                Debug.Log("Debug5 Tanker");
                tanker.upgradeNum = 5;
                break;
            case UpgradeType.ShipDamageReduction:           // �迡 Ÿ������ �谡 �޴� ������ ���� �ٿ��� 
                tanker.isShieldFlyer = true;
                Debug.Log("Debug6 Tanker");
                tanker.upgradeNum = 7;
                break;
            case UpgradeType.HitSkillPerGetMana:            // �з��� ���� ����ŭ ���� ȹ��         
                tanker.isHitSkillPerGetMana = true;
                Debug.Log("Debug7 Tanker");
                tanker.upgradeNum = 8;
                break;
            case UpgradeType.CloserMoreDamage:               // �Ÿ��� ����� ���� ������ �� ��
                tanker.isCloserMoreDamage = true;
                Debug.Log("Debug8 Tanker");
                tanker.upgradeNum = 9;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}
