using UnityEngine;

public class UnstoppableRageEffect : StatusEffect
{
    private const float maxBonus = 0.15f; // 15%
    private const float perHit = 0.01f; // 1% por golpe
    private const float resetTime = 5f;

    private float lastHitTime;
    private float bonusDamage; // acumulado
    private float duration = 0f; // manejamos manualmente

    public UnstoppableRageEffect()
    {
        this.duration = float.PositiveInfinity; // dura mientras se gestione externamente
    }

    public void RegisterHit()
    {
        lastHitTime = Time.time;
        bonusDamage = Mathf.Min(maxBonus, bonusDamage + perHit);
    }

    public float GetDamageMultiplier()
    {
        return 1f + bonusDamage;
    }

    public override void Tick()
    {
        // override base to handle reset
        if (Time.time - lastHitTime >= resetTime)
        {
            bonusDamage = 0f;
            lastHitTime = Time.time; // prevenir repetidos resets hasta nuevo hit
        }
    }

    protected override void OnApply()
    {
        // nothing to init beyond default
        lastHitTime = Time.time;
        bonusDamage = 0f;
    }
}
