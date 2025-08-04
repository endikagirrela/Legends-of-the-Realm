using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Steelbreaker/HeavySlash")]
public class SkillHeavySlash : SkillBase
{
    public float damagePercent = 1.1f; // 110%

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (!IsReady()) yield break;

        float baseDamage = user.Stats.CalculatePhysicalAttackDamage() * damagePercent;
        // aplicar bonus de rage si existe
        float multiplier = 1f;
        if (user is CharacterBase)
        {
            // asumiendo que tienes un método expuesto para obtener el rage effect
            // aquí simplificamos: busca en efectos activos
            foreach (var field in user.GetType().GetField("activeEffects", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(user) as System.Collections.IList)
            {
                if (field is UnstoppableRageEffect rage)
                {
                    multiplier = rage.GetDamageMultiplier();
                    rage.RegisterHit();
                    break;
                }
            }
        }

        target.Stats.TakePhysicalDamage(baseDamage * multiplier);
        TriggerCooldown();
    }
}
