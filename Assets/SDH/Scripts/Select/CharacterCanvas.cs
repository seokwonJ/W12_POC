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
    private int nowSelectedIdx; // 지금 선택한 캐릭터

    private void Start()
    {
        foreach (GameObject icon in Managers.Asset.CharacterIcons)
        {
            GameObject characterOption = Instantiate(Managers.Asset.OptionTemplate, transform);

            Instantiate(icon, characterOption.transform);
        }

        SetNowSelectedIdx(0); // 기본 선택은 0번
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

    private void SetNowSelectedIdx(int newSelectedIdx) // 다른 옵션으로 넘어가고 색을 변경
    {
        if (newSelectedIdx < 0 || newSelectedIdx > transform.childCount - 1) return; // 인덱스 밖

        transform.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.white; // 원래 선택한 옵션 강조 해제
        nowSelectedIdx = newSelectedIdx;
        transform.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.green; // 새로 선택한 옵션 강조 설정
    }

    private void SetThumbnail()
    {
        if (thumbnail.childCount > 0) Destroy(thumbnail.GetChild(0).gameObject);

        GameObject thumbnailIcon = Instantiate(Managers.Asset.CharacterIcons[nowSelectedIdx], thumbnail);
        SortingGroup sortingGroup = thumbnailIcon.AddComponent<SortingGroup>();
        sortingGroup.sortingLayerName = "FrontGround";
    }
}
