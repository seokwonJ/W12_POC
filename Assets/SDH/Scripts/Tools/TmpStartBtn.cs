using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpStartBtn : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Select");
    }
}
