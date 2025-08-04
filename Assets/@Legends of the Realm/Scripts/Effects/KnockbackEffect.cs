using UnityEngine;

public class KnockbackEffect : StatusEffect
{
    private Vector3 force;

    public KnockbackEffect(Vector3 forceVector)
    {
        force = forceVector;
        duration = 0.1f; // immediate
    }

    protected override void OnApply()
    {
        if (target != null)
        {
            target.ApplyKnockback(force);
        }
    }
}
