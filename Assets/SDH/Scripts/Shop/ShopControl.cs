using TMPro;
using UnityEngine;

public class ShopControl : MonoBehaviour // 아이템 구매하고 적용하는 상점 시스템
{
    private LayerMask hireLayerMask; // 상점 진입 전 고용시점 전용 레이어
    private LayerMask shopLayerMask;
    public bool IsHired
    {
        get
        {
            return isHired;
        }
        set
        {
            isHired = value;
        }
    }
    private bool isHired;

    private void Awake()
    {
        hireLayerMask = LayerMask.GetMask("UI"); // 고용 선택창은 캔버스기 때문에 레이어는 UI
        shopLayerMask = ~LayerMask.GetMask("Character", "Flyer"); // 영웅(Character) 비행체(Flyer) 제외. 상점 상품 전용 레이어가 생긴다면 그렇게 넣는 것도 가능

        isHired = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // 엔터키로 구매
        {
            BuyItem();
        }
    }

    private void BuyItem() // 버튼을 누르면 아이템 구매
    {
        Collider2D collider = isHired ? Physics2D.OverlapPoint(Managers.PlayerControl.NowPlayer.transform.position, shopLayerMask) : Physics2D.OverlapPoint(Managers.PlayerControl.NowPlayer.transform.position, hireLayerMask);

        collider?.GetComponent<ShopItem>()?.BuyItem();
    }
}
