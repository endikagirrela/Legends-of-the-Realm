using UnityEngine;

public class DamageReductionEffect : StatusEffect
{
    private float reductionPercent;
    private bool physicalOnly;

    private float originalPhysicalResistance;
    private float originalMagicalResistance;

    public DamageReductionEffect(float reductionPercent, float duration, bool physicalOnly = false)
    {
        this.reductionPercent = reductionPercent;
        this.duration = duration;
        this.physicalOnly = physicalOnly;
    }

    protected override void OnApply()
    {
        if (target != null)
        {
            if (physicalOnly)
            {
                originalPhysicalResistance = target.Stats.physicalResistance;
                target.Stats.physicalResistance += originalPhysicalResistance * reductionPercent;
            }
            else
            {
                originalPhysicalResistance = target.Stats.physicalResistance;
                originalMagicalResistance = target.Stats.magicalResistance;
                target.Stats.physicalResistance += originalPhysicalResistance * reductionPercent;
                target.Stats.magicalResistance += originalMagicalResistance * reductionPercent;
            }
        }
    }

    public override void OnExpire()
    {
        if (target != null)
        {
            if (physicalOnly)
            {
                target.Stats.physicalResistance = originalPhysicalResistance;
            }
            else
            {
                target.Stats.physicalResistance = originalPhysicalResistance;
                target.Stats.magicalResistance = originalMagicalResistance;
            }
        }
    }
}
