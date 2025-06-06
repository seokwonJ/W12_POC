using UnityEngine;

public class FindAnySetUpgradeCanvas : MonoBehaviour
{
    private void Start()
    {
        Managers.SceneFlow.SetUpgradeCanvasCS = GetComponent<SetUpgradeCanvas>();
    }
}
