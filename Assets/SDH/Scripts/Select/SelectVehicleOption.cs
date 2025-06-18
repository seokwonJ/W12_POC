using UnityEngine;

public class SelectVehicleOption : SelectOption
{
    [SerializeField] private GameObject vehicleCanvasObj;
    [SerializeField] private VehicleCanvas vehicleCanvas;

    public override void ChooseOption()
    {
        vehicleCanvasObj.SetActive(true);
        vehicleCanvas.StartGetInput();
    }
}
