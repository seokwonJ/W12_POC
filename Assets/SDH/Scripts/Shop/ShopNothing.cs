public class ShopNothing : ShopItem // �׳� �ƹ��͵� �ƴ� �ڵ�
{
    public override void BuyItem()
    {
        if (Managers.Status.Gold < 10) return;

        Managers.Status.Gold -= 10;
    }
}
