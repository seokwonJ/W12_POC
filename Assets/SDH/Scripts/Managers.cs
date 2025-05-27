using UnityEngine;

public class Managers : MonoBehaviour
{
    // Singleton
    public static Managers Instance => instance;
    private static Managers instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init()
    {
        Application.targetFrameRate = 60;
    }
}
