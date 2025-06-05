using TMPro;
using UnityEngine;

public class ShopControl : MonoBehaviour // ������ �����ϰ� �����ϴ� ���� �ý���
{
    private LayerMask shopLayerMask;

    private void Awake()
    {
        shopLayerMask = ~LayerMask.GetMask("Character", "Flyer"); // ����(Character) ����ü(Flyer) ����. ���� ��ǰ ���� ���̾ ����ٸ� �׷��� �ִ� �͵� ����
    }

    private void Start()
    {
        Managers.PlayerControl.NowPlayer.transform.position = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // ����Ű�� ����
        {
            BuyItem();
        }
    }

    private void BuyItem() // ��ư�� ������ ������ ����
    {
        Collider2D collider = Physics2D.OverlapPoint(Managers.PlayerControl.NowPlayer.transform.position, shopLayerMask); // ��� ��ǰ �ݶ��̴��� ��ĥ ���� ������ OverlapPoint�� �ص� ����� �� �ƴұ�

        collider?.GetComponent<ShopItem>().BuyItem();
    }
}
