using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    private void Start()
    {
        Managers.SceneFlow.GameOverCanvas = GetComponent<Canvas>();
    }
}
