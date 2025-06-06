using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpRestartBtn : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("����� ����");
        Destroy(Managers.PlayerControl.NowPlayer);
        Time.timeScale = 1;
        Managers.SceneFlow.GameOverCanvas.enabled = false;

        Destroy(Managers.PlayerControl.NowPlayer);
        Managers.PlayerControl.Reset();
        Managers.Status.StartGame();
        //Managers.Stage.StartGame();
        Managers.Stage.Stage--; //�ӽ÷� �̾��ϱ� �����ϰ� ����

        SceneManager.LoadScene("Layover");
    }
}
