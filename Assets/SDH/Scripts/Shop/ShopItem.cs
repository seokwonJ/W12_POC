using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public virtual void BuyItem() // ��ӹ޾Ƽ� ����ϱ�. �⺻���� ���� ������(�ʵ尡��)��
    {
        FindAnyObjectByType<TmpPlayerControl>().ToggleOnField();
    }
}
