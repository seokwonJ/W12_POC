using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public virtual void BuyItem() // 상속받아서 사용하기. 기본값은 상점 나가기(필드가기)임
    {
        Managers.Stage.OnField = true;
    }
}
