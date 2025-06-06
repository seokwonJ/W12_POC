using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager // 플레이어 관리
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
    private GameObject nowPlayer; // 현재 비행체 오브젝트
    public List<GameObject> Characters => characters;
    private List<GameObject> characters = new(); // 현재 영웅 오브젝트들
}
