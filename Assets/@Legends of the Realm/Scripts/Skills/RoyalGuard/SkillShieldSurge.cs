using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/RoyalGuard/ShieldSurge")]
public class SkillShieldSurge : SkillBase
{
    public float damagePercent = 0.6f;
    public float pushForce = 5f;
    public float stunDuration = 1.5f;
    public float radius = 3f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (!IsReady()) yield break;
        Collider[] hits = Physics.OverlapSphere(user.transform.position, radius);
        foreach (var hit in hits)
        {
            CharacterBase enemy = hit.GetComponent<CharacterBase>();
            if (enemy != null && enemy.IsEnemyTo(user))
            {
                enemy.TakePhysicalDamage(user.Stats.physicalDamage * damagePercent);
                enemy.ApplyStatusEffect(new StunEffect(stunDuration));
                Vector3 dir = (enemy.transform.position - user.transform.position).normalized;
                enemy.ApplyKnockback(dir * pushForce);
            }
        }
        TriggerCooldown();
    }
}
