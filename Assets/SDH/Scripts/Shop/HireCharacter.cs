using UnityEngine;
using UnityEngine.UI;

public class HireCharacter : MonoBehaviour // 상점 시작 전 동료 한 명 고용
{
    [SerializeField] private GameObject hireCanvas;
    [SerializeField] private GameObject[] characterOptions;
    private int[] characterOptionsIdx;
    private int nowSelectedIdx; // 지금 선택한 캐릭터

    private void Start()
    {
        if (Managers.PlayerControl.Characters.Count >= 4) return; // 동료가 다 차면 고용하지 않음 (해고나 동료 최대치 변경이 있다면 이 부분 수정 필요)

        Managers.PlayerControl.NowPlayer.SetActive(false); // 이거 임시코드 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        characterOptionsIdx = new int[characterOptions.Length];
        float rate = 5f / transform.parent.localScale.x;

        for (int i = 0; i < characterOptions.Length; i++)
        {
            characterOptionsIdx[i] = Random.Range(0, Managers.Asset.Characters.Length);

            GameObject characerIcon = Instantiate(Managers.Asset.Characters[characterOptionsIdx[i]], characterOptions[i].transform);

            characerIcon.transform.localPosition = Vector3.zero;
            characerIcon.transform.localScale = new(rate, rate, rate);

            Component[] components = characerIcon.GetComponents<Component>();

            Debug.Log(characerIcon.transform.position);

            for (int j = components.Length - 1; j > 0; j--) // 0번은 transform이니 제외
            {
                Destroy(components[j]);
            }
        }

        SetNowSelectedIdx(characterOptions.Length / 2); // 가운데에서 시작
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Managers.PlayerControl.NowPlayer.SetActive(true); // 이거 임시코드 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            Managers.PlayerControl.Characters.Add(Instantiate(Managers.Asset.Characters[characterOptionsIdx[nowSelectedIdx]], Managers.PlayerControl.NowPlayer.transform));
            Managers.PlayerControl.SetPlayer();
            Destroy(hireCanvas);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetNowSelectedIdx(nowSelectedIdx - 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetNowSelectedIdx(nowSelectedIdx + 1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //SetNowSelectedIdx(nowSelectedIdx - 10);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //SetNowSelectedIdx(nowSelectedIdx + 10);
        }
    }

    private void SetNowSelectedIdx(int newSelectedIdx) // 왼쪽 옵션으로 넘어가고 색을 변경
    {
        if (newSelectedIdx < 0 || newSelectedIdx > characterOptions.Length - 1) return; // 인덱스 밖

        characterOptions[nowSelectedIdx].GetComponent<Image>().color = Color.white; // 원래 선택한 옵션 강조 해제
        nowSelectedIdx = newSelectedIdx;
        characterOptions[nowSelectedIdx].GetComponent<Image>().color = Color.green; // 새로 선택한 옵션 강조 설정
    }
}
