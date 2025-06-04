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
    private StageManager stage = new(); // 전투나 상점 관리
    public static SceneFlowManager SceneFlow => instance.sceneFlow;
    private SceneFlowManager sceneFlow = new(); // 씬 전환 및 sceneLoaded계열 관리
    public static CameraManager Cam => instance.cam;
    private CameraManager cam = new(); // 카메라 관리
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

        sceneFlow.Init();
        stage.Init();
    }

    private void OnDestroy() // 초기화
    {
        sceneFlow.Clear();
    }
}
