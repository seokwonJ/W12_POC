using UnityEngine;

public class FindAnyGameOverCanvas : MonoBehaviour
{
    private void Awake()
    {
        Managers.SceneFlow.GameOverCanvas = GetComponent<Canvas>();
    }
}
