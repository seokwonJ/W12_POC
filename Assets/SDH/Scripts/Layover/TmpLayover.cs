using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpLayOver : MonoBehaviour // ����ü+ĳ���� ������ ���� ��. Layover ���� �ٽ� ���� DontDestroy�� �ɸ� ����ü ������ ���̴ϱ� ����
{
    private void Start() // �Ҵ� ���� ���������� �ӽ÷� Update�� �ڽ��ϴ�. @@@@@@@@@@@@@@@@@@@@@@@@@@@
    {
        SceneManager.LoadScene("Field");
    }
}
