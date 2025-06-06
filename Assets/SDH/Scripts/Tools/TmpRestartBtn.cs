using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpRestartBtn : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("재시작 누름");
        Destroy(Managers.PlayerControl.NowPlayer);
        Time.timeScale = 1;
        Managers.SceneFlow.GameOverCanvas.enabled = false;

        Destroy(Managers.PlayerControl.NowPlayer);
        Managers.PlayerControl.Reset();
        Managers.Status.StartGame();
        //Managers.Stage.StartGame();
        Managers.Stage.Stage--; //임시로 이어하기 가능하게 변경

        SceneManager.LoadScene("Layover");
    }
}
