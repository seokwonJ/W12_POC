using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CharacterCanvas : MonoBehaviour
{
    [SerializeField] private GameObject characterCanvas;
    [SerializeField] private SelectCanavs selectCanavs;
    [SerializeField] private Transform thumbnail;

    public int NowSelectedIdx => nowSelectedIdx;
    private int nowSelectedIdx; // ���� ������ ĳ����

    private void Start()
    {
        foreach (GameObject icon in Managers.Asset.CharacterIcons)
        {
            GameObject characterOption = Instantiate(Managers.Asset.OptionTemplate, transform);

            Instantiate(icon, characterOption.transform);
        }

        SetNowSelectedIdx(0); // �⺻ ������ 0��
        SetThumbnail();
        characterCanvas.SetActive(false);
    }

    public void StartGetInput()
    {
        StartCoroutine(GetInput());
    }

    private IEnumerator GetInput()
    {
        yield return null;

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines();
                SetThumbnail();
                selectCanavs.StartGetInput();
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
                SetNowSelectedIdx(nowSelectedIdx - 5);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SetNowSelectedIdx(nowSelectedIdx + 5);
            }

            yield return null;
        }
    }

    private void SetNowSelectedIdx(int newSelectedIdx) // �ٸ� �ɼ����� �Ѿ�� ���� ����
    {
        if (newSelectedIdx < 0 || newSelectedIdx > transform.childCount - 1) return; // �ε��� ��

        transform.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.white; // ���� ������ �ɼ� ���� ����
        nowSelectedIdx = newSelectedIdx;
        transform.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.green; // ���� ������ �ɼ� ���� ����
    }

    private void SetThumbnail()
    {
        if (thumbnail.childCount > 0) Destroy(thumbnail.GetChild(0).gameObject);

        GameObject thumbnailIcon = Instantiate(Managers.Asset.CharacterIcons[nowSelectedIdx], thumbnail);
        SortingGroup sortingGroup = thumbnailIcon.AddComponent<SortingGroup>();
        sortingGroup.sortingLayerName = "FrontGround";
    }
}
