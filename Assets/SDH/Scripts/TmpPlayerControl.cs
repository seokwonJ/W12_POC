using UnityEngine;

public class TmpPlayerControl : MonoBehaviour // �÷��̾��� ����-���� �� ��ȯ�� ��Ʈ���ϴ� ��ũ��Ʈ
{
    public GameObject[] characters; // �� ��ȯ�� ���� ��Ʈ�ѿ� ����. �ϴ��� �ۺ����� ���������� ���� �ʿ�
    private static GameObject instance; // �÷��̾�� �ı� �Ұ�

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = gameObject;
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init()
    {
        Managers.Stage.OnField = true;
    }

    public void ToggleOnField()
    {
        Managers.Stage.OnField = !Managers.Stage.OnField;

        characters[0].transform.localPosition = new(0.5f, 0.9f); // ����� ��ġ �ʱ�ȭ
        characters[1].transform.localPosition = new(0f, 0.9f);
        characters[2].transform.localPosition = new(-0.5f, 0.9f);
        characters[3].transform.localPosition = new(-1f, 0.9f);
    }
}
