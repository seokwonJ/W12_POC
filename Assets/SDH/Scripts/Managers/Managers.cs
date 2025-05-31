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
    private ArtifactManager artifact = new(); // 인게임 유물 관리
    public static StageManager Stage => instance.stage;
    private StageManager stage = new(); // 씬 전환 관리 (전투-상점 등)

    public static RiderManager Rider => instance.rider;
    private RiderManager rider = new();
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

    private void SetInit() // Awake 초기 설정
    {
        Application.targetFrameRate = 60;

        rider.Init();
    }
}
