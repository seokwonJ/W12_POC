using TMPro;
using UnityEngine;

public class ShopUpgradeCharacterItem : ShopItem
{
    [SerializeField] int upgradeCharacterIndex;

    private void Start()
    {
        Instantiate(Managers.PlayerControl.Characters[upgradeCharacterIndex].transform.GetChild(0), transform);
    }

    public override void BuyItem()
    {
        if (Managers.Status.Gold < 100) return;
        //if (Managers.SceneFlow.SetUpgradeCanvasCS == null) Debug.Log("SetUpgradeCanvas ¸øÃ£À½");

        Managers.Status.Gold -= 100;
        //Managers.SceneFlow.SetUpgradeCanvasCS.SetUpgrades(Managers.PlayerControl.Characters[upgradeCharacterIndex]);
    }
}
