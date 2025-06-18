using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    public GameObject[] backgrounds1; // 1world 배경
    public GameObject[] backgrounds2; // 2world 배경
    public GameObject[] backgrounds3; // 3world 배경
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        foreach(GameObject bg in backgrounds1)
        {
            bg.SetActive(false); // 모든 배경 비활성화
        }
        foreach (GameObject bg in backgrounds2)
        {
            bg.SetActive(false); // 모든 배경 비활성화
        }
        foreach (GameObject bg in backgrounds3)
        {
            bg.SetActive(false); // 모든 배경 비활성화
        }

        int world = Managers.Stage.World;
        int stage = Managers.Stage.Stage;
        // 현재 스테이지에 해당하는 배경 활성화
        ref GameObject[] selectedBackgrounds = ref backgrounds1;

        switch (world)
        {
            case 1:
                selectedBackgrounds = ref backgrounds1;
                break;
            case 2:
                selectedBackgrounds = ref backgrounds2;
                break;
            case 3:
                selectedBackgrounds = ref backgrounds3;
                break;
            default:
                Debug.LogWarning("Invalid world number: " + world);
                return; // 잘못된 월드 번호인 경우 함수 종료
        }
        if (stage >= 1 && stage < selectedBackgrounds.Length)
        {
            backgrounds1[stage].SetActive(true); // 현재 스테이지에 해당하는 배경 활성화
        }
        else
        {
            Debug.LogWarning("Invalid stage number: " + stage);
        }
    }
}
