using UnityEngine;

public class FadeOutCanvas : MonoBehaviour
{
    private void Start()
    {
        Managers.SceneFlow.fadeOutCanvas = GetComponent<CanvasGroup>();
    }
}
