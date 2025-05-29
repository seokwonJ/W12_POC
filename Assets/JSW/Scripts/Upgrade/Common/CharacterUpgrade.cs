using UnityEngine;

public abstract class CharacterUpgrade : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;

    public abstract void ApplyUpgrade(GameObject character);
}
