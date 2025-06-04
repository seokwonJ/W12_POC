using UnityEngine;

public class CameraManager
{
    public int riderCount;
    private CameraController cameraController;

    public void SetCameraController() // ī�޶���Ʈ�ѷ� ã��
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
