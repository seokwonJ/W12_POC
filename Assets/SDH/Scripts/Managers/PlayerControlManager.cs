using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager // �÷��̾� ����
{
    public bool IsSelecting
    {
        get
        {
            return isSelecting;
        }
        set
        {
            isSelecting = value;
        }
    }
    private bool isSelecting; // (���� �� ����â����) Ư�� �ɼ��� �ǵ帮�� �ִ��� ����
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
    public bool[] CharactersCheck => charactersCheck;
    private bool[] charactersCheck; // ���� ������Ʈ �ߺ� Ȯ��
    public List<GameObject> Characters => characters;
    private List<GameObject> characters = new(); // ���� ���� ������Ʈ��

    public void StartGame() // ���� ���� (nowPlayer�� characters�� ������ �� �����ؾ� ��)
    {
        charactersCheck = new bool[Managers.Asset.Characters.Length];
        nowPlayer.GetComponent<TmpPlayerControl>().StartGame();
    }

    public void SetPlayer() // ����� ������ ȣ��
    {
        nowPlayer.GetComponent<TmpPlayerControl>().SetPlayer();
    }

    public void Reset()
    {
        nowPlayer = null;
        characters = new();
    }
}
