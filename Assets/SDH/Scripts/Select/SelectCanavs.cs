using UnityEngine;
using UnityEngine.UI;

public class SelectCanavs : MonoBehaviour // ����â ĵ���� ����
{
    private ScrollRect scrollRect;
    private int nowSelectedIdx; // ���� ������ ���ۿɼ�
    private float step; // ������ �ϳ��� �����ϴ� ��ũ�� �Ÿ� ����

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();

        nowSelectedIdx = 0;
        scrollRect.content.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.green; // 0�� �׸� ����
    }

    private void Start()
    {
        Managers.PlayerControl.IsSelecting = false;

        scrollRect.horizontalNormalizedPosition = 0f; // ��ũ���� 0f�� �ʱ�ȭ���� �ʰų� ����� Awake�� ������ scrollRect.content.rect.width�� �������� �ʾ� ����
        step = (scrollRect.content.GetChild(0).GetComponent<RectTransform>().rect.width + scrollRect.content.GetComponent<HorizontalLayoutGroup>().spacing) / (scrollRect.content.rect.width - scrollRect.viewport.rect.width);
    }

    private void Update()
    {
        if (Managers.PlayerControl.IsSelecting) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectNowSelected();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MinusNowSelectedIdx();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlusNowSelectedIdx();
        }
    }

    private void SelectNowSelected() // ���� ������ �ɼ��� ����
    {
        Managers.PlayerControl.IsSelecting = true;
        scrollRect.content.GetChild(nowSelectedIdx).GetComponent<SelectOption>().ChooseOption();
    }

    private void MinusNowSelectedIdx() // ���� �ɼ����� �Ѿ�� ���� ����
    {
        if (nowSelectedIdx <= 0) return; // �ε��� ��

        scrollRect.content.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.white; // ���� ������ �ɼ� ���� ����
        nowSelectedIdx--;
        scrollRect.content.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.green; // ���� ������ �ɼ� ���� ����

        scrollRect.horizontalNormalizedPosition = Mathf.Min(scrollRect.horizontalNormalizedPosition, step * nowSelectedIdx); // ��ũ���� �������� �̵��ؾ� �Ѵٸ� �̵�
    }

    private void PlusNowSelectedIdx() // ������ �ɼ����� �Ѿ�� ���� ����. MinusNowSelectedIdx�� ������ �ּ��� ����
    {
        if (nowSelectedIdx >= scrollRect.content.childCount - 1) return;

        scrollRect.content.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.white; //
        nowSelectedIdx++;
        scrollRect.content.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.green; //

        scrollRect.horizontalNormalizedPosition = Mathf.Max(scrollRect.horizontalNormalizedPosition, 1f - step * (scrollRect.content.childCount - 1 - nowSelectedIdx));
    }
}
