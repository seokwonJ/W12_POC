using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SetUpgradeCanvas : MonoBehaviour
{
    [SerializeField] private ShopCharacterCanavs shopCharacterCanavs;
    [SerializeField] private Transform selectCursor;
    [SerializeField] private Image icon0;
    [SerializeField] private TextMeshProUGUI name0;
    [SerializeField] private TextMeshProUGUI description0;
    [SerializeField] private Image icon1;
    [SerializeField] private TextMeshProUGUI name1;
    [SerializeField] private TextMeshProUGUI description1;
    [SerializeField] private Image icon2;
    [SerializeField] private TextMeshProUGUI name2;
    [SerializeField] private TextMeshProUGUI description2;
    private GameObject nowCharacter;
    private List<CharacterUpgrade> nowUpgrades;
    private int nowUpgradeSelectIdx;

    private void Start()
    {
        SetNowUpgradeSelect(1); // �⺻���� ���
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActNowUpgradeSelect();
            shopCharacterCanavs.StartGetInput();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetNowUpgradeSelect(nowUpgradeSelectIdx - 1); // �ε��� �� �ٸ� �͵�� ������ �ݴ��ӿ� ����
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetNowUpgradeSelect(nowUpgradeSelectIdx + 1);
        }
    }

    private void SetNowUpgradeSelect(int newIdx)
    {
        if (newIdx < 0 || newIdx > 2) return; // �������̶�� ���� ����

        nowUpgradeSelectIdx = newIdx;
        selectCursor.transform.localPosition = new(-600f + newIdx * 600f, 315f, 0f);
    }

    private void ActNowUpgradeSelect()
    {
        nowUpgrades[nowUpgradeSelectIdx].ApplyUpgrade(nowCharacter);

        gameObject.SetActive(false);
    }

    public void SetUpgrades(GameObject character) // �Է¹��� ����(GameObject)�� ���׷��̵� ��� �߰�
    {
        nowCharacter = character;
        nowUpgrades = character.GetComponent<UpgradeController>().ShowUpgradeChoices();

        icon0.sprite = nowUpgrades[0].icon;
        name0.text = nowUpgrades[0].name;
        description0.text = nowUpgrades[0].description;

        icon1.sprite = nowUpgrades[1].icon;
        name1.text = nowUpgrades[1].name;
        description1.text = nowUpgrades[1].description;

        icon2.sprite = nowUpgrades[2].icon;
        name2.text = nowUpgrades[2].name;
        description2.text = nowUpgrades[2].description;
    }
}
