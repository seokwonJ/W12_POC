using UnityEngine;

public abstract class CharacterUpgrade : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;

    protected float attackPowerUpPercent = 20;
    protected float attackSpeedUpPercent = 20;
    protected float ProjectileSpeedUpPercent = 50;
    protected float ProjectileSizeUpPercent = 20;
    protected float KnockbackPowerUpPercent = 20;
    protected float CriticalProbabilityUpPercent = 25;
    protected float CriticalDamageUpPercent = 40;
    protected float AttackRangeUpPercent = 50;

    protected float ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent = 50;
    protected float ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent = 10;
    protected float ManaRegenSpeedUPAbilityPowerUp_ManaRegenPercent = 25;
    protected float ManaRegenSpeedUPAbilityPowerUp_AbilityPowerPercent = 10;

    public abstract void ApplyUpgrade(GameObject character);
}
