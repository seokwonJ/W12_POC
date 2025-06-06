using UnityEngine;

public class FindAnyGameOverCanvas : MonoBehaviour
{
    private void Start()
    {
        Managers.SceneFlow.GameOverCanvas = GetComponent<Canvas>();
    }
}
