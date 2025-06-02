using UnityEngine;

public class TmpPlayerControl : MonoBehaviour // 플레이어의 전투-상점 씬 전환을 컨트롤하는 스크립트
{
    public GameObject[] characters; // 씬 전환시 동료 컨트롤용 변수. 일단은 퍼블릭으로 선언하지만 수정 필요
    private static GameObject instance; // 플레이어는 파괴 불가

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = gameObject;
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

        characters[0].transform.localPosition = new(0.5f, 0.9f); // 동료들 위치 초기화
        characters[1].transform.localPosition = new(0f, 0.9f);
        characters[2].transform.localPosition = new(-0.5f, 0.9f);
        characters[3].transform.localPosition = new(-1f, 0.9f);
    }
}
