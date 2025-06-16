using TMPro;
using System;

public class StatusManager // 인게임 플레이 스탯 관리
{
    public TextMeshProUGUI goldTxt;

    // 추가: HP 변경 이벤트
    public event Action OnHpChanged;

    public int DamagePlus
    {
        get
        {
            return damagePlus;
        }
        set
        {
            damagePlus = value;
        }
    }
    private int damagePlus; // 모든 동료에게 영향을 주는 비행기에 귀속되는 공격력
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            if (goldTxt != null) goldTxt.text = "골드: " + gold.ToString();
        }
    }
    private int gold; // 인게임 재화
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
    private float maxHp; // 인게임에 진입할 때마다 재설정되는 체력값
    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            OnHpChanged?.Invoke(); // HP가 바뀔 때마다 이벤트 호출
        }
    }
    private float hp; // 인게임 체력
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

    public void StartGame() // 게임 시작. 루트 함수는 Stage임
    {
        damagePlus = 0;
        gold = 0;
        maxHp = 100;
    }
}
