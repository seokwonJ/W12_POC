using UnityEngine;

public class PlayerControlManager // �÷��̾� ����
{
    public GameObject NowPlayer
    {
        get
        {
            return nowPlayer;
        }
        set
        {
            nowPlayer = value;
        }
    }
    private GameObject nowPlayer; // ���� �÷��̾� ������Ʈ
}
