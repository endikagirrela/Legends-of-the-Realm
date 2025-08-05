using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/RoyalGuard/ShieldBash")]
public class SkillShieldBash : SkillBase
{
    public float damage = 25f;

    [Header("Stun Effect")]
    public StatusEffect stunEffect;
    public float stunDuration = 2f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (target == null || target.stats.IsDead || !user.IsInRange(target)) yield break;

        target.TakePhysicalDamage(damage);

        var statusManager = target.GetComponent<StatusEffectManager>();
        if (statusManager != null && stunEffect != null)
        {
            statusManager.ApplyEffect(stunEffect, 0f, stunDuration);
        }

        TriggerCooldown();
        yield return null;
    }
}
