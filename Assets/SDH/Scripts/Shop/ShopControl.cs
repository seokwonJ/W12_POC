using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopControl : MonoBehaviour // ������ �����ϰ� �����ϴ� ���� �ý���
{
    private Transform player;
    private LayerMask shopLayerMask;

    private void Awake()
    {
        shopLayerMask = ~LayerMask.GetMask("Character", "Flyer"); // ����(Character) ����ü(Flyer) ����. ���� ��ǰ ���� ���̾ ����ٸ� �׷��� �ִ� �͵� ����
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
        if (Input.GetKeyDown(KeyCode.P)) // �ӽ� ���� ���� �Լ�
        {
            SceneManager.LoadScene("JJHStageScene_SDH");
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
        Collider2D[] colliders = Physics2D.OverlapPointAll(player.transform.position, shopLayerMask); // ��� ��ǰ �ݶ��̴��� ��ĥ ���� ������ OverlapPoint�� �ص� ����� �� �ƴұ�

        if (colliders.Length == 0) return;

        foreach(Collider2D col in colliders)
        {
            Debug.Log(col.gameObject.name);
        }
    }
}
