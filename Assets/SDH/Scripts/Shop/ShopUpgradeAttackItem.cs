using TMPro;
using UnityEngine;

public class ShopUpgradeAttackItem : ShopItem // �������� �����ϴ� ������. �̰� �ణ �ӽ���
{
    [SerializeField] TextMeshProUGUI itemTxt;

    private void Start()
    {
        SetText();
    }

    public override void BuyItem()
    {
        if (Managers.Status.Gold < 50 * Managers.Status.DamagePlus + 50) return;

        Managers.Status.Gold -= (50 * Managers.Status.DamagePlus + 50);
        Managers.Status.DamagePlus += 1;
        SetText();
    }

    private void SetText()
    {
        itemTxt.text = "�߰� ���ݷ� ����\n" + (50 * Managers.Status.DamagePlus + 50).ToString() + "��\n(" + Managers.Status.DamagePlus.ToString() + " �� " + (Managers.Status.DamagePlus + 1).ToString() + ")";
    }
}
