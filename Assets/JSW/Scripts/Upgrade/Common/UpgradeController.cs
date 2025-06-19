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
        _availableUpgrades = allUpgrades
    .Where(upg => !_acquiredUpgrades.Any(acq => acq.upgradeName == upg.upgradeName))
    .ToList();

        List<CharacterUpgrade> choices = _availableUpgrades.OrderBy(x => Random.value).Take(3).ToList();

        return choices;
    }

    public void ApplyUpgrade(CharacterUpgrade upgrade, GameObject character)
    {
        _acquiredUpgrades.Add(upgrade);
        UpgradeNum += 1;
    }
}
