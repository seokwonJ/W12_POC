using UnityEngine;

public class Managers : MonoBehaviour
{
    // Singleton
    public static Managers Instance => instance;
    private static Managers instance;
    #region Managers
    public static StatusManager Status => instance.status;
    private StatusManager status = new(); // �ΰ��� ���� ����
    public static ArtifactManager Artifact => instance.artifact;
    private ArtifactManager artifact = new(); // �ΰ��� ���� ����
    public static StageManager Stage => instance.stage;
    private StageManager stage = new(); // ������ ���� ����
    public static SceneFlowManager SceneFlow => instance.sceneFlow;
    private SceneFlowManager sceneFlow = new(); // �� ��ȯ �� sceneLoaded�迭 ����
    public static CameraManager Cam => instance.cam;
    private CameraManager cam = new(); // ī�޶� ����
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

    private void SetInit() // Awake �ʱ� ����
    {
        Application.targetFrameRate = 60;

        sceneFlow.Init();
        stage.Init();
    }

    private void OnDestroy() // �ʱ�ȭ
    {
        sceneFlow.Clear();
    }
}
