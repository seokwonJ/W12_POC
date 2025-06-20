using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager // 씬 전환 및 sceneLoaded계열 관리
{
    public FindAnyFieldCanvas FieldCanvas;
    public Canvas GameOverCanvas;

    public void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) // 씬이 로드될 때 실행
    {
        Managers.Cam.SetCameraController();

        if (scene.name == "Field") FieldSceneLoaded();
        else if (scene.name == "Shop") ShopSceneLoaded();
    }

    private void FieldSceneLoaded() // 필드 씬이 시작될 때 실행
    {


        Managers.PlayerControl.NowPlayer?.GetComponent<TmpPlayerControl>().StartFieldDirect();
    }

    private void ShopSceneLoaded() // 상점 씬이 시작될 때 실행
    {
        Managers.PlayerControl.NowPlayer?.GetComponent<TmpPlayerControl>().SetShopPosition();

        Managers.Status.Gold += 200;
    }

    public void GotoScene(string sceneName) // string으로 씬 전환하기
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ClearTxt()
    {
        if (FieldCanvas == null)
        {
            Debug.Log("FieldCanvas 못찾음");
            return;
        }

        FieldCanvas.StartClearTxt();
    }

    public void StartDirect(float waitTime, float maxTime) // 필드 시작 연출
    {
        if (FieldCanvas == null)
        {
            Debug.Log("FieldCanvas 못찾음");
            return;
        }

        FieldCanvas.StartStartDirect(waitTime, maxTime);
    }

    public void FadeOut(float maxTime) // 필드가 끝난 뒤 어두워지는 연출
    {
        if (FieldCanvas == null)
        {
            Debug.Log("FieldCanvas 못찾음");
            return;
        }

        FieldCanvas.StartFadeOut(maxTime);
    }

    public void GameOver() // 게임오버
    {
        if (GameOverCanvas == null)
        {
            Debug.Log("GameOverCanvas 못찾음");
            return;
        }

        Time.timeScale = 0f;
        GameOverCanvas.enabled = true;
    }

    public void ClearOnSceneLoaded() // 에디터용 종료 시 구독 해제
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
