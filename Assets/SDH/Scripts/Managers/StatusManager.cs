using UnityEngine;

public class StatusManager // 인게임 플레이 스탯 관리
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
    private int gold = 0; // 인게임 재화
}
