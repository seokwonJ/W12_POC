using TMPro;
using UnityEngine;

public class ShopControl : MonoBehaviour // ������ �����ϰ� �����ϴ� ���� �ý���
{
    private TextMeshProUGUI goldTxt;
    private Transform player;
    private LayerMask shopLayerMask;

    private void Awake()
    {
        shopLayerMask = ~LayerMask.GetMask("Character", "Flyer"); // ����(Character) ����ü(Flyer) ����. ���� ��ǰ ���� ���̾ ����ٸ� �׷��� �ִ� �͵� ����
    }

    private void Start()
    {
        FindPlayer();

        goldTxt.text = "���: " + Managers.Status.Gold.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // ����Ű�� ����
        {
            BuyItem();
        }
        if (Input.GetKeyDown(KeyCode.P)) // �ӽ� ���� ���� �Լ�
        {
            FindAnyObjectByType<TmpPlayerControl>().ToggleOnField();
        }
    }

    private void FindPlayer()
    {
        player = FindAnyObjectByType<PlayerMove>()?.transform;

        if (!player)
        {
            Debug.Log("ĳ���͸� ã�� �� ����");
        }
    }

    private void BuyItem() // ��ư�� ������ ������ ����
    {
        Collider2D collider = Physics2D.OverlapPoint(player.transform.position, shopLayerMask); // ��� ��ǰ �ݶ��̴��� ��ĥ ���� ������ OverlapPoint�� �ص� ����� �� �ƴұ�

        collider?.GetComponent<ShopItem>().BuyItem();
    }
}
