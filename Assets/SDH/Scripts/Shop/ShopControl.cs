using TMPro;
using UnityEngine;

public class ShopControl : MonoBehaviour // 아이템 구매하고 적용하는 상점 시스템
{
    private LayerMask shopLayerMask;

    private void Awake()
    {
        shopLayerMask = ~LayerMask.GetMask("Character", "Flyer"); // 영웅(Character) 비행체(Flyer) 제외. 상점 상품 전용 레이어가 생긴다면 그렇게 넣는 것도 가능
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바로 구매. 상점에서 대시 끄는 기능 추가하기 @@@@@@@@@@@@@@@@@@
        {
            BuyItem();
        }
    }

    private void BuyItem() // 버튼을 누르면 아이템 구매
    {
        Collider2D collider = Physics2D.OverlapPoint(Managers.PlayerControl.NowPlayer.transform.position, shopLayerMask); // 사실 상품 콜라이더가 겹칠 일은 없으니 OverlapPoint만 해도 충분한 거 아닐까

        collider?.GetComponent<ShopItem>().BuyItem();
    }
}
