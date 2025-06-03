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
    public int RiderCount
    {
        get
        {
            return riderCount;
        }
        set
        {
            if (value > riderCount) Managers.Cam.LandCharacter(); // �����ߴٸ� ī�޶� ����
            riderCount = value;
        }
    }
    private int riderCount = 0; // ����ü�� ź �ο� ��

    public void StartGame() // ���� ����
    {
        gold = 0;
    }
}
