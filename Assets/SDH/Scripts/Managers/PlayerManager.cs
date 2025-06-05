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
    private GameObject nowPlayer; // 현재 플레이어 오브젝트
}
