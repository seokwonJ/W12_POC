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
            Managers.Stage.Stage++;
        }

        if(scene.name == "Shop") // ���� ���� �ε�� �� ����
        {
            Managers.Status.Gold += 200;
        }
    }

    public void GotoScene(string sceneName) // string���� �� ��ȯ�ϱ�
    {
        SceneManager.LoadScene(sceneName);
    }
}
