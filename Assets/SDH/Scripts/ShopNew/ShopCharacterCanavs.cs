using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ShopCharacterCanavs : MonoBehaviour
{
    [SerializeField] private SetUpgradeCanvas setUpgradeCanvas;
    [SerializeField] private Transform selectCursor;
    private int nowShopSelectIdx;

    private void Start()
    {
        //isOn = false; // ������ �� ���â�� ���� ���� �ϹǷ� false

        SetNowShopSelect(1);
    }

    public void StartGetInput()
    {
        SetShopCharacter();
        StartCoroutine(GetInput());
    }

    private IEnumerator GetInput()
    {
        yield return null;

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ActNowShopSelect();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SetNowShopSelect(nowShopSelectIdx + 1); // �ε��� �� �ٸ� �͵�� ������ �ݴ��ӿ� ����
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SetNowShopSelect(nowShopSelectIdx - 1);
            }

            yield return null;
        }
    }

    private void SetShopCharacter() // ���� ���� �� ĳ���� ����
    {
        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            GameObject shopIcon = Instantiate(Managers.Asset.CharacterIcons[Managers.PlayerControl.CharactersIdx[i]], transform);
            shopIcon.transform.localPosition = new(150f - 150f * i, -200f, 0f);
            SortingGroup sortingGroup = shopIcon.AddComponent<SortingGroup>();
            sortingGroup.sortingLayerName = "FrontGround";
        }
    }

    private void SetNowShopSelect(int newIdx) // �ε����� 0�� ������, 1�� ����, 2���� Characters.Count+1���� ĳ����
    {
        if (newIdx < 0 || newIdx > Managers.PlayerControl.Characters.Count + 1) return;
        nowShopSelectIdx = newIdx;

        if (nowShopSelectIdx == 0)
        {
            selectCursor.localPosition = new(840f, -339f, 0f);
        }
        else if (nowShopSelectIdx == 1)
        {
            selectCursor.localPosition = new(400f, 40f, 0f);
        }
        else
        {
            selectCursor.localPosition = new(150f - 150f * (nowShopSelectIdx - 2), -60f, 0f);
        }
    }

    private void ActNowShopSelect()
    {
        if (nowShopSelectIdx == 0)
        {
            Managers.SceneFlow.GotoScene("Field");
        }
        else if (nowShopSelectIdx == 1)
        {
            Debug.Log("��������");
        }
        else
        {
            if (Managers.Status.Gold < 200) return;

            Managers.Status.Gold -= 200;

            setUpgradeCanvas.gameObject.SetActive(true);
            setUpgradeCanvas.SetUpgrades(Managers.PlayerControl.Characters[nowShopSelectIdx - 2]);
        }
    }
}
