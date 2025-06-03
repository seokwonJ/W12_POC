using TMPro;
using UnityEngine;

public class ShopUpgradeItem : ShopItem
{
    [SerializeField] int upgradeCharacterIndex;
    [SerializeField] TextMeshProUGUI goldTxt;

    public override void BuyItem()
    {
        if (Managers.Status.Gold < 100) return;

        Managers.Status.Gold -= 100;
        FindAnyObjectByType<SetUpgradeCanvas>().SetUpgrades(FindAnyObjectByType<TmpPlayerControl>().characters[upgradeCharacterIndex]);
        goldTxt.text = "���: " + Managers.Status.Gold.ToString();
    }
}
