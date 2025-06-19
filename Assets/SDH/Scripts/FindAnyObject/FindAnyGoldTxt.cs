using TMPro;
using UnityEngine;

public class FindAnyGoldTxt : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = "°ñµå: " + Managers.Status.Gold.ToString();
        Managers.Status.goldTxt = GetComponent<TextMeshProUGUI>();
    }
}
