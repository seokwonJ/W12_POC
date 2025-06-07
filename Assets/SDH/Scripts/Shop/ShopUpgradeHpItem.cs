using TMPro;
using UnityEngine;

public class ShopUpgradeHpItem : ShopItem // �ִ�ü���� �����ϴ� ������. �̰� �ణ �ӽ���
{
    [SerializeField] TextMeshProUGUI itemTxt;

    private void Start()
    {
        SetText();
    }

    public override void BuyItem()
    {
        if (Managers.Status.Gold < 2 * Managers.Status.MaxHp - 180) return;

        Managers.Status.Gold -= (int)(2 * Managers.Status.MaxHp - 180);
        Managers.Status.MaxHp += 10;
        SetText();
    }

    private void SetText()
    {
        itemTxt.text = "ü�� ���� " + (2 * Managers.Status.MaxHp - 180).ToString() + "��\n(" + Managers.Status.MaxHp.ToString() + " �� " + (Managers.Status.MaxHp + 10).ToString() + ")";
    }
}
