using TMPro;
using UnityEngine;

public class ShopUpgradeHpItem : ShopItem // 최대체력을 증가하는 아이템. 이건 약간 임시임
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
        itemTxt.text = "체력 증가 " + (2 * Managers.Status.MaxHp - 180).ToString() + "원\n(" + Managers.Status.MaxHp.ToString() + " → " + (Managers.Status.MaxHp + 10).ToString() + ")";
    }
}
