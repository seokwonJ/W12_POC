using UnityEngine;

public class SelectOption : MonoBehaviour
{
    public virtual void ChooseOption() // ��ӹ޾Ƽ� ����ϱ�
    {
        Managers.PlayerControl.IsSelecting = false;
    }
}
