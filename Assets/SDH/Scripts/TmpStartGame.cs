using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpStartGame : MonoBehaviour // ���� �����ϴ� ��ũ��Ʈ. DontDestroy�鸸 �����ϰ� �ٷ� �ھ� ������ �Ѱ��ִ� �뵵��.
{
    private void Start()
    {
        SceneManager.LoadScene("JJHStageScene_SDH");
    }
}
