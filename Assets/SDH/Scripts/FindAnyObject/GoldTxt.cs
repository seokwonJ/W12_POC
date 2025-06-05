using TMPro;
using UnityEngine;

public class GoldTxtFinder : MonoBehaviour
{
    private void Start()
    {
        Managers.Status.goldTxt = GetComponent<TextMeshProUGUI>();
    }
}
