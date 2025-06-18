using UnityEngine;

public class TmpSelectSet : MonoBehaviour
{
    [SerializeField] GameObject vehicleCanvas;
    [SerializeField] GameObject characterCanvas;

    private void Start() // ���� �ɼ��� ���� ĵ������ �� ���� �ѱ� (���� �� �ش� ĵ������ ����)
    {
        vehicleCanvas.SetActive(true);
        characterCanvas.SetActive(true);
    }
}
