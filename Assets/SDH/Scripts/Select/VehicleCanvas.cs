using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VehicleCanvas : MonoBehaviour
{
    [SerializeField] private GameObject vehicleCanvas;
    [SerializeField] private Transform thumbnail;

    public int NowSelectedIdx => nowSelectedIdx;
    private int nowSelectedIdx; // 지금 선택한 캐릭터

    private void Start()
    {
        foreach(GameObject icon in Managers.Asset.VehicleIcons)
        {
            GameObject vehicleOption = Instantiate(Managers.Asset.OptionTemplate, transform);

            Instantiate(icon, vehicleOption.transform);
        }

        SetNowSelectedIdx(0); // 기본 선택은 0번
        SetThumbnail();
        vehicleCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Managers.PlayerControl.IsSelecting = false;
            SetThumbnail();
            vehicleCanvas.SetActive(false);
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

        GameObject thumbnailIcon = Instantiate(Managers.Asset.VehicleIcons[nowSelectedIdx], thumbnail);
        SortingGroup sortingGroup = thumbnailIcon.AddComponent<SortingGroup>();
        sortingGroup.sortingLayerName = "BackGround";
    }
}
