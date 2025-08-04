using UnityEngine;

public class SlowEffect : StatusEffect
{
    private float slowPercent;
    private float originalMovementSpeed;

    public SlowEffect(float slowPercent, float duration)
    {
        this.slowPercent = Mathf.Clamp01(slowPercent);
        this.duration = duration;
    }

    protected override void OnApply()
    {
        if (target != null)
        {
            originalMovementSpeed = target.Stats.movementSpeed;
            target.Stats.movementSpeed -= originalMovementSpeed * slowPercent;
        }
    }

    public override void OnExpire()
    {
        if (target != null)
        {
            target.Stats.movementSpeed = originalMovementSpeed;
        }
    }
}
