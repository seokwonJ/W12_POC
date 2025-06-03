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

    public void SetCameraController(Scene scene, LoadSceneMode mode) // 카메라컨트롤러 찾기
    {
        if (cameraController == null) cameraController = Camera.main.GetComponent<CameraController>();
        if (cameraController == null) Debug.Log("CameraController 찾을 수 없음");
    }

    public void LandCharacter() // 캐릭터가 탑승했을 때
    {
        cameraController.SetOrthographicSize(0.1f);
        cameraController.StartShake(0.1f, 0.1f);
    }
}
