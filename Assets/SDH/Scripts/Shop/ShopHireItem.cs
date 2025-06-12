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
        Managers.PlayerControl.SetPlayer();

        shopControl.IsHired = true;
        Destroy(hireCanvas);
    }

    private void SetItem() // characterOptionIdx을 받으면 해당 캐릭터를 선택지에 표시
    {
        float rate = 5f / hireCanvas.transform.localScale.x;

        GameObject characerIcon = Instantiate(Managers.Asset.Characters[characterOptionIdx], transform);

        characerIcon.transform.localPosition = Vector3.zero;
        characerIcon.transform.localScale = new(rate, rate, rate);

        Component[] components = characerIcon.GetComponents<Component>();

        for (int j = components.Length - 1; j > 0; j--) // 0번은 transform이니 제외
        {
            Destroy(components[j]);
        }
    }
}
