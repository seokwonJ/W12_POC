using UnityEngine;

public abstract class CharacterUpgrade : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;

    protected float attackPowerUpPercent;
    protected float attackSpeedUpPercent;
    protected float ProjectileSpeedUpPercent;
    protected float ProjectileSizeUpPercent;
    protected float KnockbackPowerUpPercent;
    protected float CriticalProbabilityUpPercent;
    protected float CriticalDamageUpPercent;
    protected float AttackRangeUpPercent;

    protected float ManaRegenSpeedDownAttackPowerUp_ManaRegenPercent;
    protected float ManaRegenSpeedDownAttackPowerUp_AttackPowerPercent;
    protected float ManaRegenSpeedUPAttackPowerDown_ManaRegenPercent;
    protected float ManaRegenSpeedUPAttackPowerDown_AttackPowerPercent;

    public abstract void ApplyUpgrade(GameObject character);
}
