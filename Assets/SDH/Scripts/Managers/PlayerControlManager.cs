using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager // 플레이어 관리
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
    private bool isSelecting; // (시작 전 선택창에서) 특정 옵션을 건드리고 있는지 여부
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
    private GameObject nowPlayer; // 현재 비행체 오브젝트
    public bool[] CharactersCheck => charactersCheck;
    private bool[] charactersCheck; // 동료 오브젝트 중복 확인
    public List<GameObject> Characters => characters;
    private List<GameObject> characters = new(); // 현재 동료 오브젝트들

    public void StartGame() // 게임 시작 (nowPlayer와 characters를 설정한 뒤 실행해야 함)
    {
        charactersCheck = new bool[Managers.Asset.Characters.Length];
        nowPlayer.GetComponent<TmpPlayerControl>().StartGame();
    }

    public void SetPlayer() // 고용할 때마다 호출
    {
        nowPlayer.GetComponent<TmpPlayerControl>().SetPlayer();
    }

    public void Reset()
    {
        nowPlayer = null;
        characters = new();
    }
}
