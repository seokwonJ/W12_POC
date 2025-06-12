using UnityEngine;

public class ShopExitItem : ShopItem
{
    public override void BuyItem() // 상점 나가기
    {
        Managers.Stage.OnField = true;
    }
}
