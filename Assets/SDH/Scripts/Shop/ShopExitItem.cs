using UnityEngine;

public class ShopExitItem : ShopItem
{
    public override void BuyItem() // ���� ������
    {
        Managers.Stage.OnField = true;
    }
}
