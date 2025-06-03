using UnityEngine;

public class TmpPlayerControl : MonoBehaviour // 플레이어의 전투-상점 씬 전환을 컨트롤하는 스크립트
{
    public GameObject[] characters; // 씬 전환시 동료 컨트롤용 변수. 일단은 퍼블릭으로 선언하지만 수정 필요

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init()
    {
        Managers.Stage.OnField = true;
    }

    public void ToggleOnField()
    {
        Managers.Stage.OnField = !Managers.Stage.OnField;

        foreach(GameObject character in characters)
        {
            character.transform.SetParent(transform); // 애들이 스킬쓰고 점프 뛸때마다 부모가 풀리던데 왜인지는 모름
        }

        characters[0].transform.localPosition = new(0.5f, 0.9f); // 동료들 위치 초기화
        characters[1].transform.localPosition = new(0f, 0.9f);
        characters[2].transform.localPosition = new(-0.5f, 0.9f);
        characters[3].transform.localPosition = new(-1f, 0.9f);

        if (Managers.Stage.OnField) // 필드 돌입
        {
            
        }
        else // 상점 돌입
        {

        }
    }
}
