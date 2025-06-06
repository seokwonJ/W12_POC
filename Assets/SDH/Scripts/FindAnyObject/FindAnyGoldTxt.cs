using TMPro;
using UnityEngine;

public class FindAnyGoldTxt : MonoBehaviour
{
    private void Start()
    {
        Managers.Status.goldTxt = GetComponent<TextMeshProUGUI>();
        GetComponent<TextMeshProUGUI>().text = "°ñµå: " + Managers.Status.Gold.ToString();
    }
}
