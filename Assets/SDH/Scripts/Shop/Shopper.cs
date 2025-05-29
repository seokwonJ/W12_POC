using UnityEngine;

public class Shopper : MonoBehaviour // 아이템 구매하고 적용하는 상점 시스템
{
    private Transform player;

    private void Start()
    {
        FindPlayer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        Debug.Log("구매 시도");

        Collider2D[] colliders = Physics2D.OverlapPointAll(player.transform.position); // 레이어 체크 필요 (플레이어 내 콜라이더와 겹침)

        if (colliders.Length == 0) return;

        foreach(Collider2D col in colliders)
        {
            Debug.Log(col.gameObject.name);
        }
    }
}
