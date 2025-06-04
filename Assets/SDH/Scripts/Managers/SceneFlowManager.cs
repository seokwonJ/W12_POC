using UnityEngine.SceneManagement;

public class SceneFlowManager
{
    public void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) // 씬이 로드될 때 실행
    {
        Managers.Cam.SetCameraController();

        if (scene.name == "Field") // 전투 씬이 로드될 때 실행
        {

        }

        if(scene.name == "Shop") // 상점 씬이 로드될 때 실행
        {

        }
    }

    public void GotoScene(string sceneName) // string으로 씬 전환하기
    {
        SceneManager.LoadScene(sceneName);
    }
}
