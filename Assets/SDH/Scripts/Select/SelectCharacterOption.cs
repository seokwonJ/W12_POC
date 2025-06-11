using UnityEngine;

public class SelectCharacterOption : SelectOption
{
    [SerializeField] private GameObject characterCanvas;

    public override void ChooseOption()
    {
        characterCanvas.SetActive(true);
    }
}
