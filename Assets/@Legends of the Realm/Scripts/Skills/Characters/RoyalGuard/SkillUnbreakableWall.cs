using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/RoyalGuard/UnbreakableWall")]
public class SkillUnbreakableWall : SkillBase
{
    public float aoeDamage = 50f;
    public float aoeRadius = 5f;

    public StatusEffect physicalDefenseBuff;
    public StatusEffect magicalDefenseBuff;
    public float defenseBuffMagnitude = 20f;
    public float defenseBuffDuration = 10f;

    public StatusEffect slowDebuff;
    public float slowDebuffMagnitude = 0.15f;
    public float slowDebuffDuration = 10f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        // AOE en círculo alrededor del personaje
        var all = GameObject.FindObjectsOfType<CharacterBase>();
        foreach (var enemy in all)
        {
            if (user.IsEnemyTo(enemy) && !enemy.stats.IsDead)
            {
                float dist = Vector3.Distance(user.transform.position, enemy.transform.position);
                if (dist <= aoeRadius)
                {
                    enemy.TakePhysicalDamage(aoeDamage);
                }
            }
        }

        // Buff para sí mismo
        var manager = user.GetComponent<StatusEffectManager>();
        if (manager != null)
        {
            manager.ApplyEffect(physicalDefenseBuff, defenseBuffMagnitude, defenseBuffDuration);
            manager.ApplyEffect(magicalDefenseBuff, defenseBuffMagnitude, defenseBuffDuration);
            manager.ApplyEffect(slowDebuff, slowDebuffMagnitude, slowDebuffDuration);
        }

        TriggerCooldown();
        yield return null;
    }
}
