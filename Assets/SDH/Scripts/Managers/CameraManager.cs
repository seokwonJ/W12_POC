using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager
{
    public int riderCount;
    private CameraController cameraController;

    public void Init()
    {
        SceneManager.sceneLoaded += SetCameraController;
    }

    public void SetCameraController(Scene scene, LoadSceneMode mode) // ī�޶���Ʈ�ѷ� ã��
    {
        if (cameraController == null) cameraController = Camera.main.GetComponent<CameraController>();
        if (cameraController == null) Debug.Log("CameraController ã�� �� ����");
    }

    public void LandCharacter() // ĳ���Ͱ� ž������ ��
    {
        cameraController.SetOrthographicSize(0.1f);
        cameraController.StartShake(0.1f, 0.1f);
    }
}
