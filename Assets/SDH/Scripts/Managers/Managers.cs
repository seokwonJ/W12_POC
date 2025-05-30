using UnityEngine;

public class Managers : MonoBehaviour
{
    // Singleton
    public static Managers Instance => instance;
    private static Managers instance;
    #region Managers
    public static StatusManager Status => instance.status;
    private StatusManager status = new(); // 인게임 스탯 관리
    public static ArtifactManager Artifact => instance.artifact;
    private ArtifactManager artifact; // 인게임 유물 관리
    public static StageManager Stage => instance.stage;
    private StageManager stage; // 씬 전환 관리 (전투-상점 등)
    #endregion

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        SetInit();
    }

    private void SetInit() // 초기 설정
    {
        Application.targetFrameRate = 60;
    }
}
