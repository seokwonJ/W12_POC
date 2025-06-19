using UnityEngine;

public class SelectCharacterOption : SelectOption
{
    [SerializeField] private GameObject characterCanvasObj;
    [SerializeField] private CharacterCanvas characterCanvas;

    public override void ChooseOption()
    {
        characterCanvasObj.SetActive(true);
        characterCanvas.StartGetInput();
    }
}
