using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpStartBtn : MonoBehaviour
{
    public void StartGame()
    {
        Managers.Status.StartGame();
        Managers.Stage.StartGame();

        SceneManager.LoadScene("Layover");
    }
}
