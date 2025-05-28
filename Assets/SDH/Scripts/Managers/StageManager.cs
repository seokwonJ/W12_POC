using UnityEngine;

public class StageManager // 씬 전환 관리 (전투-상점 등)
{
    public float StageTime
    {
        get
        {
            return stageTime;
        }
        set
        {
            stageTime = value;
        }
    }
    private float stageTime; // 스테이지 전환 시간
}
