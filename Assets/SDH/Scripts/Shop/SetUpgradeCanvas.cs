using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetUpgradeCanvas : MonoBehaviour
{
    [SerializeField] private Image icon0;
    [SerializeField] private TextMeshProUGUI name0;
    [SerializeField] private TextMeshProUGUI description0;
    [SerializeField] private Button button0;
    [SerializeField] private Image icon1;
    [SerializeField] private TextMeshProUGUI name1;
    [SerializeField] private TextMeshProUGUI description1;
    [SerializeField] private Button button1;
    [SerializeField] private Image icon2;
    [SerializeField] private TextMeshProUGUI name2;
    [SerializeField] private TextMeshProUGUI description2;
    [SerializeField] private Button button2;

    public void SetUpgrades(GameObject character)
    {
        Debug.Log("업그레이드 시작");
        List<CharacterUpgrade> upgrades = character.GetComponent<UpgradeController>().ShowUpgradeChoices();

        GetComponent<Canvas>().enabled = true;

        icon0.sprite = upgrades[0].icon;
        name0.text = upgrades[0].name;
        description0.text = upgrades[0].description;
        button0.onClick.AddListener(() => { upgrades[0].ApplyUpgrade(character); GetComponent<Canvas>().enabled = false; });

        icon1.sprite = upgrades[1].icon;
        name1.text = upgrades[1].name;
        description1.text = upgrades[1].description;
        button1.onClick.AddListener(() => { upgrades[1].ApplyUpgrade(character); GetComponent<Canvas>().enabled = false; });

        icon2.sprite = upgrades[2].icon;
        name2.text = upgrades[2].name;
        description2.text = upgrades[2].description;
        button2.onClick.AddListener(() => { upgrades[2].ApplyUpgrade(character); GetComponent<Canvas>().enabled = false; });
    }
}
