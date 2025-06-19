using UnityEngine;

public class FindAnyFieldCanvas : MonoBehaviour
{

    public CurtainFadeOut PanelCanvasGroup => panelCanvasGroup;
    [SerializeField] private CurtainFadeOut panelCanvasGroup;
    public SetClearTxt ClearTxt => clearTxt;
    [SerializeField] private SetClearTxt clearTxt;

    private void Start()
    {
        Managers.SceneFlow.FieldCanvas = this;
    }

    public void StartFadeOut(float maxTime)
    {
        panelCanvasGroup.gameObject.SetActive(true);
        StartCoroutine(panelCanvasGroup.FadeOut(maxTime));
    }

    public void StartClearTxt()
    {
        clearTxt.gameObject.SetActive(true);
    }
}
