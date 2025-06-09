using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    public GameObject[] backgrounds; // 배경 오브젝트 배열
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        foreach(GameObject bg in backgrounds)
        {
            bg.SetActive(false); // 모든 배경 비활성화
        }

        int stageNum = Managers.Stage.Stage;
        if (stageNum >= 0 && stageNum < backgrounds.Length)
        {
            backgrounds[stageNum].SetActive(true); // 현재 스테이지에 해당하는 배경 활성화
        }
        else
        {
            Debug.LogWarning("Invalid stage number: " + stageNum);
        }
    }
}
