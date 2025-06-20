using UnityEngine;

public class ShopHireItem : ShopItem
{
    [SerializeField] private GameObject hireCanvas;
    [SerializeField] private ShopControl shopControl;

    public int CharacterOptionIdx
    {
        get
        {
            return characterOptionIdx;
        }
        set
        {
            characterOptionIdx = value;
            SetItem();
        }
    }
    private int characterOptionIdx;

    public override void BuyItem() // 동료 고용
    {
        Managers.PlayerControl.Characters.Add(Instantiate(Managers.Asset.Characters[characterOptionIdx], Managers.PlayerControl.NowPlayer.transform));
        Managers.PlayerControl.CharactersIdx.Add(characterOptionIdx);
        Managers.PlayerControl.CharactersCheck[characterOptionIdx] = true;
        Managers.PlayerControl.SetPlayer();

        shopControl.IsHired = true;
        Destroy(hireCanvas);
    }

    private void SetItem() // characterOptionIdx을 받으면 해당 캐릭터를 선택지에 표시
    {
        float rate = 5f / hireCanvas.transform.localScale.x;

        GameObject characerIcon = Instantiate(Managers.Asset.CharacterIcons[characterOptionIdx], transform);
    }
}
