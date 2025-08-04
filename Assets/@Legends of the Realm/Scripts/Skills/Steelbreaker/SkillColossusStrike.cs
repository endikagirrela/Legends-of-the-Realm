using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Steelbreaker/ColossusStrike")]
public class SkillColossusStrike : SkillBase
{
    public float directDamagePercent = 2.5f; // 250%
    public float coneDamagePercent = 1.5f;   // 150%
    public float coneAngle = 60f;
    public float coneRange = 4f;
    public float knockdownDuration = 1.5f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (!IsReady()) yield break;

        // Golpe directo
        float direct = user.Stats.CalculatePhysicalAttackDamage() * directDamagePercent;
        target.Stats.TakePhysicalDamage(direct);
        target.ApplyStatusEffect(new KnockdownEffect(knockdownDuration));

        // Onda en cono detrás del impacto
        Collider[] hits = Physics.OverlapSphere(user.transform.position, coneRange);
        foreach (var hit in hits)
        {
            CharacterBase enemy = hit.GetComponent<CharacterBase>();
            if (enemy == null || !enemy.IsEnemyTo(user) || enemy == target) continue;

            Vector3 to = enemy.transform.position - user.transform.position;
            if (Vector3.Angle(user.transform.forward, to) <= coneAngle / 2f)
            {
                float coneDamage = user.Stats.CalculatePhysicalAttackDamage() * coneDamagePercent;
                enemy.Stats.TakePhysicalDamage(coneDamage);
                enemy.ApplyStatusEffect(new KnockdownEffect(knockdownDuration * 0.5f));
            }
        }

        RegisterRageHit(user);
        TriggerCooldown();
    }

    private void RegisterRageHit(CharacterBase user)
    {
        foreach (var field in user.GetType().GetField("activeEffects", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(user) as System.Collections.IList)
        {
            if (field is UnstoppableRageEffect rage)
            {
                rage.RegisterHit();
                break;
            }
        }
    }
}
