using UnityEngine;

public class StunEffect : StatusEffect
{
    private float originalAttackSpeed;

    public StunEffect(float stunDuration)
    {
        duration = stunDuration;
    }

    protected override void OnApply()
    {
        // Here you would disable actions; for placeholder, you could set attack speed to 0
        if (target != null)
        {
            originalAttackSpeed = target.Stats.attackSpeed;
            target.Stats.attackSpeed = 0f;
            // TODO: disable input/AI
        }
    }

    public override void OnExpire()
    {
        if (target != null)
        {
            target.Stats.attackSpeed = originalAttackSpeed;
            // TODO: re-enable input/AI
        }
    }
}
