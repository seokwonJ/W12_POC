using UnityEngine;
using UnityEngine.UI;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField] private GameObject characterCanvas;

    private GridLayoutGroup gridLayoutGroup;
    public int NowSelectedIdx => nowSelectedIdx;
    private int nowSelectedIdx; // 지금 선택한 캐릭터

    private void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    private void Start()
    {
        //int size = (int)(4 / transform.parent.localScale.x);
        //gridLayoutGroup.cellSize = new(size, size);

        float rate = 3f / transform.parent.localScale.x;

        foreach (GameObject character in Managers.Asset.Characters)
        {
            GameObject characterOption = Instantiate(Managers.Asset.OptionTemplate, transform);

            GameObject characerIcon = Instantiate(character, Vector3.zero, Quaternion.identity, characterOption.transform);

            characerIcon.transform.localScale = new(rate, rate, rate);

            Component[] components = characerIcon.GetComponents<Component>();

            for(int i = components.Length - 1; i > 0; i--) // 0번은 transform이니 제외
            {
                Destroy(components[i]); // 이거 좀 이상하긴 한데... 고칠까?
            }
        }

        SetNowSelectedIdx(0); // 기본 선택은 0번
        characterCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Managers.PlayerControl.IsSelecting = false;
            characterCanvas.SetActive(false);
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
            SetNowSelectedIdx(nowSelectedIdx - 10);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetNowSelectedIdx(nowSelectedIdx + 10);
        }
    }

    private void SetNowSelectedIdx(int newSelectedIdx) // 왼쪽 옵션으로 넘어가고 색을 변경
    {
        if (newSelectedIdx < 0 || newSelectedIdx > transform.childCount - 1) return; // 인덱스 밖

        transform.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.white; // 원래 선택한 옵션 강조 해제
        nowSelectedIdx = newSelectedIdx;
        transform.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.green; // 새로 선택한 옵션 강조 설정
    }
}
