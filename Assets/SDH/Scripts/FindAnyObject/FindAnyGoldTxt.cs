using TMPro;
using UnityEngine;

public class FindAnyGoldTxt : MonoBehaviour
{
    private void Start()
    {
        Managers.Status.goldTxt = GetComponent<TextMeshProUGUI>();
        GetComponent<TextMeshProUGUI>().text = "���: " + Managers.Status.Gold.ToString();
    }
}
