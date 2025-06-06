using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager // 씬 전환 및 sceneLoaded계열 관리
{
    public Canvas FadeOutCanvas;
    public Canvas GameOverCanvas;
    public SetUpgradeCanvas SetUpgradeCanvasCS;

    public void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) // 씬이 로드될 때 실행
    {
        Managers.Cam.SetCameraController();

        Managers.PlayerControl.NowPlayer?.GetComponent<TmpPlayerControl>().SetStartPosition();

        if (scene.name == "Field") FieldSceneLoaded();
        if (scene.name == "Shop") ShopSceneLoaded();
    }

    private void FieldSceneLoaded() // 필드 씬이 시작될 때 실행
    {
        Managers.Stage.Stage++;
    }

    private void ShopSceneLoaded() // 상점 씬이 시작될 때 실행
    {
        Managers.Status.Gold += 200;
    }

    public void GotoScene(string sceneName) // string으로 씬 전환하기
    {
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator FadeOut(float maxTime) // 씬 전환 시 연출
    {
        if (FadeOutCanvas == null)
        {
            Debug.Log("FadeOutCanvas 못찾음");
            yield break;
        }

        FadeOutCanvas.enabled = true;
        float nowTime = 0f;

        while (nowTime <= maxTime)
        {
            FadeOutCanvas.GetComponent<CanvasGroup>().alpha = nowTime / maxTime;
            nowTime += Time.deltaTime;
            yield return null;
        }

        FadeOutCanvas.enabled = false;

        yield break;
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

    public void Clear() // 에디터용 종료 시 구독 해제
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
