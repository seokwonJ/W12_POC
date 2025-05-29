using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeController : MonoBehaviour
{
    public List<CharacterUpgrade> allUpgrades;
    private List<CharacterUpgrade> _availableUpgrades;
    private List<CharacterUpgrade> _acquiredUpgrades = new();

    public List<CharacterUpgrade> ShowUpgradeChoices()
    {
        _availableUpgrades = allUpgrades.Except(_acquiredUpgrades).ToList();
        List<CharacterUpgrade> choices = _availableUpgrades.OrderBy(x => Random.value).Take(3).ToList();

        return choices;
    }

    public void ApplyUpgrade(CharacterUpgrade upgrade, GameObject character)
    {
        upgrade.ApplyUpgrade(character);
        _acquiredUpgrades.Add(upgrade);
    }
}
