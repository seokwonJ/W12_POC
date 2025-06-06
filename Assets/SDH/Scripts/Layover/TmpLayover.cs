using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpLayOver : MonoBehaviour // ����ü+ĳ���� ������ ���� ��. Layover ���� �ٽ� ���� DontDestroy�� �ɸ� ����ü ������ ���̴ϱ� ����
{
    private void Update() // �Ҵ� ���� ���������� �ӽ÷� Update�� �ڽ��ϴ�. @@@@@@@@@@@@@@@@@@@@@@@@@@@
    {
        for (int i = 0; i < Managers.PlayerControl.Characters.Count; i++)
        {
            Managers.PlayerControl.Characters[i].transform.localPosition = new(0.5f - i * 0.5f, 1.2f, 0f);
        }

        SceneManager.LoadScene("Field");
    }
}
