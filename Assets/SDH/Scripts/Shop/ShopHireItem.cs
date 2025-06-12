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

    public override void BuyItem() // ���� ���
    {
        Managers.PlayerControl.Characters.Add(Instantiate(Managers.Asset.Characters[characterOptionIdx], Managers.PlayerControl.NowPlayer.transform));
        Managers.PlayerControl.SetPlayer();

        shopControl.IsHired = true;
        Destroy(hireCanvas);
    }

    private void SetItem() // characterOptionIdx�� ������ �ش� ĳ���͸� �������� ǥ��
    {
        float rate = 5f / hireCanvas.transform.localScale.x;

        GameObject characerIcon = Instantiate(Managers.Asset.CharacterIcons[characterOptionIdx], transform);
    }
}
