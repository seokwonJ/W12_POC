using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.LightTransport;

public class StatusManager // �ΰ��� �÷��� ���� ����
{
    public TextMeshProUGUI goldTxt;

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            if (goldTxt != null) goldTxt.text = gold.ToString();
        }
    }
    private int gold; // �ΰ��� ��ȭ
    public float MaxHp
    {
        get
        {
            return maxHp;
        }
        set
        {
            maxHp = value;
        }
    }
    private float maxHp; // �ΰ��ӿ� ������ ������ �缳���Ǵ� ü�°�
    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }
    private float hp; // �ΰ��� ü��
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
        maxHp = 100;
    }
}
