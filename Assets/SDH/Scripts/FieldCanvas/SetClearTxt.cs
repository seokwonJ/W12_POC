using TMPro;
using UnityEngine;

public class SetClearTxt : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clearTxt;

    private void Start()
    {
        clearTxt.text = $"Stage {Managers.Stage.World}-{Managers.Stage.Stage} Clear!!";
    }
}
