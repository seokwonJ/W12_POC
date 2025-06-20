using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager // �� ��ȯ �� sceneLoaded�迭 ����
{
    public FindAnyFieldCanvas FieldCanvas;
    public Canvas GameOverCanvas;

    public void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) // ���� �ε�� �� ����
    {
        Managers.Cam.SetCameraController();

        if (scene.name == "Field") FieldSceneLoaded();
        else if (scene.name == "Shop") ShopSceneLoaded();
    }

    private void FieldSceneLoaded() // �ʵ� ���� ���۵� �� ����
    {


        Managers.PlayerControl.NowPlayer?.GetComponent<TmpPlayerControl>().StartFieldDirect();
    }

    private void ShopSceneLoaded() // ���� ���� ���۵� �� ����
    {
        Managers.PlayerControl.NowPlayer?.GetComponent<TmpPlayerControl>().SetShopPosition();

        Managers.Status.Gold += 200;
    }

    public void GotoScene(string sceneName) // string���� �� ��ȯ�ϱ�
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ClearTxt()
    {
        if (FieldCanvas == null)
        {
            Debug.Log("FieldCanvas ��ã��");
            return;
        }

        FieldCanvas.StartClearTxt();
    }

    public void StartDirect(float waitTime, float maxTime) // �ʵ� ���� ����
    {
        if (FieldCanvas == null)
        {
            Debug.Log("FieldCanvas ��ã��");
            return;
        }

        FieldCanvas.StartStartDirect(waitTime, maxTime);
    }

    public void FadeOut(float maxTime) // �ʵ尡 ���� �� ��ο����� ����
    {
        if (FieldCanvas == null)
        {
            Debug.Log("FieldCanvas ��ã��");
            return;
        }

        FieldCanvas.StartFadeOut(maxTime);
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

    public void ClearOnSceneLoaded() // �����Ϳ� ���� �� ���� ����
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
