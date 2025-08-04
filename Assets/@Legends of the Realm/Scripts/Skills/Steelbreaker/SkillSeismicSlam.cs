using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Steelbreaker/SeismicSlam")]
public class SkillSeismicSlam : SkillBase
{
    public float damagePercent = 1.5f;
    public float knockbackForce = 5f;
    public float radius = 3f;
    public float cooldownOverride = 8f; // si quieres forzar

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (!IsReady()) yield break;

        // Impacto central
        float baseDamage = user.Stats.CalculatePhysicalAttackDamage() * damagePercent;
        target.Stats.TakePhysicalDamage(baseDamage);
        // knockback al objetivo principal
        Vector3 dir = (target.transform.position - user.transform.position).normalized;
        target.ApplyKnockback(dir * knockbackForce);
        target.ApplyStatusEffect(new KnockdownEffect(1f)); // asume knockdown dura 1s

        // Ground shake & area afectado
        Collider[] hits = Physics.OverlapSphere(user.transform.position, radius);
        foreach (var hit in hits)
        {
            CharacterBase enemy = hit.GetComponent<CharacterBase>();
            if (enemy != null && enemy.IsEnemyTo(user) && enemy != target)
            {
                enemy.Stats.TakePhysicalDamage(baseDamage * 0.5f); // daño secundario reducido
            }
        }

        // Registrar hit en rage
        RegisterRageHit(user);

        TriggerCooldown();
    }

    private void RegisterRageHit(CharacterBase user)
    {
        // similar a antes
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
