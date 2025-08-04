using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "PassiveSkills/UnstoppableRage")]
public class PassiveUnstoppableRage : PassiveSkillBase
{
    // Este passive solo asegura que el character tenga el efecto activo y lo exponga.
    public override void ApplyPassiveEffect(CharacterBase character)
    {
        // A�adir el efecto y mantener referencia en el personaje (puedes guardarlo en un campo si lo necesitas)
        UnstoppableRageEffect rage = new UnstoppableRageEffect();
        character.ApplyStatusEffect(rage);
    }

    // Helper de utilidad para que otros scripts obtengan el multiplicador
    public static UnstoppableRageEffect GetRageEffect(CharacterBase character)
    {
        // asume que character expone sus efectos; si no, tendr�as que a�adir getter
        // buscar entre sus efectos uno de tipo UnstoppableRageEffect
        // Aqu� se hace de forma simplificada usando reflection si no tienes acceso: (mejor exponer lista p�blica)
        return null; // implementar seg�n tu acceso a los efectos activos
    }
}
