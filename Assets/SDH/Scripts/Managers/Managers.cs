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
    private ArtifactManager artifact; // �ΰ��� ���� ����
    public static StageManager Stage => instance.stage;
    private StageManager stage; // �� ��ȯ ���� (����-���� ��)
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

    private void SetInit() // �ʱ� ����
    {
        Application.targetFrameRate = 60;
    }
}
