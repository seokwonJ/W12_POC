using TMPro;
using UnityEngine;

public class ShopUpgradeAttackItem : ShopItem // 데미지를 증가하는 아이템. 이건 약간 임시임
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
        itemTxt.text = "추가 공격력 증가\n" + (50 * Managers.Status.DamagePlus + 50).ToString() + "원\n(" + Managers.Status.DamagePlus.ToString() + " → " + (Managers.Status.DamagePlus + 1).ToString() + ")";
    }
}
