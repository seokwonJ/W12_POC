using UnityEngine;

public class FindAnyCharacterCanvas : MonoBehaviour
{
    private void Start()
    {
        Managers.Shop.characterCanvas = GetComponent<Canvas>();
    }
}
