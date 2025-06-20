using UnityEngine;

public class FindAnyFieldCanvas : MonoBehaviour
{

    public CurtainFadeOut PanelCanvasGroup => panelCanvasGroup;
    [SerializeField] private CurtainFadeOut panelCanvasGroup;
    public SetClearTxt ClearTxt => clearTxt;
    [SerializeField] private SetClearTxt clearTxt;
    public ActStartDirect StartDirect => startDirect;
    [SerializeField] private ActStartDirect startDirect;

    private void Awake()
    {
        Managers.SceneFlow.FieldCanvas = this;
    }

    public void StartStartDirect(float waitTime, float maxTime)
    {
        startDirect.gameObject.SetActive(true);
        startDirect.StartFadeOut(waitTime, maxTime);
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
