using TMPro;
using UnityEngine;

public class ShopControl : MonoBehaviour // ������ �����ϰ� �����ϴ� ���� �ý���
{
    private LayerMask hireLayerMask; // ���� ���� �� ������ ���� ���̾�
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
        hireLayerMask = LayerMask.GetMask("UI"); // ��� ����â�� ĵ������ ������ ���̾�� UI
        shopLayerMask = ~LayerMask.GetMask("Character", "Flyer"); // ����(Character) ����ü(Flyer) ����. ���� ��ǰ ���� ���̾ ����ٸ� �׷��� �ִ� �͵� ����

        isHired = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �����̽��ٷ� ����. �������� ��� ���� ��� �߰��ϱ� @@@@@@@@@@@@@@@@@@
        {
            BuyItem();
        }
    }

    private void BuyItem() // ��ư�� ������ ������ ����
    {
        LayerMask nowLayerMask = isHired ? shopLayerMask : hireLayerMask;
        Collider2D collider = Physics2D.OverlapPoint(Managers.PlayerControl.NowPlayer.transform.position, nowLayerMask);

        collider?.GetComponent<ShopItem>()?.BuyItem();
    }
}
