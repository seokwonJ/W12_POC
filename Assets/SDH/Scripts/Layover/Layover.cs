using UnityEngine;
using UnityEngine.SceneManagement;

public class LayOver : MonoBehaviour // ����ü+ĳ���� ������ ���� ��. Layover ���� �ٽ� ���� DontDestroy�� �ɸ� ����ü ������ ���δ�.
{
    private void Start()
    {
        SceneManager.LoadScene("Field");
    }
}
