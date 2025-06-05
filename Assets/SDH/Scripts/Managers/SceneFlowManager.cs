using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager // �� ��ȯ �� sceneLoaded�迭 ����
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

    public void Clear()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public IEnumerator FadeOut(float maxTime) // �� ��ȯ �� ����
    {
        if (fadeOutCanvas == null)
        {
            Debug.Log("FadeOutCanvas ��ã��");
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
