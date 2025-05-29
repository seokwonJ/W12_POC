using UnityEngine;

public class Shopper : MonoBehaviour // ������ �����ϰ� �����ϴ� ���� �ý���
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
            Debug.Log("ĳ���͸� ã�� �� ����");
        }
    }

    private void BuyItem() // ��ư�� ������ ������ ����
    {
        Debug.Log("���� �õ�");

        Collider2D[] colliders = Physics2D.OverlapPointAll(player.transform.position); // ���̾� üũ �ʿ� (�÷��̾� �� �ݶ��̴��� ��ħ)

        if (colliders.Length == 0) return;

        foreach(Collider2D col in colliders)
        {
            Debug.Log(col.gameObject.name);
        }
    }
}
