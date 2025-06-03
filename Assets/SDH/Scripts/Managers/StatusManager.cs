using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.LightTransport;

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
    public int RiderCount
    {
        get
        {
            return riderCount;
        }
        set
        {
            if (value > riderCount) Managers.Cam.LandCharacter(); // 착지했다면 카메라 흔들기
            riderCount = value;
        }
    }
    private int riderCount = 0; // 비행체에 탄 인원 수

    public void StartGame() // 게임 시작
    {
        gold = 0;
    }
}
