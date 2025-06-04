using UnityEngine.SceneManagement;

public class SceneFlowManager
{
    public void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) // ���� �ε�� �� ����
    {
        Managers.Cam.SetCameraController();

        if (scene.name == "Field") // ���� ���� �ε�� �� ����
        {

        }

        if(scene.name == "Shop") // ���� ���� �ε�� �� ����
        {

        }
    }

    public void GotoScene(string sceneName) // string���� �� ��ȯ�ϱ�
    {
        SceneManager.LoadScene(sceneName);
    }
}
