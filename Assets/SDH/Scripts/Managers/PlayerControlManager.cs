using System.Collections.Generic;
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
    private GameObject nowPlayer; // ���� ����ü ������Ʈ
    public List<GameObject> Characters => characters;
    private List<GameObject> characters = new(); // ���� ���� ������Ʈ��
}
