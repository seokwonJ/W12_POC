using UnityEngine;
using UnityEngine.UI;

public class VehicleCanvas : MonoBehaviour
{
    [SerializeField] private GameObject vehicleCanvas;

    public int NowSelectedIdx => nowSelectedIdx;
    private int nowSelectedIdx; // 지금 선택한 캐릭터

    private void Start()
    {
        foreach(GameObject icon in Managers.Asset.VehicleIcons)
        {
            float rate = 1f / transform.parent.localScale.x;

            GameObject vehicleOption = Instantiate(Managers.Asset.OptionTemplate, transform);

            GameObject vehicleIcon = Instantiate(icon, vehicleOption.transform);

            vehicleIcon.transform.localPosition = Vector3.zero;
            vehicleIcon.transform.localScale = new(rate, rate, rate);
        }

        SetNowSelectedIdx(0); // 기본 선택은 0번
        vehicleCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Managers.PlayerControl.IsSelecting = false;
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

    private void SetNowSelectedIdx(int newSelectedIdx) // 왼쪽 옵션으로 넘어가고 색을 변경
    {
        if (newSelectedIdx < 0 || newSelectedIdx > transform.childCount - 1) return; // 인덱스 밖

        transform.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.white; // 원래 선택한 옵션 강조 해제
        nowSelectedIdx = newSelectedIdx;
        transform.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.green; // 새로 선택한 옵션 강조 설정
    }
}
