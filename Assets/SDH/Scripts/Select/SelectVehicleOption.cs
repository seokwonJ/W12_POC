using UnityEngine;

public class SelectVehicleOption : SelectOption
{
    [SerializeField] private GameObject vehicleCanvas;

    public override void ChooseOption()
    {
        vehicleCanvas.SetActive(true);
    }
}
