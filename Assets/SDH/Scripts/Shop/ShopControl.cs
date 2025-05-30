using System.Collections;
using UnityEngine;

public class ShopControl : MonoBehaviour // 아이템 구매하고 적용하는 상점 시스템
{
    private Transform player;
    private LayerMask shopLayerMask;

    private void Awake()
    {
        shopLayerMask = ~LayerMask.GetMask("Character", "Flyer");
    }

    private void Start()
    {
        FindPlayer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            BuyItem();
        }
    }

    private void FindPlayer()
    {
        player = FindAnyObjectByType<PlayerMove>().transform;

        if (!player)
        {
            Debug.Log("캐릭터를 찾을 수 없음");
        }
    }

    private void BuyItem() // 버튼을 누르면 아이템 구매
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(player.transform.position, shopLayerMask);

        if (colliders.Length == 0) return;

        foreach(Collider2D col in colliders)
        {
            Debug.Log(col.gameObject.name);
        }
    }
}
