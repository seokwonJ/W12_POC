using UnityEngine;

public class TmpSelectSet : MonoBehaviour
{
    [SerializeField] GameObject vehicleCanvas;
    [SerializeField] GameObject characterCanvas;

    private void Start() // 시작 옵션을 위해 캔버스를 한 번씩 켜기 (끄는 건 해당 캔버스에 있음)
    {
        vehicleCanvas.SetActive(true);
        characterCanvas.SetActive(true);
    }
}
