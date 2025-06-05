using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpRestartBtn : MonoBehaviour
{
    public void RestartGame()
    {
        Debug.Log("´©¸§");
        Destroy(Managers.PlayerControl.NowPlayer);
        Time.timeScale = 1;
        Managers.SceneFlow.GameOverCanvas.enabled = false;

        Managers.Status.StartGame();
        Managers.Stage.StartGame();

        SceneManager.LoadScene("Layover");
    }
}
