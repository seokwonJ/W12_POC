using UnityEngine;
using UnityEngine.UI;

public class VehicleCanvas : MonoBehaviour
{
    [SerializeField] private GameObject vehicleCanvas;

    public int NowSelectedIdx => nowSelectedIdx;
    private int nowSelectedIdx; // ���� ������ ĳ����

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

        SetNowSelectedIdx(0); // �⺻ ������ 0��
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

    private void SetNowSelectedIdx(int newSelectedIdx) // ���� �ɼ����� �Ѿ�� ���� ����
    {
        if (newSelectedIdx < 0 || newSelectedIdx > transform.childCount - 1) return; // �ε��� ��

        transform.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.white; // ���� ������ �ɼ� ���� ����
        nowSelectedIdx = newSelectedIdx;
        transform.GetChild(nowSelectedIdx).GetComponent<Image>().color = Color.green; // ���� ������ �ɼ� ���� ����
    }
}
