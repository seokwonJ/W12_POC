using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject restartButton;
    public GameObject gameClearUI;
    public int currentSceneNum;

    private void Start()
    {
        print("timeScale : " + Time.timeScale);
        Time.timeScale = 1;
    }

    public void GameOverUI()
    {
        Time.timeScale = 0;
        restartButton.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneNum);
    }

    public void GameClearUI()
    {
        Time.timeScale = 0;
        gameClearUI.SetActive(true);
    }

    public void NextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneNum+1);
    }
}
