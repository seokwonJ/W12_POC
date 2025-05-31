using UnityEngine;

public class RiderManager // 여기 수정 필요 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
{
    public int riderCount;
    private CameraController cameraController;

    public void Init()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        if (cameraController == null) Debug.Log("CameraController 찾을 수 없음");
    }

    public void RiderCountUp()
    {
        riderCount += 1;
        cameraController.SetOrthographicSize(0.1f);
        cameraController.StartShake(0.1f, 0.1f);

    }

    public void RiderCountDown()
    {
        riderCount -= 1;
    }
}
