public class ShopNothing : ShopItem // 그냥 아무것도 아닌 코드
{
    public override void BuyItem()
    {
        if (Managers.Status.Gold < 10) return;

        Managers.Status.Gold -= 10;
    }
}
