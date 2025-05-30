using UnityEditor.Searcher;
using UnityEngine;

[CreateAssetMenu(fileName = "WorriorUpgrade", menuName = "Upgrades/WorriorUpgrade")]
public class WorriorUpgrade : CharacterUpgrade
{
    public enum UpgradeType
    {
        AttackSpeed,       // �Ϲ� ���� �ӵ� ���� (��Ÿ�� ����)
        AttackPower,       // ���ݷ� ����
        ManaRegen,         // ���� �ʴ� ������ ����
        NormalAttackSize,         // �Ϲ� ���� ũ�� ����
        NormalProjectileSpeed,    // �Ϲݰ��� ����ü �ӵ� ����
        FallSpeed,     // �������� �ӵ� ����
        SkillProjectileCount,   // �ñر� ������ ���� ����
        NormalProjectileLifetime,       // �Ϲݰ��� ������Ÿ�� ����
        CollisionOnFallDamage,        // �������鼭 ���� �ε��� ��� ������ ���� 
        ShipDamageReduction         // �迡 Ÿ������ �谡 �޴� ������ ���� �ٿ���
    }
    public UpgradeType type;
    public float value;


    public override void ApplyUpgrade(GameObject character)
    {
        Worrior worrior = character.GetComponent<Worrior>();
        switch (type)
        {
            case UpgradeType.AttackSpeed:
                worrior.normalFireInterval -= 0.1f;              // ���� ��Ÿ�� 0.1�� ����
                Debug.Log("Debug0 Worrior");
                break;
            case UpgradeType.AttackPower:
                worrior.attackDamage += 10;                    // ad 10 ����
                Debug.Log("Debug1 Worrior");
                worrior.upgradeNum = 1;
                break;
            case UpgradeType.ManaRegen:                      // �ʴ� mp ������ 3����
                worrior.mpPerSecond += 3;
                Debug.Log("Debug2 Worrior");
                worrior.upgradeNum = 2;
                break;
            case UpgradeType.NormalAttackSize:                     // nomalAttackSize  1.5�� ����
                worrior.nomalAttackSize *= 1.5f;
                Debug.Log("Debug3 Worrior");
                worrior.upgradeNum = 3;
                break;
            case UpgradeType.NormalProjectileSpeed:                    // ����ü �ӵ� 15����
                Debug.Log("Debug9 Worrior");
                worrior.upgradeNum = 4;
                break;
            case UpgradeType.FallSpeed:                         // maxfallsize  -10
                worrior.maxFallSpeed += 10;
                Debug.Log("Debug4 Worrior");
                worrior.upgradeNum = 5;
                break;
            case UpgradeType.SkillProjectileCount:                 // ��ų Ƚ���� +2 ����
                worrior.skillCount += 2;
                Debug.Log("Debug5 Worrior");
                worrior.upgradeNum = 5;
                break;
            case UpgradeType.NormalProjectileLifetime:               // attack�����ð� 5�÷���
                worrior.nomalAttackLifetime += 5;
                Debug.Log("Debug6 Worrior");
                worrior.upgradeNum = 7;
                break;
            case UpgradeType.CollisionOnFallDamage:                   //  �������鼭 ���� �ε��� ��� ������ ���� true�� �ٲ���
                worrior.isfallingCanAttack = true;
                Debug.Log("Debug7 Worrior");
                worrior.upgradeNum = 8;
                break;
            case UpgradeType.ShipDamageReduction:                    // �迡 Ÿ������ �谡 �޴� ������ ���� �ٿ��� true�� �ٲ���
                worrior.isShieldFlyer = true;
                Debug.Log("Debug8 Worrior");
                worrior.upgradeNum = 9;
                break;
        }

        Debug.Log("���׷��̵� ����");
    }
}
