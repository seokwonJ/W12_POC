using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeController : MonoBehaviour
{
    public int UpgradeNum = 0;
    public List<CharacterUpgrade> allUpgrades;
    private List<CharacterUpgrade> _availableUpgrades;
    public List<CharacterUpgrade> _acquiredUpgrades = new();

    public List<CharacterUpgrade> ShowUpgradeChoices()
    {
        if (UpgradeNum >= 5) return null;
        _availableUpgrades = allUpgrades.Except(_acquiredUpgrades).ToList();
        List<CharacterUpgrade> choices = _availableUpgrades.OrderBy(x => Random.value).Take(3).ToList();

        return choices;
    }

    public void ApplyUpgrade(CharacterUpgrade upgrade, GameObject character)
    {
        print("나 사용했니>?!!!!!!!!!!!!!!!!!!!!!");
        _acquiredUpgrades.Add(upgrade);
        UpgradeNum += 1;
    }
}
