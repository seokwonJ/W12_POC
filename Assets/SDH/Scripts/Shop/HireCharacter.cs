using UnityEngine;
using UnityEngine.UI;

public class HireCharacter : MonoBehaviour // ���� ���� �� ���� �� �� ���
{
    [SerializeField] private GameObject hireCanvas;
    [SerializeField] private GameObject[] characterOptions;
    private int[] characterOptionsIdx;
    private int nowSelectedIdx; // ���� ������ ĳ����

    private void Start()
    {
        if (Managers.PlayerControl.Characters.Count >= 4) return; // ���ᰡ �� ���� ������� ���� (�ذ� ���� �ִ�ġ ������ �ִٸ� �� �κ� ���� �ʿ�)

        Managers.PlayerControl.NowPlayer.SetActive(false); // �̰� �ӽ��ڵ� @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

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

            for (int j = components.Length - 1; j > 0; j--) // 0���� transform�̴� ����
            {
                Destroy(components[j]);
            }
        }

        SetNowSelectedIdx(characterOptions.Length / 2); // ������� ����
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Managers.PlayerControl.NowPlayer.SetActive(true); // �̰� �ӽ��ڵ� @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
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

    private void SetNowSelectedIdx(int newSelectedIdx) // ���� �ɼ����� �Ѿ�� ���� ����
    {
        if (newSelectedIdx < 0 || newSelectedIdx > characterOptions.Length - 1) return; // �ε��� ��

        characterOptions[nowSelectedIdx].GetComponent<Image>().color = Color.white; // ���� ������ �ɼ� ���� ����
        nowSelectedIdx = newSelectedIdx;
        characterOptions[nowSelectedIdx].GetComponent<Image>().color = Color.green; // ���� ������ �ɼ� ���� ����
    }
}
