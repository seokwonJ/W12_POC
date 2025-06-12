using UnityEngine;
using UnityEngine.UI;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField] private GameObject characterCanvas;

    private GridLayoutGroup gridLayoutGroup;
    public int NowSelectedIdx => nowSelectedIdx;
    private int nowSelectedIdx; // ���� ������ ĳ����

    private void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }

    private void Start()
    {
        //int size = (int)(4 / transform.parent.localScale.x);
        //gridLayoutGroup.cellSize = new(size, size);

        float rate = 3f / transform.parent.localScale.x;

        foreach (GameObject icon in Managers.Asset.CharacterIcons)
        {
            GameObject characterOption = Instantiate(Managers.Asset.OptionTemplate, transform);

            GameObject characterIcon = Instantiate(icon, characterOption.transform);

            characterIcon.transform.localPosition = Vector3.zero;
            characterIcon.transform.localScale = new(rate, rate, rate);
        }

        SetNowSelectedIdx(0); // �⺻ ������ 0��
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

    private void SetNowSelectedIdx(int newSelectedIdx) // ���� �ɼ����� �Ѿ�� ���� ����
    {
        if (newSelectedIdx < 0 || newSelectedIdx > transform.childCount - 1) return; // �ε��� ��

        transform.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.white; // ���� ������ �ɼ� ���� ����
        nowSelectedIdx = newSelectedIdx;
        transform.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.green; // ���� ������ �ɼ� ���� ����
    }
}
