using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Steelbreaker/UnstoppableCharge")]
public class SkillUnstoppableCharge : SkillBase
{
    public float damagePercentPerHit = 1.4f; // 140%
    public float chargeDistance = 6f;
    public float slowPercent = 0.4f;
    public float slowDuration = 2f;
    public float speed = 10f; // movement speed during charge
    public float hitRadius = 1f; // radius to detect enemies while charging

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (!IsReady()) yield break;
        yield return user.StartCoroutine(DoCharge(user));
    }

    private IEnumerator DoCharge(CharacterBase user)
    {
        Vector3 start = user.transform.position;
        Vector3 end = start + user.transform.forward * chargeDistance;
        float traveled = 0f;
        float step = speed * Time.deltaTime;

        // To avoid hitting the same enemy repeatedly in one frame, you can track them
        var alreadyHit = new HashSet<CharacterBase>();

        while (traveled < chargeDistance)
        {
            float move = speed * Time.deltaTime;
            user.transform.position += user.transform.forward * move;
            traveled += move;

            Collider[] hits = Physics.OverlapSphere(user.transform.position, hitRadius);
            foreach (var hit in hits)
            {
                CharacterBase enemy = hit.GetComponent<CharacterBase>();
                if (enemy != null && enemy.IsEnemyTo(user) && !alreadyHit.Contains(enemy))
                {
                    // Damage with rage multiplier
                    float baseDamage = user.Stats.CalculatePhysicalAttackDamage() * damagePercentPerHit;
                    var rage = user.GetEffect<UnstoppableRageEffect>();
                    float multiplier = 1f;
                    if (rage != null)
                    {
                        multiplier = rage.GetDamageMultiplier();
                        rage.RegisterHit();
                    }

                    enemy.Stats.TakePhysicalDamage(baseDamage * multiplier);

                    // Apply slow
                    enemy.ApplyStatusEffect(new SlowEffect(slowPercent, slowDuration));

                    alreadyHit.Add(enemy);
                }
            }

            yield return null;
        }

        TriggerCooldown();
    }
}
