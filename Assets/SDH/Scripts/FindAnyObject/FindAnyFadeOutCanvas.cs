using UnityEngine;

public class FindAnyFadeOutCanvas : MonoBehaviour
{
    private void Start()
    {
        Managers.SceneFlow.FadeOutCanvas = GetComponent<Canvas>();
    }
}
