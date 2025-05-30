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
        UltimateExpansion,      // �ñر� ���� ����
        FallingSpeedToSkillDamage,     // �������� �ӵ��� �������� �� ������ ����
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
            case UpgradeType.AttackSpeed:
                tanker.normalFireInterval -= 0.1f;             
                Debug.Log("Debug0 Tanker");
                break;
            case UpgradeType.AttackPower:
                tanker.attackDamage += 10;                
                Debug.Log("Debug1 Tanker");
                tanker.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                    
                tanker.mpPerSecond += 3;
                Debug.Log("Debug2 Tanker");
                tanker.upgradeNum = 2;
                break;
            case UpgradeType.FallSpeed:                     
                tanker.maxFallSpeed += 10;
                Debug.Log("Debug3 Tanker");
                tanker.upgradeNum = 3;
                break;
            case UpgradeType.UltimateExpansion:
                tanker.skillRange *= 2;
                Debug.Log("Debug9 Tanker");
                tanker.upgradeNum = 4;
                break;
            case UpgradeType.FallingSpeedToSkillDamage:
                tanker.isFallingSpeedToSkillDamage = true;
                Debug.Log("Debug4 Tanker");
                tanker.upgradeNum = 5;
                break;
            case UpgradeType.NomalAttackKnockback:                
                tanker.knockBackpower += 10;
                Debug.Log("Debug5 Tanker");
                tanker.upgradeNum = 5;
                break;
            case UpgradeType.ShipDamageReduction:              
                tanker.isShieldFlyer = true;
                Debug.Log("Debug6 Tanker");
                tanker.upgradeNum = 7;
                break;
            case UpgradeType.HitSkillPerGetMana:                  
                tanker.isHitSkillPerGetMana = true;
                Debug.Log("Debug7 Tanker");
                tanker.upgradeNum = 8;
                break;
            case UpgradeType.CloserMoreDamage:                    
                tanker.isCloserMoreDamage = true;
                Debug.Log("Debug8 Tanker");
                tanker.upgradeNum = 9;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}
