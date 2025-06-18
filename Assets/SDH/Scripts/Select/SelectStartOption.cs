using UnityEngine;

public class SelectStartOption : SelectOption
{
    [SerializeField] private VehicleCanvas vehicleCanvas;
    [SerializeField] private CharacterCanvas characterCanvas;
    [SerializeField] private Canvas curtain;

    public override void ChooseOption() // 게임 시작
    {
        curtain.enabled = true;

        Managers.PlayerControl.NowPlayer = Instantiate(Managers.Asset.Vehicles[vehicleCanvas.NowSelectedIdx], Vector3.zero, Quaternion.identity);
        Managers.PlayerControl.Characters.Add(Instantiate(Managers.Asset.Characters[characterCanvas.NowSelectedIdx], Managers.PlayerControl.NowPlayer.transform));
        Managers.PlayerControl.CharactersIdx.Add(characterCanvas.NowSelectedIdx);

        Managers.PlayerControl.StartGame();
        Managers.PlayerControl.CharactersCheck[characterCanvas.NowSelectedIdx] = true;

        Managers.Status.StartGame();
        Managers.Stage.StartGame();

        Managers.SceneFlow.GotoScene("Field");
    }
}
