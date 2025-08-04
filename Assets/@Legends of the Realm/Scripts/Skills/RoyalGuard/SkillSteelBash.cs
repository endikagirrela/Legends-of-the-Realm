using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/RoyalGuard/SteelBash")]
public class SkillSteelBash : SkillBase
{
    public float damagePercent = 0.5f;
    public float stunDuration = 2f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (!IsReady()) yield break;
        float damage = user.Stats.physicalDamage * damagePercent;
        target.TakePhysicalDamage(damage);
        target.ApplyStatusEffect(new StunEffect(stunDuration));
        TriggerCooldown();
    }
}
