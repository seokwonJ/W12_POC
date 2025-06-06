using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JSW_UpgradeManager : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;

    public GameObject character1;
    public GameObject character2;

    [Header("궁수")]
    public Image icon;
    public Text naming;
    public Text description;
    public Button button;

    public Image icon2;
    public Text naming2;
    public Text description2;
    public Button button2;

    public Image icon3;
    public Text naming3;
    public Text description3;
    public Button button3;



    [Header("전사")]
    public Image wicon;
    public Text wnaming;
    public Text wdescription;
    public Button wbutton;

    public Image wicon2;
    public Text wnaming2;
    public Text wdescription2;
    public Button wbutton2;

    public Image wicon3;
    public Text wnaming3;
    public Text wdescription3;
    public Button wbutton3;

    public  List<CharacterUpgrade> _ShowUpgrades;
    public List<CharacterUpgrade> _ShowUpgrades2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            panel1.GetComponent<CanvasGroup>().alpha = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            panel2.GetComponent<CanvasGroup>().alpha = 1;
        }
    }


    private void Start()
    {
        // 캐릭터마다 upgradeController가 있으며 거기에 접근해서 업그레이드 띄워주면 됨
        // 레벨업은 아직 없으니 레벨업했다고 생각하고 하면 될듯

        _ShowUpgrades = character1.GetComponent<UpgradeController>().ShowUpgradeChoices();

        icon.sprite = _ShowUpgrades[0].icon;
        naming.text = _ShowUpgrades[0].upgradeName;
        description.text = _ShowUpgrades[0].description;
        button.onClick.AddListener(() => _ShowUpgrades[0].ApplyUpgrade(character1));

        icon2.sprite = _ShowUpgrades[1].icon;
        naming2.text = _ShowUpgrades[1].upgradeName;
        description2.text = _ShowUpgrades[1].description;
        button2.onClick.AddListener(() => _ShowUpgrades[1].ApplyUpgrade(character1));

        icon3.sprite = _ShowUpgrades[2].icon;
        naming3.text = _ShowUpgrades[2].upgradeName;
        description3.text = _ShowUpgrades[2].description;
        button3.onClick.AddListener(() => _ShowUpgrades[2].ApplyUpgrade(character1));


        _ShowUpgrades2 = character2.GetComponent<UpgradeController>().ShowUpgradeChoices();

        wicon.sprite = _ShowUpgrades2[0].icon;
        wnaming.text = _ShowUpgrades2[0].upgradeName;
        wdescription.text = _ShowUpgrades2[0].description;
        wbutton.onClick.AddListener(() => _ShowUpgrades2[0].ApplyUpgrade(character2));
        
        wicon2.sprite = _ShowUpgrades2[1].icon;
        wnaming2.text = _ShowUpgrades2[1].upgradeName;
        wdescription2.text = _ShowUpgrades2[1].description;
        wbutton2.onClick.AddListener(() => _ShowUpgrades2[1].ApplyUpgrade(character2));
        
        wicon3.sprite = _ShowUpgrades2[2].icon;
        wnaming3.text = _ShowUpgrades2[2].upgradeName;
        wdescription3.text = _ShowUpgrades2[2].description;
        wbutton3.onClick.AddListener(() => _ShowUpgrades2[2].ApplyUpgrade(character2));
    }


    //public List<CharacterUpgrade> allUpgrades;
    //private List<CharacterUpgrade> _availableUpgrades;
    //private List<CharacterUpgrade> _acquiredUpgrades = new();

    //public void ShowUpgradeChoices()
    //{
    //    _availableUpgrades = allUpgrades.Except(_acquiredUpgrades).ToList();
    //    var choices = _availableUpgrades.OrderBy(x => Random.value).Take(3).ToList();

    //    // UI에 띄우고 선택하게 함
    //    foreach (var upgrade in choices)
    //    {
    //        // upgrade.upgradeName, upgrade.description, upgrade.icon 으로 UI 구성
    //    }
    //}

    //public void ApplyUpgrade(CharacterUpgrade upgrade, GameObject character)
    //{
    //    upgrade.ApplyUpgrade(character);
    //    _acquiredUpgrades.Add(upgrade);
    //}
}
