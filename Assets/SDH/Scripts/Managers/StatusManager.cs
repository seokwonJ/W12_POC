using TMPro;

public class StatusManager // 인게임 플레이 스탯 관리
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

    public void StartGame() // 게임 시작
    {
        gold = 0;
        maxHp = 100;
    }
}
