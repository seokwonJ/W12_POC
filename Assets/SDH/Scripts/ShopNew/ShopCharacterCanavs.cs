using UnityEngine;

public class ShopCharacterCanavs : MonoBehaviour
{
    [SerializeField] private SetUpgradeCanvas setUpgradeCanvas;
    [SerializeField] private GameObject select;
    private int nowShopSelectIdx;

    public void Start()
    {
        SetShopCharacter();

        SetNowShopSelect(Managers.PlayerControl.Characters.Count - 1);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActNowShopSelect();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetNowShopSelect(nowShopSelectIdx - 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetNowShopSelect(nowShopSelectIdx + 1);
        }
    }

    public void SetShopCharacter() // 상점 진입 시 캐릭터 진열
    {
        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            GameObject shopIcon = Instantiate(Managers.Asset.CharacterIcons[Managers.PlayerControl.CharactersIdx[i]], Managers.Shop.characterCanvas.transform);
            shopIcon.transform.localPosition = new(150f - 150f * i, -200f, 0f);
        }
    }

    public void SetNowShopSelect(int newIdx) // 인덱스값이 크기와 같다면 상인, 그것보다 1 크다면 나가기
    {
        if (newIdx < 0 || newIdx > Managers.PlayerControl.Characters.Count + 1) return;
        nowShopSelectIdx = newIdx;

        if(nowShopSelectIdx< Managers.PlayerControl.Characters.Count)
        {
            select.transform.localPosition = new(150f - 150f * nowShopSelectIdx, -200f, 0f);
        }
        else if (nowShopSelectIdx == Managers.PlayerControl.Characters.Count)
        {
            select.transform.localPosition = new(400f, -100f, 0f);
        }
        else
        {
            select.transform.localPosition = new(850f, -490f, 0f);
        }
    }

    public void ActNowShopSelect()
    {
        if (nowShopSelectIdx < Managers.PlayerControl.Characters.Count)
        {
            if (Managers.Status.Gold < 200) return;

            Managers.Status.Gold -= 200;
            setUpgradeCanvas.SetUpgrades(Managers.PlayerControl.Characters[nowShopSelectIdx]);
        }
        else if (nowShopSelectIdx == Managers.PlayerControl.Characters.Count)
        {
            Debug.Log("상점주인");
        }
        else
        {
            Managers.SceneFlow.GotoScene("Field");
        }
    }
}
