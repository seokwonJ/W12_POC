using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager // �� ��ȯ �� sceneLoaded�迭 ����
{
    public Canvas FadeOutCanvas;
    public Canvas GameOverCanvas;
    public SetUpgradeCanvas SetUpgradeCanvasCS;

    public void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) // ���� �ε�� �� ����
    {
        Managers.Cam.SetCameraController();

        Managers.PlayerControl.NowPlayer?.GetComponent<TmpPlayerControl>().SetStartPosition();

        if (scene.name == "Field") FieldSceneLoaded();
        if (scene.name == "Shop") ShopSceneLoaded();
    }

    private void FieldSceneLoaded() // �ʵ� ���� ���۵� �� ����
    {
        Managers.Stage.Stage++;
    }

    private void ShopSceneLoaded() // ���� ���� ���۵� �� ����
    {
        Managers.Status.Gold += 200;
    }

    public void GotoScene(string sceneName) // string���� �� ��ȯ�ϱ�
    {
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator FadeOut(float maxTime) // �� ��ȯ �� ����
    {
        if (FadeOutCanvas == null)
        {
            Debug.Log("FadeOutCanvas ��ã��");
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

    public void GameOver() // ���ӿ���
    {
        if (GameOverCanvas == null)
        {
            Debug.Log("GameOverCanvas ��ã��");
            return;
        }

        Time.timeScale = 0f;
        GameOverCanvas.enabled = true;
    }

    public void Clear() // �����Ϳ� ���� �� ���� ����
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
