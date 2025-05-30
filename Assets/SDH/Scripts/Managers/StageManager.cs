using System.Collections;
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

    public IEnumerator StartStage() // 스테이지 시작
    {
        float leftTime = stageTime; // 버텨야 하는 시간

        while (leftTime > 0)
        {
            leftTime -= Time.deltaTime;

            yield return null;
        }

        // 스테이지 클리어
    }
}
