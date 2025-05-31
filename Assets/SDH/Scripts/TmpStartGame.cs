using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpStartGame : MonoBehaviour // 게임 시작하는 스크립트. DontDestroy들만 생성하고 바로 코어 씬으로 넘겨주는 용도임.
{
    private void Start()
    {
        SceneManager.LoadScene("JJHStageScene_SDH");
    }
}
