using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.LightTransport;

public class StatusManager // �ΰ��� �÷��� ���� ����
{
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
        }
    }
    private int gold = 0; // �ΰ��� ��ȭ

    public void StartGame() // ���� ����
    {
        gold = 0;
    }
}
