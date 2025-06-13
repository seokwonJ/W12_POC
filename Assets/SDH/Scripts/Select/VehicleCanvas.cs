using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VehicleCanvas : MonoBehaviour
{
    [SerializeField] private GameObject vehicleCanvas;
    [SerializeField] private Transform thumbnail;

    public int NowSelectedIdx => nowSelectedIdx;
    private int nowSelectedIdx; // ���� ������ ĳ����

    private void Start()
    {
        foreach(GameObject icon in Managers.Asset.VehicleIcons)
        {
            GameObject vehicleOption = Instantiate(Managers.Asset.OptionTemplate, transform);

            Instantiate(icon, vehicleOption.transform);
        }

        SetNowSelectedIdx(0); // �⺻ ������ 0��
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

        GameObject thumbnailIcon = Instantiate(Managers.Asset.VehicleIcons[nowSelectedIdx], thumbnail);
        SortingGroup sortingGroup = thumbnailIcon.AddComponent<SortingGroup>();
        sortingGroup.sortingLayerName = "BackGround";
    }
}
