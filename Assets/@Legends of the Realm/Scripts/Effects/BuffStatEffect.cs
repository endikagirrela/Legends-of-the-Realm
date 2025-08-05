using UnityEngine;

[CreateAssetMenu(menuName = "Status Effects/Buff Stat")]
public class BuffStatEffect : StatusEffect
{
    public enum StatType
    {
        PhysicalDamage,
        MagicalDamage,
        PhysicalDefense,
        MagicalDefense,
        AttackSpeed,
        AttackRange,
        Speed
    }

    public enum ModifierMode
    {
        FlatBonus,
        PercentageMultiplier
    }

    public StatType statToBuff;
    public ModifierMode modifierMode;

    public override void ApplyEffect(CharacterBase target, StatusEffectInstance instance)
    {
        var stats = target.stats;

        switch (statToBuff)
        {
            case StatType.PhysicalDamage:
                ApplyModifier(stats.physicalDamage, instance);
                break;
            case StatType.MagicalDamage:
                ApplyModifier(stats.magicalDamage, instance);
                break;
            case StatType.PhysicalDefense:
                ApplyModifier(stats.physicalResistance, instance);
                break;
            case StatType.MagicalDefense:
                ApplyModifier(stats.magicalResistance, instance);
                break;
            case StatType.AttackSpeed:
                ApplyModifier(stats.attackSpeed, instance);
                break;
            case StatType.AttackRange:
                ApplyModifier(stats.attackRange, instance);
                break;
            case StatType.Speed:
                ApplyModifier(stats.movementSpeed, instance);
                break;
        }
    }

    public override void RemoveEffect(CharacterBase target, StatusEffectInstance instance)
    {
        var stats = target.stats;

        switch (statToBuff)
        {
            case StatType.PhysicalDamage:
                RemoveModifier(stats.physicalDamage, instance);
                break;
            case StatType.MagicalDamage:
                RemoveModifier(stats.magicalDamage, instance);
                break;
            case StatType.PhysicalDefense:
                RemoveModifier(stats.physicalResistance, instance);
                break;
            case StatType.MagicalDefense:
                RemoveModifier(stats.magicalResistance, instance);
                break;
            case StatType.AttackSpeed:
                RemoveModifier(stats.attackSpeed, instance);
                break;
            case StatType.AttackRange:
                RemoveModifier(stats.attackRange, instance);
                break;
            case StatType.Speed:
                RemoveModifier(stats.movementSpeed, instance);
                break;
        }
    }

    private void ApplyModifier(Stat stat, StatusEffectInstance instance)
    {
        switch (modifierMode)
        {
            case ModifierMode.FlatBonus:
                stat.flatBonusBuff += instance.magnitude;
                break;
            case ModifierMode.PercentageMultiplier:
                stat.multiplier *= (1f + instance.magnitude);
                break;
        }
    }

    private void RemoveModifier(Stat stat, StatusEffectInstance instance)
    {
        switch (modifierMode)
        {
            case ModifierMode.FlatBonus:
                stat.flatBonusBuff -= instance.magnitude;
                break;
            case ModifierMode.PercentageMultiplier:
                stat.multiplier /= (1f + instance.magnitude);
                break;
        }
    }
}
