using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager // 씬 전환 및 sceneLoaded계열 관리
{
    public CanvasGroup FadeOutCanvas
    {
        get
        {
            return fadeOutCanvas;
        }
        set
        {
            fadeOutCanvas = value;
        }
    }
    public CanvasGroup fadeOutCanvas;

    public void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) // 씬이 로드될 때 실행
    {
        Managers.Cam.SetCameraController();

        if (scene.name == "Field") // 전투 씬이 로드될 때 실행
        {
            Managers.Stage.Stage++;
        }

        if(scene.name == "Shop") // 상점 씬이 로드될 때 실행
        {
            Managers.Status.Gold += 200;
        }
    }

    public void GotoScene(string sceneName) // string으로 씬 전환하기
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Clear()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public IEnumerator FadeOut(float maxTime) // 씬 전환 시 연출
    {
        if (fadeOutCanvas == null)
        {
            Debug.Log("FadeOutCanvas 못찾음");
            yield break;
        }

        float nowTime = 0f;

        while (nowTime <= maxTime)
        {
            fadeOutCanvas.alpha = nowTime / maxTime;
            nowTime += Time.deltaTime;
            yield return null;
        }

        yield break;
    }
}
