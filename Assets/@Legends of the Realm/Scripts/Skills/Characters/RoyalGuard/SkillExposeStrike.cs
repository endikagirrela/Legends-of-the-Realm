using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/RoyalGuard/ExposeStrike")]
public class SkillExposeStrike : SkillBase
{
    public float damage = 30f;
    public StatusEffect defenseDebuff;
    public float debuffMagnitude = 10f;
    public float debuffDuration = 5f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (target == null || target.stats.IsDead || !user.IsInRange(target)) yield break;

        target.TakePhysicalDamage(damage);

        var manager = target.GetComponent<StatusEffectManager>();
        if (manager != null)
        {
            manager.ApplyEffect(defenseDebuff, debuffMagnitude, debuffDuration);
        }

        TriggerCooldown();
    }
}
