using UnityEngine;

public class Managers : MonoBehaviour
{
    // Singleton
    public static Managers Instance => instance;
    private static Managers instance;
    #region Managers
    public static AssetManager Asset => instance.asset;
    private AssetManager asset = new(); // Resources에서 받아오는 각종 오브젝트 관리
    public static StatusManager Status => instance.status;
    private StatusManager status = new(); // 인게임 스탯 관리
    public static ArtifactManager Artifact => instance.artifact;
    private ArtifactManager artifact = new(); // 인게임 유물 관리
    public static StageManager Stage => instance.stage;
    private StageManager stage = new(); // 전투나 상점 관리
    public static PlayerControlManager PlayerControl => instance.playerControl;
    private PlayerControlManager playerControl = new(); // 플레이어 관리
    public static SceneFlowManager SceneFlow => instance.sceneFlow;
    private SceneFlowManager sceneFlow = new(); // 씬 전환 및 sceneLoaded계열 관리
    public static CameraManager Cam => instance.cam;
    private CameraManager cam = new(); // 카메라 관리
    public static RecordManager Record => instance.record;
    private RecordManager record = new(); // 게임 기록 관리
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

        asset.Init();
        sceneFlow.Init();
    }

    private void OnDestroy() // 초기화
    {
        sceneFlow.Clear();
    }
}
