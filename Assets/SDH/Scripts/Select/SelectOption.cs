using UnityEngine;

public class SelectOption : MonoBehaviour
{
    public virtual void ChooseOption() // 상속받아서 사용하기
    {
        Managers.PlayerControl.IsSelecting = false;
    }
}
