using UnityEngine;

public class RiderManager : MonoBehaviour
{
    public static RiderManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int riderCount;
    private CameraController cameraController;

    private void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
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
